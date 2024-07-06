using Fragrance_Web_App.Models;
using Fragrance_Web_App.Services;
using Microsoft.AspNetCore.Mvc;

namespace Fragrance_Web_App.Controllers
{
    public class AccountController(IUserService userService) : Controller
    {
        [HttpGet]
        public async Task<IActionResult> Profile()
        {
            var model = await userService.GetProfileAsync(User.Identity.Name);
            if (model == null)
            {
                return NotFound();
            }

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Profile(ProfileViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await userService.UpdateProfileAsync(model);
                if (result)
                {
                    // Optionally add a success message
                    return RedirectToAction("Profile");
                }

                ModelState.AddModelError(string.Empty, "An error occurred while updating the profile.");
            }

            return View(model);
        }
    }
}
