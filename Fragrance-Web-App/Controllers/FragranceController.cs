using AutoMapper;
using Fragrance_Web_App.Models;
using Fragrance_Web_App.Repositories;
using Fragrance_Web_App.Services;
using Microsoft.AspNetCore.Mvc;

namespace Fragrance_Web_App.Controllers
{
    public class FragranceController(IFragranceService fragranceService, IMapper mapper) : Controller
    {
        public async Task<IActionResult> Index()
        {
            return View(new FragranceDto
            {
                Categories = await this.RetrieveFragranceCategories()
            });
        }

        private Task<IEnumerable<CategoryDto>> RetrieveFragranceCategories()
        {
            return fragranceService.GetFragranceCategories();
        }
    }
}
