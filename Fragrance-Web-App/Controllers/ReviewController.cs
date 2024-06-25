using Fragrance_Web_App.Models;
using Fragrance_Web_App.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Fragrance_Web_App.Controllers
{
    public class ReviewController(IFragranceService fragranceService) : Controller
    {
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create(FragranceDto fragranceModel)
        {
            var userPrincipal = User;

            try
            {
                await fragranceService.CreateReview(fragranceModel, userPrincipal);
                return Redirect($"/Fragrance/Details?fragranceId={fragranceModel.Id}");
            }
            catch (UnauthorizedAccessException)
            {
                return Unauthorized();
            }
            catch
            {
                return BadRequest();
            }
        }
    }
}
