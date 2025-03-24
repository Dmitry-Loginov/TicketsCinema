using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TicketsCinema.ViewModels;
using TicketsCinema.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

namespace TicketsCinema.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly ApplicationContext _context;

        public AccountController(UserManager<User> userManager, SignInManager<User> signInManager, ApplicationContext context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _context = context;
        }
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                User user = new User { UserName = model.UserName };
                // добавляем пользователя
                var result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    // установка куки
                    await _signInManager.SignInAsync(user, false);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }
            }
            return View(model);
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View(new LoginViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result =
                    await _signInManager.PasswordSignInAsync(model.UserName, model.Password, model.RememberMe, false);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", "Неправильный логин и (или) пароль");
                }
            }
            return View(model);
        }

        [Authorize]
        public async Task<IActionResult> Details(string? id = null)
        {
            User user;

            if (id == null) // Если id не передан, получаем текущего пользователя
            {
                user = await _userManager.GetUserAsync(User);
            }
            else // Если id передан, проверяем роль
            {
                if (!User.IsInRole("admin")) // Проверяем, является ли пользователь админом
                {
                    return Forbid(); // Запрещаем доступ
                }

                user = await _userManager.FindByIdAsync(id);
            }

            if (user == null)
            {
                return NotFound();
            }

            var bookedSeats = await _context.BookedSeats
                .Include(bs => bs.Movie)
                .Include(bs => bs.Seat)
                .Where(bs => bs.UserId == user.Id)
                .ToListAsync();

            var pastTickets = bookedSeats.Where(bs => bs.Movie.DateTime < DateTime.Now).ToList();
            var upcomingTickets = bookedSeats.Where(bs => bs.Movie.DateTime >= DateTime.Now).ToList();

            var model = new DetailViewModel
            {
                UserId = id,
                UserName = user.UserName,
                Budget = user.Budget,
                PastTickets = pastTickets,
                UpcomingTickets = upcomingTickets
            };

            return View(model);
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> AddBudget(string? userId = null)
        {
            var model = new AddBudgetViewModel();

            if (!string.IsNullOrEmpty(userId) && User.IsInRole("admin"))
            {
                model.UserId = userId; // Устанавливаем UserId для администратора
            }

            return View(model); // Возвращаем представление с моделью
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddBudget(AddBudgetViewModel model, string? userId = null)
        {

            User userToUpdate;

            if (string.IsNullOrEmpty(userId)) // Если userId не указан, пополняем бюджет текущего пользователя
            {
                userToUpdate = await _userManager.GetUserAsync(User);
            }
            else // Иначе, получаем пользователя по ID
            {
                if (!User.IsInRole("admin")) // Проверяем, является ли текущий пользователь администратором
                {
                    return Forbid(); // Запрещаем доступ
                }

                userToUpdate = await _userManager.FindByIdAsync(userId);
                if (userToUpdate == null)
                {
                    return NotFound(); // Если пользователь не найден
                }
            }

            // Пополнение бюджета
            userToUpdate.Budget += model.Amount;

            await _userManager.UpdateAsync(userToUpdate); // Обновляем пользователя в базе данных

            return userId == null ? RedirectToAction("Details") : RedirectToAction("Details", new { id = userToUpdate.Id });

        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CancelTicket(int ticketId, string? userId = null)
        {
            if (userId != null && !User.IsInRole("admin"))
            {
                return Forbid(); // Запрещаем доступ
            }

            var user = userId == null ? await _userManager.GetUserAsync(User) : await _userManager.FindByIdAsync(userId);
            var bookedSeat = await _context.BookedSeats.FindAsync(ticketId);

            if (bookedSeat == null || bookedSeat.UserId != user.Id)
            {
                return NotFound();
            }

            // Увеличиваем бюджет пользователя
            user.Budget += bookedSeat.PriceBooked;

            _context.BookedSeats.Remove(bookedSeat); // Удаляем билет
            await _context.SaveChangesAsync(); // Сохраняем изменения

            if(User.IsInRole("admin"))
            {
                return RedirectToAction("Details", new { id = user.Id }); // Перенаправляем на страницу деталей пользователя
            }
            else
            {
                return RedirectToAction("Details"); // Перенаправляем на страницу деталей пользователя
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            // удаляем аутентификационные куки
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}