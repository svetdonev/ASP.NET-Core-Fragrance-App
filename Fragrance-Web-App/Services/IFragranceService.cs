using Fragrance_Web_App.Models;

namespace Fragrance_Web_App.Services
{
    public interface IFragranceService
    {
        Task<FragranceDto> CreateFragrance(FragranceCreateRequest request);
        Task<IEnumerable<CategoryDto>> GetFragranceCategories();
    }
}
