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
        /// <inheritdoc />
        public async Task<FragranceDto> CreateFragrance(Fragrance fragrance)
        {
            await dbContext.Fragrances.AddAsync(fragrance);
            await dbContext.SaveChangesAsync();

            return new FragranceDto();
        }

        public async Task<IEnumerable<CategoryDto>> GetFragranceCategories()
        {
            var categories = await dbContext.Categories.AsNoTracking().ToListAsync();
            var categoryDtos = mapper.Map<List<CategoryDto>>(categories);

            return categoryDtos;
        }

        public async Task<IEnumerable<FragranceDto>> GetFragrances(FragranceQuery fragranceQuery)
        {
            var fragrances = await dbContext.Fragrances
                .Include(f => f.Category)
                .Include(f => f.FragranceNotes)
                .ThenInclude(fn => fn.Note)
                .AsNoTracking()
                .FilterByCategoryId(fragranceQuery.CategoryId)
                .FilterBySearchTerm(fragranceQuery.SearchTerm)
                .OrderByPropertyName(fragranceQuery.OrderByClause?.PropertyName, fragranceQuery.OrderByClause?.Direction ?? OrderDirection.Desc)
                .ToListAsync();

            return mapper.Map<List<Fragrance>, List<FragranceDto>>(fragrances);
        }

        public async Task<IEnumerable<NoteDto>> GetNotes()
        {
            var notes = await dbContext.Notes.AsNoTracking().ToListAsync();
            var noteDtos = mapper.Map<List<NoteDto>>(notes);

            return noteDtos;
        }
        public async Task<FragranceDto> FragranceDetails(string fragranceId)
        {
            var fragrance = await dbContext.Fragrances
                .Include(f => f.Category)
                .Include(f => f.FragranceNotes)
                .ThenInclude(fn => fn.Note)
                .FirstOrDefaultAsync(f => f.Id == fragranceId);

            var fragranceDto = mapper.Map<FragranceDto>(fragrance);

            return fragranceDto;
        }
    }
}
