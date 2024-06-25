using System.ComponentModel.DataAnnotations;

namespace Fragrance_Web_App.Data.Models
{
    public class Review
    {
        [Key]
        [Required]
        public string Id { get; set; }

        [Required]
        public string Content { get; set; }
        public DateTime CreatedOn { get; set; }
        public string AuthorId { get; set; }
        public User Author { get; set; }

        [Required]
        public string FragranceId { get; set; }
        public Fragrance Fragrance { get; set; }
        public ICollection<UserReview> UserReviews { get; set; }
    }
}
