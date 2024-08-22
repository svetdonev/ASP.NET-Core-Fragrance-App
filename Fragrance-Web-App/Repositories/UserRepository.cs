using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Fragrance_Web_App.Data;
using Fragrance_Web_App.Models;
using Fragrance_Web_App.Data.Models;
using Microsoft.AspNetCore.Identity;

namespace Fragrance_Web_App.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly FragranceAppDbContext _context;
        private readonly UserManager<User> _userManager;

        public UserRepository(FragranceAppDbContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<User> GetUserByUsernameAsync(string username)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.UserName == username);
        }

        public async Task<User> GetUserByIdAsync(string id)
        {
            return await _context.Users.FindAsync(id);
        }

        public async Task<bool> UpdateUserAsync(User user)
        {
            _context.Users.Update(user);
            return await _context.SaveChangesAsync() > 0;
        }
        public async Task UpdateUserAvatarAsync(string userId, string avatarImageUrl)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user != null)
            {
                user.Avatar = avatarImageUrl;
                await _userManager.UpdateAsync(user);
            }
        }
    }
}
