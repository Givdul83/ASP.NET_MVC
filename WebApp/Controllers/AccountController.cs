using Microsoft.AspNetCore.Mvc;
using WebApp.ViewModels;

namespace WebApp.Controllers;

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

    [HttpGet]
    public IActionResult Security()
    {
        var viewmodel = new AccountSecurityViewModel();
        return View(viewmodel);
    }


    [HttpPost]

    public IActionResult Security(AccountSecurityViewModel viewmodel)
    {
        if (ModelState.IsValid)
        {
            return RedirectToAction("Security", "Account");
        }
        return View(viewmodel);
    }

    [HttpGet]
    public IActionResult SavedItems()
    {
        var viewmodel = new AccountSavedItemsViewModel();
        return View(viewmodel);
    }

    [HttpPost]

    public IActionResult SavedItems(AccountSavedItemsViewModel viewmodel)
    {
        if (ModelState.IsValid)
        {
            return RedirectToAction("SavedItems", "Account");
        }
        return View(viewmodel);
    }
}
