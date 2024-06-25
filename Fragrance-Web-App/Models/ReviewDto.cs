namespace Fragrance_Web_App.Models
{
    public class ReviewDto
    {
        public string Id { get; set; }
        public string Content { get; set; }
        public string CreatedOn { get; set; }
        public string AuthorId { get; set; }
        public string AuthorName { get; set; }
        public string AuthorAvatarUrl { get; set; }
        public string FragranceId { get; set; }
    }
}
