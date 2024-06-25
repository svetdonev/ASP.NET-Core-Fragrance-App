using Fragrance_Web_App.Models;
using System.Security.Claims;

namespace Fragrance_Web_App.Services
{
    public interface IFragranceService
    {
        Task<FragranceDto> CreateFragrance(FragranceCreateRequest request);
        Task<IEnumerable<FragranceDto>> GetFragrances(FragranceQuery fragranceQuery);
        Task<IEnumerable<CategoryDto>> GetFragranceCategories();
        Task<IEnumerable<NoteDto>> GetNotes();
        Task<FragranceDto> FragranceDetails(string fragranceId);
        Task UpdateFragrance(string fragranceId, FragranceUpdateRequest fragrance);
        Task DeleteFragrance(string fragranceId);
        int GetTotalFragrancesCount();
        int GetUsersCount();
        Task<List<FragranceListingViewModel>> GetLatestFragrances();
        Task CreateReview(FragranceDto fragranceModel, ClaimsPrincipal userPrincipal);

    }
}
