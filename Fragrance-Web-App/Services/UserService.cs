using Fragrance_Web_App.Models;
using Fragrance_Web_App.Repositories;

namespace Fragrance_Web_App.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<ProfileViewModel> GetProfileAsync(string username)
        {
            var user = await _userRepository.GetUserByUsernameAsync(username);
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
                AboutMe = user.AboutMe,
                Gender = user.Gender,
                RegisteredOn = user.RegisteredOn
            };

            return model;
        }

        public async Task<bool> UpdateProfileAsync(ProfileViewModel model)
        {
            var user = await _userRepository.GetUserByIdAsync(model.Id);

            if (user == null)
            {
                return false;
            }

            user.FirstName = model.FirstName;
            user.LastName = model.LastName;
            user.Email = model.Email;
            user.PhoneNumber = model.PhoneNumber;
            user.Gender = model.Gender;
            user.AboutMe = model.AboutMe;

            return await _userRepository.UpdateUserAsync(user);
        }

        public async Task<bool> UpdateAvatarAsync(string username, string avatarUrl)
        {
            var user = await _userRepository.GetUserByUsernameAsync(username);
            if (user == null)
            {
                return false;
            }

            user.Avatar = avatarUrl;
            return await _userRepository.UpdateUserAsync(user);
        }
    }
}
