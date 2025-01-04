using System.ComponentModel.DataAnnotations;

namespace MVCWebSite.Models
{
    public class ResetMyPasswordModel
    {
        [Required(ErrorMessage = "Enter the login")]
        public string Login { get; set; }
        [Required(ErrorMessage = "Enter the email")]
        public string Email { get; set; }
    }
}
