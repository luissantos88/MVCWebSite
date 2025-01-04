using System.ComponentModel.DataAnnotations;

namespace MVCWebSite.Models
{
    public class LoginModel
    {
        [Required(ErrorMessage = "Enter the login")]
        public string Login { get; set; }
        [Required(ErrorMessage = "Enter the password")]
        public string Password { get; set; }
    }
}
