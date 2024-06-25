using Microsoft.AspNetCore.Identity;

namespace Fragrance_Web_App.Data.Models
{
    public class User : IdentityUser
    {
        public string Avatar { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Gender { get; set; }
        public DateTime RegisteredOn { get; init; } = DateTime.UtcNow;
        public string AboutMe { get; set; }
        public ICollection<Fragrance> Fragrances { get; set; }
        public ICollection<Review> Reviews { get; set; }
        public ICollection<UserReview> UserReviews { get; set; }
    }
}
