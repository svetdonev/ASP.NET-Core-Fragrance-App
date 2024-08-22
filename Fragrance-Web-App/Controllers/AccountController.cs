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
        public async Task<IActionResult> ChangeAvatar(string userId, string avatarImageUrl)
        {
            if (ModelState.IsValid)
            {
                await _userService.UpdateUserAvatarAsync(userId, avatarImageUrl);
                TempData["SuccessMessage"] = "Avatar updated successfully!";
            }
            return RedirectToAction("Profile", new { userId });
        }
    }
}
