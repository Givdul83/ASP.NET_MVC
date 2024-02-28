using Microsoft.AspNetCore.Mvc;
using WebApp.ViewModels;

namespace WebApp.Controllers;

public class HomeController : Controller
{
    public IActionResult Index()
    {
        ViewData["Title"] = "Ultimate Task Management Assistant";

        return View();
    }

    [HttpGet]
    public IActionResult NotFound404()
    {
        var viewModel = new NotFoundViewModel();
        return View(viewModel);
    }

    [HttpPost] 
    public IActionResult NotFound404(NotFoundViewModel viewmodel)
    {
        if (ModelState.IsValid)
        {
            return RedirectToAction("Index", "Home");
        }

        return View(viewmodel);
    }

    [HttpGet]
    public IActionResult Contact()
    {
        var viewModel = new ContactViewModel();
        return View(viewModel);
    }

    [HttpPost]
    public IActionResult Contact(ContactViewModel viewmodel)
    {
        if (ModelState.IsValid)
        {
            return RedirectToAction("Contact", "Home");
        }

        return View(viewmodel);
    }
}
