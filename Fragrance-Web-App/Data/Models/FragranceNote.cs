namespace Fragrance_Web_App.Data.Models
{
    public class FragranceNote
    {
        public string FragranceId { get; set; }
        public Fragrance Fragrance { get; set; }
        public int NoteId { get; set; }
        public Note Note { get; set; }
    }
}
