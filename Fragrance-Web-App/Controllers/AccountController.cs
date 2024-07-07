using Fragrance_Web_App.Models;
using Fragrance_Web_App.Services;
using Microsoft.AspNetCore.Mvc;

namespace Fragrance_Web_App.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUserService _userService;

        public AccountController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public async Task<IActionResult> Profile()
        {
            var model = await _userService.GetProfileAsync(User.Identity.Name);
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
                var result = await _userService.UpdateProfileAsync(model);
                if (result)
                {
                    // Optionally add a success message
                    return RedirectToAction("Profile");
                }

                ModelState.AddModelError(string.Empty, "An error occurred while updating the profile.");
            }

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ChangeAvatar(ProfileViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _userService.UpdateAvatarAsync(User.Identity.Name, model.Avatar);
                if (result)
                {
                    // Optionally add a success message
                    return RedirectToAction("Profile");
                }

                ModelState.AddModelError(string.Empty, "An error occurred while updating the avatar.");
            }

            return View("Profile", model); // Return to the profile view with validation errors if any
        }
    }
}
