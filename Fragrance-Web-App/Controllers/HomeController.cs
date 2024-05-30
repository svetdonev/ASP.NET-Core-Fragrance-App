using Fragrance_Web_App.Models;
using Fragrance_Web_App.Services;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Fragrance_Web_App.Controllers
{
    public class HomeController(IFragranceService fragranceService) : Controller
    {
        public async Task<IActionResult> Index()
        {
            var totalFragrances = fragranceService.GetTotalFragrancesCount();
            var totalUsers = fragranceService.GetUsersCount();
            var fragrances = await fragranceService.GetLatestFragrances();

            return View(new IndexViewModel
            {
                FragrancesCount = totalFragrances,
                UsersCount = totalUsers,
                Fragrances = fragrances
            });
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
