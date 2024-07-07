using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Fragrance_Web_App.Data;
using Fragrance_Web_App.Models;
using Fragrance_Web_App.Data.Models;

namespace Fragrance_Web_App.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly FragranceAppDbContext _context;

        public UserRepository(FragranceAppDbContext context)
        {
            _context = context;
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
    }
}
