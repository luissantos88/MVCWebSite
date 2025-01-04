using MVCWebSite.Enums;
using MVCWebSite.Helper;
using System.ComponentModel.DataAnnotations;

namespace MVCWebSite.Models
{
    public class UserModel
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
        [Required(ErrorMessage = "Enter the user password")]
        public string Password { get; set; }
        public DateTime RegistrationDate { get; set; }
        public DateTime? UpdateTime { get; set; }
        public virtual List<ContactModel> Contacts { get; set; }

        public bool ValidPassword(string password)
        {
           return Password == password.HashGenerator();
        }
        public void SetHashPassword()
        {
            Password = Password.HashGenerator();
        }

        public void SetNewPassword(string newPassword)
        {
            Password = newPassword.HashGenerator();
        }
        public string GenerateNewPassword()
        {
            string newPassword = Guid.NewGuid().ToString().Substring(0, 8);
            Password = newPassword.HashGenerator();
            return newPassword;
        }
    }
}

