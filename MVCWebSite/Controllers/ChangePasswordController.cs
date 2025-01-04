using Microsoft.AspNetCore.Mvc;
using MVCWebSite.Filters;
using MVCWebSite.Helper;
using MVCWebSite.Models;
using MVCWebSite.Repository;

namespace MVCWebSite.Controllers
{
    [PageForUserLogin]
    public class ChangePasswordController : Controller
    {
        private readonly IUserRepository _userRepository;
        private readonly IUserSession _userSession;
        public ChangePasswordController(IUserRepository userRepository, IUserSession userSession)
        {
            _userRepository = userRepository;
            _userSession = userSession;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Change(ChangePasswordModel changePasswordModel)
        {
            try
            {
                UserModel userLoggedIn = _userSession.GetUserSession();
                changePasswordModel.Id = userLoggedIn.Id;
                if (ModelState.IsValid)
                {                   
                    _userRepository.ChangePassword(changePasswordModel);
                    TempData["SucessMessage"] = "Password successfully updated!";
                    return View("Index", changePasswordModel);
                }

                return View("Index", changePasswordModel);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"Ops, can't update your user password, try gain, erro detail: {ex.Message}";
                return View("Index", changePasswordModel);
            }
        }       
    }
}
