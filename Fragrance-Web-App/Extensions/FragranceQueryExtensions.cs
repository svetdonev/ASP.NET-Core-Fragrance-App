using Fragrance_Web_App.Data.Models;
using Fragrance_Web_App.Models;

namespace Fragrance_Web_App.Extensions
{
    public static class FragranceQueryExtensions
    {
        public static IQueryable<Fragrance> FilterByCategoryId(this IQueryable<Fragrance> query, int? categoryId)
        {
            if (categoryId.HasValue)
            {
                query = query.Where(f => f.CategoryId == categoryId);
            }

            return query;
        }

        public static IQueryable<Fragrance> FilterBySearchTerm(this IQueryable<Fragrance> query, string searchTerm)
        {
            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                searchTerm = searchTerm.ToLower();
                query = query.Where(f => f.Name.ToLower() == searchTerm);
            }

            return query;
        }

        public static IQueryable<Fragrance> OrderByPropertyName(this IQueryable<Fragrance> query, string  propertyName, OrderDirection direction)
        {
            propertyName = propertyName?.ToLowerInvariant();
            switch (propertyName)
            {
                case "year":
                    return direction == OrderDirection.Asc ? query.OrderBy(f => f.Year) : query.OrderByDescending(f => f.Year);
                case "name":
                    return direction == OrderDirection.Desc ? query.OrderBy(f => f.Name) : query.OrderByDescending(f => f.Name);
                default:
                    return query.OrderByDescending(f => f.Name);
            }
        }
    }
}
