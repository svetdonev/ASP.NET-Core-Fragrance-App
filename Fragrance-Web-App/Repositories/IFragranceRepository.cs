using Fragrance_Web_App.Data.Models;

namespace Fragrance_Web_App.Repositories
{
    public interface IFragranceRepository
    {
        Task<FragranceDto> Create(Fragrance fragrance);
    }
}
