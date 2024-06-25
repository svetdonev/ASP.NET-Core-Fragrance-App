using System.ComponentModel.DataAnnotations;
using static Fragrance_Web_App.Data.Constants;

namespace Fragrance_Web_App.Data.Models
{
    public class Fragrance
    {
        public string Id { get; set; }

        [Required]
        [MaxLength(FragranceNameMaxLength)]
        public string Name { get; set; }

        [Required]
        public int Year { get; set; }

        [Required]
        [MaxLength(FragranceDescriptionMaxLength)]
        public string Description { get; set; }

        [Required]
        [MaxLength(FragranceTypeMaxLength)]
        public string Type { get; set; }

        [Required]
        public string ImageUrl { get; set; }
        public string UserId { get; set; }
        public User User { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }
        public ICollection<FragranceNote> FragranceNotes { get; set; } = new List<FragranceNote>();
        public ICollection<Review> Reviews { get; set; } = new List<Review>();
    }
}
