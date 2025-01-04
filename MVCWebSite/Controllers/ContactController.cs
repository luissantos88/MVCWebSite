using Microsoft.AspNetCore.Mvc;
using MVCWebSite.Filters;
using MVCWebSite.Helper;
using MVCWebSite.Models;
using MVCWebSite.Repository;

namespace MVCWebSite.Controllers
{
    [PageForUserLogin]
    public class ContactController : Controller
    {
        private readonly IContactRepository _contactRepository;
        private readonly IUserSession _userSession;
        public ContactController(IContactRepository contactRepository, IUserSession userSession)
        {
            _contactRepository = contactRepository;
            _userSession = userSession;
        }
        public IActionResult Index()
        {
            UserModel userLoggedIn = _userSession.GetUserSession();
            List<ContactModel> contacts = _contactRepository.GetAll(userLoggedIn.Id);

            return View(contacts);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpGet]
        public IActionResult Edit(int id)
        {
            ContactModel contact = _contactRepository.ListById(id);
            return View(contact);
        }
        public IActionResult DeleteConfirmation(int id)
        {
            ContactModel contact = _contactRepository.ListById(id);
            return View(contact);
        }
        public IActionResult Delete(int id)
        {
            try
            {
                bool deleted = _contactRepository.Delete(id);

                if (deleted)
                {
                    TempData["SucessMessage"] = "Contact successfully deleted!";
                }
                else
                {
                    TempData["ErrorMessage"] = "Ops, cant delete contact!";
                }

                return RedirectToAction("Index");
            }
            catch (Exception e)
            {
                TempData["ErrorMessage"] = $"Ops, cant delete contact, try again, error detail:{e.Message}";
                return RedirectToAction("Index");
            }
        }
        [HttpPost]
        public IActionResult Create(ContactModel contactModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    UserModel userLoggedIn = _userSession.GetUserSession();
                    contactModel.UserId = userLoggedIn.Id;
                    _contactRepository.Add(contactModel);
                    TempData["SucessMessage"] = "Contact successfully registered!";
                    return RedirectToAction("Index");
                }

                return View(contactModel);
            }
            catch (Exception e)
            {
                TempData["ErrorMessage"] = $"Ops, cant register contact, try again, error detail:{e.Message}";
                return RedirectToAction("Index");
            }
        }
        [HttpPost]
        public IActionResult Edit(ContactModel contactModel)
        {
            try
            {
                //if (ModelState.IsValid)
                //{
                    UserModel userLoggedIn = _userSession.GetUserSession();
                    contactModel.UserId = userLoggedIn.Id;
                    contactModel = _contactRepository.Update(contactModel);
                    TempData["SucessMessage"] = "Contact successfully updated!";
                    return RedirectToAction("Index");
                //}
                //return View(contactModel);
                // return View("Editar", contactModel) force to go to view Edit
            }
            catch (Exception e)
            {
                TempData["ErrorMessage"] = $"Ops, cant update contact, error detail:{e.Message}";
                return RedirectToAction("Index");

            }
        }
    }
}
