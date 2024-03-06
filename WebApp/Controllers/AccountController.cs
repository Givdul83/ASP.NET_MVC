using Infrastructure.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebApp.ViewModels;

namespace WebApp.Controllers;

public class AccountController : Controller
{

    private readonly UserManager<UserEntity> _userManager;
    private readonly SignInManager<UserEntity> _signInManager;
    public AccountController(UserManager<UserEntity> userManager, SignInManager<UserEntity> signInManager)
    {
        _userManager = userManager;
        _signInManager = signInManager;
    }

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


   
    [HttpGet]

    public new async Task<IActionResult>LogOut(AccountDetailsViewModel viewmodel) 
    {
       await _signInManager.SignOutAsync();
        return RedirectToAction("Index", "Home");
        
    }
    
}
