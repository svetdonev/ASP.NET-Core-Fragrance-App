using Fragrance_Web_App.Data.Models;

namespace Fragrance_Web_App.Models
{
    public class FragranceDto
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public int Year { get; set; }
        public string Description { get; set; }
        public string Type { get; set; }
        public string ImageUrl { get; set; }
        public CategoryDto Category { get; set; }
        public IEnumerable<int> NoteIds { get; set; }
        public IEnumerable<Note> Notes { get; init; }
    }
}
