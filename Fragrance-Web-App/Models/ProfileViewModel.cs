using System.ComponentModel.DataAnnotations;

namespace Fragrance_Web_App.Models
{
    public class ProfileViewModel
    {
        public string Id { get; set; }
        public string Avatar { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string Gender { get; set; }
        public string AboutMe { get; set; }
        public DateTime RegisteredOn { get; set; }

    }
}
