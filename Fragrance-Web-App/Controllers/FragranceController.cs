using AutoMapper;
using Fragrance_Web_App.Models;
using Fragrance_Web_App.Services;
using Microsoft.AspNetCore.Mvc;

namespace Fragrance_Web_App.Controllers
{
    public class FragranceController(IFragranceService fragranceService, IMapper mapper) : Controller
    { 
        public async Task<IActionResult> Add()
        {
            return View(new FragranceDto
            {
                Categories = await fragranceService.GetFragranceCategories()
            });
        }

        [HttpPost]
        public async Task<IActionResult> Add(FragranceCreateRequest fragrance)
        {
            var categories = (await fragranceService.GetFragranceCategories()).ToList()
                ;
            if (!categories.Any(f => f.Id == fragrance.CategoryId))
            {
                this.ModelState.AddModelError(nameof(fragrance.CategoryId), "Category does not exist!");
            }

            if (!ModelState.IsValid)
            {
                fragrance.Categories = categories;

                return View(fragrance);
            }

            await fragranceService.CreateFragrance(fragrance);
            return RedirectToAction("All");
        }
    }
}