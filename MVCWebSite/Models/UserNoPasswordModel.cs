using MVCWebSite.Enums;
using System.ComponentModel.DataAnnotations;

namespace MVCWebSite.Models
{
    public class UserNoPasswordModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Enter the user name")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Enter the contact login")]
        public string Login { get; set; }
        [Required(ErrorMessage = "Enter the contact email")]
        [EmailAddress(ErrorMessage = "Email is not valid")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Choose the user profile")]
        public ProfileEnum? Profile { get; set; }
    }
}
