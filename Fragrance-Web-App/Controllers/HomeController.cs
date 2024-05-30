using Fragrance_Web_App.Models;
using Fragrance_Web_App.Services;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Fragrance_Web_App.Controllers
{
    public class HomeController(IFragranceService fragranceService) : Controller
    {
        public IActionResult Index()
        {
            return View(new IndexViewModel
            {
                FragrancesCount = fragranceService.GetTotalFragrancesCount(),
                UsersCount = fragranceService.GetUsersCount()
            });
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
