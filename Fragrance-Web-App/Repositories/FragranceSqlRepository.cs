using AutoMapper;
using Fragrance_Web_App.Data;
using Fragrance_Web_App.Data.Models;
using Fragrance_Web_App.Models;
using Microsoft.EntityFrameworkCore;

namespace Fragrance_Web_App.Repositories
{
    public class FragranceSqlRepository(FragranceAppDbContext dbContext, IMapper mapper) : IFragranceRepository
    {
        public async Task<FragranceDto> Create(Fragrance fragrance)
        {
            await dbContext.Fragrances.AddAsync(fragrance);
            await dbContext.SaveChangesAsync();

            return new FragranceDto();
        }

        public async Task<IEnumerable<CategoryDto>> GetFragranceCategories()
        {
            var categories = await dbContext.Categories.ToListAsync();
            var categoryDtos = mapper.Map<IEnumerable<CategoryDto>>(categories);

            return categoryDtos;
        }
    }
}
