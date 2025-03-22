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
                UserName = user.UserName,
                Budget = user.Budget,
                PastTickets = pastTickets,
                UpcomingTickets = upcomingTickets
            };

            return View(model);
        }

        [Authorize]
        [HttpGet]
        public IActionResult AddBudget()
        {
            return View(new AddBudgetViewModel());
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddBudget(AddBudgetViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.GetUserAsync(User);
                user.Budget += model.Amount; // Увеличиваем бюджет

                _context.Users.Update(user);
                await _context.SaveChangesAsync(); // Сохраняем изменения

                return RedirectToAction("Details", new { id = user.Id }); // Перенаправляем на страницу деталей пользователя
            }

            return View(model);
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