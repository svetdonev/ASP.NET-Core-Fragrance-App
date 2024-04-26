using Fragrance_Web_App.Data.Models;
using Fragrance_Web_App.Models;

namespace Fragrance_Web_App.Repositories
{
    public interface IFragranceRepository
    {
        Task<FragranceDto> CreateFragrance(Fragrance fragrance);
        Task<IEnumerable<FragranceDto>> GetFragrances(FragranceQuery fragranceQuery);
        Task<IEnumerable<NoteDto>> GetNotes();
        Task<IEnumerable<CategoryDto>> GetFragranceCategories();
    }
}
