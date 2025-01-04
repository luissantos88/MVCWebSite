using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.Metrics;
using System.Xml.Linq;

namespace MVCWebSite.Models
{
    public class ContactModel
    {
        public int Id { get; set; } // contact unique code
        [Required(ErrorMessage = "Enter the contact name")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Enter the contact email")]
        [EmailAddress(ErrorMessage = "Email is not valid")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Enter the contact phone.")]
        [Phone(ErrorMessage = "Phone number is not valid")]
        public string Phone { get; set; }
        [DisplayName("UserId")]
        public int? UserId { get; set; }
        [DisplayName("User")]
        public UserModel? User { get; set; }
    }
}
