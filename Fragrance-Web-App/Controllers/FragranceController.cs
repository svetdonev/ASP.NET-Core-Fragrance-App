using AutoMapper;
using Fragrance_Web_App.Data.Models;
using Fragrance_Web_App.Models;
using Fragrance_Web_App.Services;
using Microsoft.AspNetCore.Mvc;

namespace Fragrance_Web_App.Controllers
{
    public class FragranceController(IFragranceService fragranceService, IMapper mapper) : Controller
    {
        [HttpGet]
        public async Task<IActionResult> Add()
        {
            return View(new FragranceCreateRequest
            {
                Categories = await fragranceService.GetFragranceCategories(),
                Notes = await fragranceService.GetNotes()
            });
        }

        [HttpPost]
        public async Task<IActionResult> Add(FragranceCreateRequest fragrance)
        {
            var categories = (await fragranceService.GetFragranceCategories()).ToList();

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

        [HttpGet]
        public async Task<IActionResult> All([FromQuery]FragranceQuery query)
        {
            var fragrances = await fragranceService.GetFragrances(query);
            var categories = await fragranceService.GetFragranceCategories();

            query.Categories = categories;
            query.Fragrances = fragrances;

            return View(query);
        }

        [HttpGet]
        public async Task<IActionResult> Details(string fragranceId)
        {
            var fragrance = await fragranceService.FragranceDetails(fragranceId);

            if(fragrance == null)
            {
                return NotFound();
            }

            return View(fragrance);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(string fragranceId)
        {
            var fragrance = await fragranceService.FragranceDetails(fragranceId);

            if (fragrance == null)
            {
                return NotFound();
            }

            return View(fragrance);
        }

        [HttpPut]
        public async Task<IActionResult> Edit(string fragranceId, FragranceUpdateRequest request)
        {
            var fragrance = await fragranceService.FragranceDetails(fragranceId);

            if(!ModelState.IsValid)
            {
                return View(fragrance);
            }

            await fragranceService.UpdateFragrance(fragranceId, request);

            return Redirect($"/Fragrance/Details?fragranceId={fragrance.Id}");
        }

        [HttpGet]
        public async Task<IActionResult> Delete(string fragranceId)
        {
            var fragrance = await fragranceService.FragranceDetails(fragranceId);

            if (fragrance == null)
            {
                return NotFound();
            }

            return View(fragrance);
        }

        [HttpPost]
        [ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(string fragranceId)
        {
            var fragrance = await fragranceService.FragranceDetails(fragranceId);

            if (!ModelState.IsValid)
            {
                return View(fragrance);
            }

            await fragranceService.DeleteFragrance(fragranceId);

            return RedirectToAction(nameof(All));
        }
    }
}