using AutoMapper;
using Fragrance_Web_App.Data.Models;
using Fragrance_Web_App.Models;
using Fragrance_Web_App.Repositories;

namespace Fragrance_Web_App.Services
{
    public class FragranceService(IFragranceRepository fragranceRespository, IMapper mapper) : IFragranceService
    {
        public async Task<FragranceDto> CreateFragrance(FragranceCreateRequest request)
        {
            var fragrance = mapper.Map<FragranceCreateRequest, Fragrance>(request);
            fragrance.Id = Guid.NewGuid().ToString();

            fragrance.FragranceNotes.ToList().ForEach(fn => fn.FragranceId = fragrance.Id);

            return await fragranceRespository.Create(fragrance);
        }

        public async Task<IEnumerable<CategoryDto>> GetFragranceCategories()
        {
            return await fragranceRespository.GetFragranceCategories();
        }
    }
}
