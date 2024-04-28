namespace Fragrance_Web_App.Models
{
    public class FragranceQuery
    {
        public int? CategoryId { get; set; }
        public string SearchTerm { get; set; }
        public OrderBy OrderByClause { get; set; }
        public IEnumerable<CategoryDto> Categories { get; set; }
        public IEnumerable<FragranceDto> Fragrances { get; set; }
    }
}
