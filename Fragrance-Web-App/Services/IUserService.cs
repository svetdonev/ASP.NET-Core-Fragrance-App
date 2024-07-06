using Fragrance_Web_App.Models;

namespace Fragrance_Web_App.Services
{
    public interface IUserService
    {
        Task<ProfileViewModel> GetProfileAsync(string username);
        Task<bool> UpdateProfileAsync(ProfileViewModel model);
    }
}
