using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace WebApp.Models
{
    public class UserForgotPass
    {
        [Required, EmailAddress]
        public string? email { get; set; }
        [PasswordPropertyText, Required, MinLength(7, ErrorMessage = "Password must be longer than 7 characters")]
        public string? password1 { get; set; }
        [PasswordPropertyText, Required, MinLength(7, ErrorMessage = "Password must be longer than 7 characters")]
        public string? password2 { get; set; }
        public UserForgotPass()
        {
        }
    }
}
