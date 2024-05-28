using Fragrance_Web_App.Data.Models;
using Fragrance_Web_App.Models;

namespace Fragrance_Web_App.Repositories
{
    public interface IFragranceRepository
    {
        /// <summary>
        /// Adds a new fragrance in the database.
        /// </summary>
        /// <param name="fragrance">The fragrance details.</param>
        /// <returns>Returns the added fragrance.</returns>
        Task<FragranceDto> CreateFragrance(Fragrance fragrance);
        Task<IEnumerable<FragranceDto>> GetFragrances(FragranceQuery fragranceQuery);
        Task<IEnumerable<NoteDto>> GetNotes();
        Task<IEnumerable<CategoryDto>> GetFragranceCategories();
        Task<FragranceDto> FragranceDetails(string fragranceId);
        Task UpdateFragrance(string fragranceId, Fragrance fragrance);

    }
}
