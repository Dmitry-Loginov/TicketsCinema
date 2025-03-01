using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using TicketsCinema.Models;

namespace TicketsCinema.Controllers;

public class HomeController : Controller
{
    ApplicationContext db;
    public HomeController(ApplicationContext context)
    {
        db = context;
    }

    public IActionResult Index()
    {
        var movies = db.Movies.Take(3).ToList(); // Берем 3 ближайших фильма
        return View(movies);
    }


    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
