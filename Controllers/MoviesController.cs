using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TicketsCinema.Models;
using TicketsCinema.ViewModels;

namespace TicketsCinema.Controllers
{
    public class MoviesController : Controller
    {
        private readonly ApplicationContext _context;
        private readonly UserManager<User> _userManager;

        public MoviesController(UserManager<User> userManager, ApplicationContext context)
        {
            _userManager = userManager;
            _context = context;
        }

        public async Task<IActionResult> Index() => View(_context.Movies.ToList());

        public async Task<IActionResult> Details(int id)
        {
            var movie = await _context.Movies.FirstOrDefaultAsync(m => m.Id == id);
            if (movie == null) return NotFound();

            var bookedSeats = await _context.BookedSeats
                .Include(bs => bs.Movie)
                .Include(bs => bs.Seat)
                .Where(bs => bs.MovieId == movie.Id)
                .ToListAsync();

            var model = new MovieDetailViewModel
            {
                Title = movie.Title,
                PreviewUrl = movie.PreviewUrl,
                ShortDesc = movie.ShortDesc,
                DateTime = movie.DateTime,
                Price = movie.Price
            };

            return View(model);
        }

        [Authorize(Roles = "admin")]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(MovieCreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                var movie = new Movie
                {
                    Title = model.Title,
                    ShortDesc = model.ShortDesc,
                    PreviewUrl = model.PreviewUrl,
                    DateTime = model.DateTime,
                    Price = model.Price
                };
                _context.Movies.Add(movie);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Edit(int id)
        {
            var movie = await _context.Movies.FindAsync(id);
            if (movie == null) return NotFound();

            var model = new MovieEditViewModel
            {
                Id = movie.Id,
                Title = movie.Title,
                ShortDesc = movie.ShortDesc,
                PreviewUrl = movie.PreviewUrl,
                DateTime = movie.DateTime,
                Price = movie.Price
            };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, MovieEditViewModel model)
        {
            if (id != model.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var movie = await _context.Movies.FindAsync(id);
                if (movie == null) return NotFound();

                movie.Title = model.Title;
                movie.ShortDesc = model.ShortDesc;
                movie.PreviewUrl = model.PreviewUrl;
                movie.DateTime = model.DateTime;
                movie.Price = model.Price;

                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Delete(int id)
        {
            var movie = await _context.Movies.FindAsync(id);
            if (movie == null) return NotFound();
            return View(movie);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var movie = await _context.Movies.Include(m => m.BookedSeats).FirstOrDefaultAsync(m => m.Id == id);
            if (movie == null) return NotFound();

            // Возврат средств пользователям
            foreach (var bookedSeat in movie.BookedSeats)
            {
                var user = await _userManager.FindByIdAsync(bookedSeat.UserId);
                if (user != null)
                {
                    // Здесь следует добавить вашу логику для возврата средств
                    user.Budget += bookedSeat.PriceBooked; // Увеличиваем бюджет пользователя на стоимость билета
                    await _userManager.UpdateAsync(user); // Обновляем пользователя в базе данных
                }
            }

            _context.Movies.Remove(movie); // Удаляем фильм
            await _context.SaveChangesAsync(); // Сохраняем изменения

            return RedirectToAction(nameof(Index));
        }
    }
}
