using Microsoft.AspNetCore.Mvc;
using MVCWebSite.Filters;
using MVCWebSite.Models;
using MVCWebSite.Repository;

namespace MVCWebSite.Controllers
{
    [PageOnlyForAdmin]
    public class UserController : Controller
    {
        private readonly IUserRepository _userRepository;
        private readonly IContactRepository _contactRepository;
        public UserController(IUserRepository userRepository, IContactRepository contactRepository)
        {
            _userRepository = userRepository;
            _contactRepository = contactRepository;
        }

        public IActionResult Index()
        {
            List<UserModel> users = _userRepository.GetAll();

            return View(users);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            UserModel user = _userRepository.ListById(id);
            return View(user);
        }

        [HttpGet]
        public IActionResult DeleteConfirmation(int id)
        {
            UserModel user = _userRepository.ListById(id);
            return View(user);
        }

        public IActionResult Delete(int id)
        {
            try
            {
                bool deleted = _userRepository.Delete(id);

                if (deleted)
                {
                    TempData["SucessMessage"] = "User successfully deleted!";
                }
                else
                {
                    TempData["ErrorMessage"] = "Ops, can't delete user!";
                }

                return RedirectToAction("Index");
            }
            catch (Exception e)
            {
                TempData["ErrorMessage"] = $"Ops, can't delete user, try again, error detail:{e.Message}";
                return RedirectToAction("Index");
            }
        }

        public IActionResult ListContactsByUserId(int id)
        {
            List<ContactModel> contacts = _contactRepository.GetAll(id);
            return PartialView("_ContactsUser", contacts);
        }
        [HttpPost]
        public IActionResult Create(UserModel userModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _userRepository.Add(userModel);

                    TempData["SucessMessage"] = "User successfully registered!";
                    return RedirectToAction("Index");
                }

                return View(userModel);
            }
            catch (Exception e)
            {
                TempData["ErrorMessage"] = $"Ops, can't register user, error detail:{e.Message}";
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public IActionResult Edit(UserNoPasswordModel userNoPasswordModel)
        {
            try
            {
                UserModel user = null;

                if (ModelState.IsValid)
                {
                    user = new UserModel()
                    {
                        Id = userNoPasswordModel.Id,
                        Name = userNoPasswordModel.Name,
                        Login = userNoPasswordModel.Login,
                        Email = userNoPasswordModel.Email,
                        Profile = userNoPasswordModel.Profile
                    };

                    _userRepository.Update(user);
                    TempData["SucessMessage"] = "User successfully updated!";
                    return RedirectToAction("Index");
                }

                return View(user);
            }
            catch (Exception e)
            {
                TempData["ErrorMessage"] = $"Ops, can't update user, try again, error detail:{e.Message}";
                return RedirectToAction("Index");
            }
        }
    }
}
