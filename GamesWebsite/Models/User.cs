using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace GamesWebsite.Models
{
    public class User
    {
        public int? userId { get; set; }
        [Required, MinLength(4, ErrorMessage = "Username should have a minimum of 4 characters")]
        public string? username { get; set; }
        [Required, EmailAddress]
        public string? email { get; set; }
        [PasswordPropertyText, Required, MinLength(7, ErrorMessage = "Password must be longer than 7 characters")]
        public string? password { get; set; }
        [Required(ErrorMessage = "Date of Birth is required")]
        public DateTime dateOfBirth { get; set; }
        public string? level { get; set; }
        public bool? isAdmin { get; set; }
        public string? passwordSalt { get; set; }
        public User()
        {
        }
        public string Role()
        {
            if (this.isAdmin == true)
                return "Admin";
            return string.Empty;
        }
    }
}
