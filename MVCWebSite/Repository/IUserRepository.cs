using MVCWebSite.Models;

namespace MVCWebSite.Repository
{
    public interface IUserRepository
    {
        UserModel ListByLogin(string login);
        UserModel GetByEmailAndLogin(string email, string login);
        List<UserModel> GetAll();
        UserModel ListById(int id);
        UserModel Add(UserModel user);
        UserModel Update(UserModel user);
        UserModel ChangePassword(ChangePasswordModel changePasswordModel);
        bool Delete(int id);      
    }
}
