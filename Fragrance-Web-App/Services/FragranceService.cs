using AutoMapper;
using Fragrance_Web_App.Data.Models;
using Fragrance_Web_App.Models;
using Fragrance_Web_App.Repositories;

namespace Fragrance_Web_App.Services
{
    public class FragranceService(IFragranceRepository fragranceRespository, IMapper mapper) : IFragranceService
    {
        public Task<FragranceDto> CreateFragrance(FragranceCreateRequest request)
        {
            var fragrance = mapper.Map<FragranceCreateRequest, Fragrance>(request);
            fragrance.Id = Guid.NewGuid().ToString();

            fragrance.FragranceNotes.ToList().ForEach(fn => fn.FragranceId = fragrance.Id);

            return fragranceRespository.CreateFragrance(fragrance);
        }

        public Task<IEnumerable<CategoryDto>> GetFragranceCategories()
        {
            return fragranceRespository.GetFragranceCategories();
        }

        public Task<IEnumerable<FragranceDto>> GetFragrances(FragranceQuery fragranceQuery)
        {
            return fragranceRespository.GetFragrances(fragranceQuery);
        }

        public async Task<IEnumerable<NoteDto>> GetNotes()
        {
            return await fragranceRespository.GetNotes();
        }
        public async Task<FragranceDto> FragranceDetails(string fragranceId)
        {
            return await fragranceRespository.FragranceDetails(fragranceId);
        }

        public async Task<FragranceDto> EditFragrance(string fragranceId, FragranceCreateRequest request)
        {
            var existingFragrance = await fragranceRespository.FragranceDetails(fragranceId);

            if (existingFragrance == null)
            {
                return null;
            }

            return await fragranceRespository.EditFragrance(fragranceId, request);
        }
    }
}
