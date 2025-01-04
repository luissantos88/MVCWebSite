using MVCWebSite.Models;

namespace MVCWebSite.Helper
{
    public interface IUserSession
    {
        void CreateUserSession(UserModel userModel);
        void RemoveUserSession();
        UserModel GetUserSession();
    }
}
