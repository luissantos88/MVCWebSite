using System.ComponentModel.DataAnnotations;

namespace MVCWebSite.Models
{
    public class ChangePasswordModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Enter user current password")]
        public string CurrentPassword { get; set; }
        [Required(ErrorMessage = "Enter user new password")]
        public string NewPassword { get; set; }
        [Required(ErrorMessage = "Confirm user new password")]
        [Compare("NewPassword", ErrorMessage = "New password doesn't match match the confirm password")]
        public string ConfirmPassword { get; set; }
    }
}
