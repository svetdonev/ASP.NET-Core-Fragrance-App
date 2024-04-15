namespace Fragrance_Web_App.Data.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<Fragrance> Fragrances { get; set; } = new List<Fragrance>();
    }
}
