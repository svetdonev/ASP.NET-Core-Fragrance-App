using Fragrance_Web_App.Data.Models;

namespace Fragrance_Web_App.Repositories
{
    public interface IUserRepository
    {
        Task<User> GetUserByIdAsync(string id);
        Task<User> GetUserByUsernameAsync(string username);
        Task<bool> UpdateUserAsync(User user);
    }
}
