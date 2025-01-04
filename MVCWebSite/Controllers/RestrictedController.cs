using Microsoft.AspNetCore.Mvc;
using MVCWebSite.Filters;

namespace MVCWebSite.Controllers
{
    [PageForUserLogin]
    public class RestrictedController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
