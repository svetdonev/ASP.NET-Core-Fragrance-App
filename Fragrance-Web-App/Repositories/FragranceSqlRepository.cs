using Fragrance_Web_App.Data;
using Fragrance_Web_App.Data.Models;

namespace Fragrance_Web_App.Repositories
{
    public class FragranceSqlRepository(FragranceAppDbContext dbContext) : IFragranceRepository
    {
        public async Task<FragranceDto> Create(Fragrance fragrance)
        {
            await dbContext.Fragrances.AddAsync(fragrance);
            await dbContext.SaveChangesAsync();

            return new FragranceDto();
        }
    }
}
