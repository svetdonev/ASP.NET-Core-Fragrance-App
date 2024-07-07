using Fragrance_Web_App.Data.Models;

namespace Fragrance_Web_App.Repositories
{
    public interface IUserRepository
    {
        Task<User> GetUserByUsernameAsync(string username);
        Task<User> GetUserByIdAsync(string id);
        Task<bool> UpdateUserAsync(User user);
    }
}
