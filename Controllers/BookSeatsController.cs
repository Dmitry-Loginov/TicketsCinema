using Microsoft.AspNetCore.Mvc;
using TicketsCinema.Models;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace TicketsCinema.Controllers
{
    [Authorize]
    public class BookSeats : Controller
    {
        private readonly ApplicationContext _context;
        UserManager<User> _userManager;

        public BookSeats(ApplicationContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // Метод для отображения формы выбора мест для фильма
        public async Task<IActionResult> Book(int movieId)
        {
            var movie = _context.Movies
                .Include(m => m.BookedSeats)
                .FirstOrDefault(m => m.Id == movieId);

            if (movie == null)
            {
                return NotFound();
            }

            var bookedSeatIds = movie.BookedSeats.Select(bs => bs.SeatId).ToList();
            var availableSeats = _context.Seats
                .Where(s => !bookedSeatIds.Contains(s.Id))
                .ToList();

            var model = new BookSeatsViewModel
            {
                MovieId = movieId,
                Title = movie.Title,
                SelectedSeatIds = new List<int>(), // Этот список будет заполняться на странице
                AvailableSeats = availableSeats,
                AllSeats = _context.Seats.ToList()
            };

            return View(model);
        }

        // Метод для обработки покупки билетов
        [HttpPost]
        public async Task<IActionResult> Book(BookSeatsViewModel model)
        {
            var userId = _userManager.GetUserId(User);
            var user = await _userManager.FindByIdAsync(userId);
            var movie = await _context.Movies.FindAsync(model.MovieId);

            if (movie == null)
            {
                return NotFound(); // Фильм не найден
            }

            // Проверяем, достаточно ли у пользователя средств
            if (user.Budget < model.SelectedSeatIds.Count() * movie.Price)
            {
                TempData["ErrorMessage"] = "Недостаточно средств на счете.";
                return RedirectToAction("Book", new { MovieId = model.MovieId }); // Перенаправляем обратно на страницу покупки билетов
            }

            foreach (var seatId in model.SelectedSeatIds)
            {
                var bookedSeat = new BookedSeat
                {
                    UserId = userId,
                    MovieId = model.MovieId,
                    SeatId = seatId,
                    PriceBooked = movie.Price
                };

                _context.BookedSeats.Add(bookedSeat);
            }

            user.Budget -= model.SelectedSeatIds.Count() * movie.Price;
            await _context.SaveChangesAsync();

            return RedirectToAction("Index", "Movies"); // Перенаправление пользователя после покупки
        }
    }
}