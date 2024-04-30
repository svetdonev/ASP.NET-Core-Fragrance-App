using Fragrance_Web_App.Models;

namespace Fragrance_Web_App.Services
{
    public interface IFragranceService
    {
        Task<FragranceDto> CreateFragrance(FragranceCreateRequest request);
        Task<IEnumerable<FragranceDto>> GetFragrances(FragranceQuery fragranceQuery);
        Task<IEnumerable<CategoryDto>> GetFragranceCategories();
        Task<IEnumerable<NoteDto>> GetNotes();
        Task<FragranceDto> FragranceDetails(string fragranceId);
        Task<FragranceDto> EditFragrance(string fragranceId, FragranceCreateRequest request);

    }
}
