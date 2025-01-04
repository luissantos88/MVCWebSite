using Microsoft.AspNetCore.Http;
using MVCWebSite.Models;
using Newtonsoft.Json;

namespace MVCWebSite.Helper
{
    public class UserSession : IUserSession
    {
        private readonly IHttpContextAccessor _httpcontext;

        public UserSession(IHttpContextAccessor httpcontext)
        {
            _httpcontext = httpcontext;
        }
        public void CreateUserSession(UserModel user)
        {
            string value = JsonConvert.SerializeObject(user);

            _httpcontext.HttpContext.Session.SetString("userSessionLogin", value);
        }

        public UserModel GetUserSession()
        {
            string userSession = _httpcontext.HttpContext.Session.GetString("userSessionLogin");

            if (string.IsNullOrEmpty(userSession))  return null;

            return JsonConvert.DeserializeObject<UserModel>(userSession);
        }

        public void RemoveUserSession()
        {
            _httpcontext.HttpContext.Session.Remove("userSessionLogin");
        }
    }
}
