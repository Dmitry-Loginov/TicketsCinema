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
            foreach (var seatId in model.SelectedSeatIds)
            {
                var bookedSeat = new BookedSeat
                {
                    UserId = _userManager.GetUserId(User), // Предполагаем, что пользователь аутентифицирован
                    MovieId = model.MovieId,
                    SeatId = seatId,
                    PriceBooked = _context.Movies.Find(model.MovieId)?.Price ?? 0
                };

                _context.BookedSeats.Add(bookedSeat);
            }

            _context.SaveChanges();
            return RedirectToAction("Index", "Movies"); // Перенаправление пользователя после покупки
        }
    }
}