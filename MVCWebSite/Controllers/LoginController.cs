using Microsoft.AspNetCore.Mvc;
using MVCWebSite.Helper;
using MVCWebSite.Models;
using MVCWebSite.Repository;

namespace MVCWebSite.Controllers
{
    public class LoginController : Controller
    {

        private readonly IUserRepository _userRepository;
        private readonly IUserSession _userSession;
        private readonly IEmail _email;
        public LoginController(IUserRepository userRepository, IUserSession userSession, IEmail email)
        {
            _userRepository = userRepository;
            _userSession = userSession;
            _email = email;
        }
        public IActionResult Index()
        {
            // if user already logged redirect to home
            if (_userSession.GetUserSession() != null) return RedirectToAction("Index", "Home");
            return View();
        }

        public IActionResult ResetMyPassword()
        {
            return View();
        }

        public IActionResult Logout()
        {
            _userSession.RemoveUserSession();

            return RedirectToAction("Index", "Login");
        }

        [HttpPost]
        public IActionResult Login(LoginModel loginModel)
        {
            try
            {
                if (ModelState.IsValid)
                {

                    UserModel user = _userRepository.ListByLogin(loginModel.Login);

                    if (user != null)
                    {
                        if (user.ValidPassword(loginModel.Password))
                        {
                            _userSession.CreateUserSession(user);
                            return RedirectToAction("Index", "Home");
                        }

                        TempData["ErrorMessage"] = $"User password invalid. Please, try again.";
                    }

                    TempData["ErrorMessage"] = $"User and/or password invalid. Please, try again.";
                }

                return View("Index");
            }
            catch (Exception e)
            {
                TempData["ErrorMessage"] = $"Ops, couldn´t process your login, try again error detail:{e.Message}";
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public IActionResult SendLinkToDefinePassword(ResetMyPasswordModel resetMyPasswordModel)
        {
            try
            {
                if (ModelState.IsValid)
                {

                    UserModel user = _userRepository.GetByEmailAndLogin(resetMyPasswordModel.Email, resetMyPasswordModel.Login);

                    if (user != null)
                    {
                        string newPassword = user.GenerateNewPassword();
                        string message = $"Your new password is : {newPassword}";

                        bool sendEmail = _email.Send(user.Email, "MVC Web Site - New Password", message);

                        if (sendEmail)
                        {
                            _userRepository.Update(user);
                            TempData["SucessMessage"] = $"We have just sent a new password to your email.";
                        }
                        else
                        {
                            TempData["ErrorMessage"] = $"Couldn't send email. Please try again";
                        }
                                              
                        return RedirectToAction("Index", "Login");
                    }

                    TempData["ErrorMessage"] = $"Couldn´t reset your password. Please verify your login and email.";
                }

                return View("Index");
            }
            catch (Exception e)
            {
                TempData["ErrorMessage"] = $"Ops, couldn't reset your password, try again error detail:{e.Message}";
                return RedirectToAction("Index");
            }
        }


    }
}
