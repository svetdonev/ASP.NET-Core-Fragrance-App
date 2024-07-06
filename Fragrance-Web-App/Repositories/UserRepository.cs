using Fragrance_Web_App.Data;
using Fragrance_Web_App.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Fragrance_Web_App.Repositories
{
    public class UserRepository(FragranceAppDbContext dbContext) : IUserRepository
    {
        public async Task<User> GetUserByIdAsync(string id)
        {
            return await dbContext.Users.FindAsync(id);
        }

        public async Task<User> GetUserByUsernameAsync(string username)
        {
            return await dbContext.Users.SingleOrDefaultAsync(u => u.UserName == username);
        }

        public async Task<bool> UpdateUserAsync(User user)
        {
            dbContext.Users.Update(user);
            return await dbContext.SaveChangesAsync() > 0;
        }
    }
}
