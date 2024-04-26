using AutoMapper;
using Fragrance_Web_App.Data;
using Fragrance_Web_App.Data.Models;
using Fragrance_Web_App.Extensions;
using Fragrance_Web_App.Models;
using Microsoft.EntityFrameworkCore;

namespace Fragrance_Web_App.Repositories
{
    public class FragranceSqlRepository(FragranceAppDbContext dbContext, IMapper mapper) : IFragranceRepository
    {
        public async Task<FragranceDto> CreateFragrance(Fragrance fragrance)
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

        public async Task<IEnumerable<FragranceDto>> GetFragrances(FragranceQuery fragranceQuery)
        {
            var fragrances = await dbContext.Fragrances
                .AsNoTracking()
                .FilterByCategoryId(fragranceQuery.CategoryId)
                .FilterBySearchTerm(fragranceQuery.SearchTerm)
                .OrderByPropertyName(fragranceQuery.OrderByClause?.PropertyName, fragranceQuery.OrderByClause?.Direction ?? OrderDirection.Desc)
                .ToListAsync();

            return mapper.Map<IEnumerable<Fragrance>, IEnumerable<FragranceDto>>(fragrances);
        }

        public async Task<IEnumerable<NoteDto>> GetNotes()
        {
            var notes = await dbContext.Notes.AsNoTracking().ToListAsync();
            var noteDtos = mapper.Map<List<NoteDto>>(notes);

            return noteDtos;
        }
    }
}
