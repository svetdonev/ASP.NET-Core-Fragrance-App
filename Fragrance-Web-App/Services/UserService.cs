using Fragrance_Web_App.Models;
using Fragrance_Web_App.Repositories;

namespace Fragrance_Web_App.Services
{
    public class UserService(IUserRepository userRepository) : IUserService
    {
        public async Task<ProfileViewModel> GetProfileAsync(string username)
        {
            var user = await userRepository.GetUserByUsernameAsync(username);
            if (user == null)
            {
                return null;
            }

            var model = new ProfileViewModel
            {
                Id = user.Id,
                UserName = user.UserName,
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                PhoneNumber = user.PhoneNumber,
                Avatar = user.Avatar,
                AboutMe = user.AboutMe
            };

            return model;
        }

        public async Task<bool> UpdateProfileAsync(ProfileViewModel model)
        {
            var user = await userRepository.GetUserByIdAsync(model.Id);

            if (user == null)
            {
                return false;
            }

            user.FirstName = model.FirstName;
            user.LastName = model.LastName;
            user.Email = model.Email;
            user.PhoneNumber = model.PhoneNumber;
            user.Avatar = model.Avatar;
            user.AboutMe = model.AboutMe;

            return await userRepository.UpdateUserAsync(user);
        }
    }
}
