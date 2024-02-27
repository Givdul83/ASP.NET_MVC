using Microsoft.AspNetCore.Mvc;
using WebApp.ViewModels;

namespace WebApp.Controllers
{
    public class AccountController : Controller
    {

        [HttpGet] 
        public IActionResult Index()
        {
            var viewmodel = new AccountDetailsViewModel();
            return View(viewmodel);
        }

        [HttpPost]

        public IActionResult Index(AccountDetailsViewModel viewmodel)
        {
            if (ModelState.IsValid)
            {
                return RedirectToAction("Index", "Account");
            }
            return View(viewmodel);
        }
    }
}
