namespace Fragrance_Web_App.Data.Models
{
    public class Note
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Color { get; set; }
        public ICollection<FragranceNote> FragranceNotes { get; set; } = new List<FragranceNote>();
    }
}
