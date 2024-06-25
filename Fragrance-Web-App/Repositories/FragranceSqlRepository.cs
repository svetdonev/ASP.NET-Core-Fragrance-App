using AutoMapper;
using Fragrance_Web_App.Data;
using Fragrance_Web_App.Data.Models;
using Fragrance_Web_App.Extensions;
using Fragrance_Web_App.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Fragrance_Web_App.Repositories
{
    public class FragranceSqlRepository(FragranceAppDbContext dbContext, IMapper mapper, UserManager<User> userManager) : IFragranceRepository
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
            var query = dbContext.Fragrances
                .Include(f => f.Category)
                .Include(f => f.FragranceNotes)
                .ThenInclude(fn => fn.Note)
                .AsQueryable();

            if (fragranceQuery.CategoryId.HasValue)
            {
                query = query.Where(f => f.CategoryId == fragranceQuery.CategoryId.Value);
            }

            if (!string.IsNullOrEmpty(fragranceQuery.SearchTerm))
            {
                query = query.Where(f => f.Name.Contains(fragranceQuery.SearchTerm));
            }

            if (fragranceQuery.OrderByClause != null)
            {
                query = query.OrderByPropertyName(fragranceQuery.OrderByClause.PropertyName, fragranceQuery.OrderByClause.Direction);
            }

            fragranceQuery.TotalFragrances = await query.CountAsync();

            query = query
                .Skip((fragranceQuery.CurrentPage - 1) * FragranceQuery.MoviesPerPage)
                .Take(FragranceQuery.MoviesPerPage);

            var fragrances = await query.AsNoTracking().ToListAsync();

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

        public async Task UpdateFragrance(string fragranceId, Fragrance fragrance)
        {
            var existingFragrance = await dbContext.Fragrances
                .Include(f => f.Category)
                .Include(f => f.FragranceNotes)
                .ThenInclude(fn => fn.Note)
                .FirstOrDefaultAsync(f => f.Id == fragranceId);

            existingFragrance.Name = fragrance.Name;
            existingFragrance.Year = fragrance.Year;
            existingFragrance.Description = fragrance.Description;
            existingFragrance.Type = fragrance.Type;
            existingFragrance.ImageUrl = fragrance.ImageUrl;
            existingFragrance.Category = fragrance.Category;
            existingFragrance.FragranceNotes = fragrance.FragranceNotes;

            await dbContext.SaveChangesAsync();
        }

        public async Task DeleteFragrance(string fragranceId)
        {
            var existingFragrance = await dbContext.Fragrances
                .Include(f => f.Category)
                .Include(f => f.FragranceNotes)
                .ThenInclude(fn => fn.Note)
                .FirstOrDefaultAsync(f => f.Id == fragranceId);

            if (existingFragrance != null)
            {
                dbContext.Fragrances.Remove(existingFragrance);
                await dbContext.SaveChangesAsync();
            }
        }
        public int GetTotalFragrancesCount()
        {
            return dbContext.Fragrances.Count();
        }

        public int GetUsersCount()
        {
            return dbContext.Users.Count();
        }

        public async Task<List<FragranceListingViewModel>> GetLatestFragrances()
        {
            return await dbContext.Fragrances
                .OrderByDescending(f => f.Id)
                .Select(f => new FragranceListingViewModel
                {
                    Id = f.Id,
                    Name = f.Name,
                    ImageUrl = f.ImageUrl,
                    Year = f.Year,
                    Type = f.Type,
                    Category = f.Category.Name
                })
                .Take(8)
                .ToListAsync();
        }

        public async Task CreateReview(FragranceDto fragranceModel, ClaimsPrincipal userPrincipal)
        {
            var fragrance = await dbContext.Fragrances.FindAsync(fragranceModel.Id);
            var user = await userManager.GetUserAsync(userPrincipal);

            if (user == null)
            {
                throw new UnauthorizedAccessException("User is not authorized to create a review.");
            }

            if (string.IsNullOrWhiteSpace(fragranceModel.ReviewContent) || fragrance == null)
            {
                throw new ArgumentException("Something is broken!");
            }

            var review = new Review
            {
                Id = Guid.NewGuid().ToString(),
                Content = fragranceModel.ReviewContent,
                CreatedOn = DateTime.UtcNow.ToLocalTime(),
                AuthorId = user.Id,
                FragranceId = fragranceModel.Id
            };

            dbContext.Reviews.Add(review);
            await dbContext.SaveChangesAsync();
        }
    }
}
