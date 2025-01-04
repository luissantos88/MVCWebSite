using Microsoft.EntityFrameworkCore;
using MVCWebSite.Data;
using MVCWebSite.Models;

namespace MVCWebSite.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly BankContext _bankContext;
        public UserRepository(BankContext bankContext)
        {
            _bankContext = bankContext;
        }

        public UserModel ListByLogin(string login)
        {
            return _bankContext.Users.FirstOrDefault(x => x.Login.ToUpper() == login.ToUpper());
        }
       
        public UserModel GetByEmailAndLogin(string email, string login)
        {
            return _bankContext.Users.FirstOrDefault(x => x.Email.ToUpper() == email.ToUpper() && x.Login.ToUpper() == login.ToUpper());
        }

        public UserModel ListById(int id)
        {   
            return _bankContext.Users.FirstOrDefault(x => x.Id == id);
        }

        public List<UserModel> GetAll()
        {
            return _bankContext.Users
                .Include(x => x.Contacts)
                .ToList();
        }

        public UserModel Add(UserModel user)
        {
            user.RegistrationDate = DateTime.Now;
            user.SetHashPassword();
            _bankContext.Users.Add(user);
            _bankContext.SaveChanges();
            return user;
        }
        public UserModel Update(UserModel user)
        {
            UserModel userDB = ListById(user.Id);
            if(userDB == null)
            {
                throw new Exception("Something went error updating this contact");
            }
            userDB.Name = user.Name;
            userDB.Email = user.Email;
            userDB.Login = user.Login;
            userDB.Profile = user.Profile;
            userDB.UpdateTime = DateTime.Now;
                       
            _bankContext.Users.Update(userDB);
            _bankContext.SaveChanges();
            return userDB;
        }

        public UserModel ChangePassword(ChangePasswordModel changePasswordModel)
        {
            UserModel userDB = ListById(changePasswordModel.Id);

            if (userDB == null) throw new Exception("There was an error updating password, user not found");

            if (!userDB.ValidPassword(changePasswordModel.CurrentPassword)) throw new Exception("Current password don't match");

            if (userDB.ValidPassword(changePasswordModel.NewPassword)) throw new Exception("New password must be diferent from current password");

            userDB.SetNewPassword(changePasswordModel.NewPassword);
            userDB.UpdateTime = DateTime.Now;

            _bankContext.Users.Update(userDB);
            _bankContext.SaveChanges();

            return userDB;
        }


        public bool Delete(int id)
        {
            UserModel userDB = ListById(id);
            if (userDB == null)
            {
                throw new Exception("Something went error deleting this contact");
            }

            _bankContext.Users.Remove(userDB);
            _bankContext.SaveChanges();

            return true;
        }
    }
}
