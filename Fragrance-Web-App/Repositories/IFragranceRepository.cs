using Fragrance_Web_App.Data.Models;
using Fragrance_Web_App.Models;

namespace Fragrance_Web_App.Repositories
{
    public interface IFragranceRepository
    {
        Task<FragranceDto> Create(Fragrance fragrance);
        Task<IEnumerable<CategoryDto>> GetFragranceCategories();
    }
}
