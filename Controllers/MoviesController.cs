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

        public async Task<IActionResult> Index()
        {
            var currentTime = DateTime.Now; // Текущее время
            var movies = await _context.Movies
                .Where(movie => movie.DateTime > currentTime) // Фильтруем только фильмы с будущей датой и временем
                .ToListAsync();

            return View(movies);
        }

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
                var movie = await _context.Movies
                    .Include(m => m.BookedSeats) // Загружаем связанные забронированные места
                    .FirstOrDefaultAsync(m => m.Id == id);

                if (movie == null) return NotFound();

                // Проверяем наличие забронированных мест
                if (movie.BookedSeats.Any())
                {
                    // Сообщаем, что изменение даты и времени недопустимо
                    if (model.DateTime != movie.DateTime)
                    {
                        ModelState.AddModelError("DateTime", "Невозможно изменить дату и время фильма, так как уже куплены билеты.");
                        // Сбрасываем изменения в DateTime
                        model.DateTime = movie.DateTime; // Возвращаем старое значение даты и времени
                        return View(model);
                    }
                }

                // Обновляем данные фильма, кроме даты и времени, если это изменение недопустимо
                movie.Title = model.Title;
                movie.ShortDesc = model.ShortDesc;
                movie.PreviewUrl = model.PreviewUrl;
                movie.Price = model.Price;

                // Дата и время будут обновлены только если изменения допустимы
                if (!model.DateTime.Equals(movie.DateTime))
                {
                    movie.DateTime = model.DateTime;
                }

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
