using Microsoft.AspNetCore.Mvc;
using WebApp.ViewModels;

namespace WebApp.Controllers
{
    public class AuthController : Controller
    {
        //[Route("/signup")]
        [HttpGet]
        public IActionResult SignUp()
        {
            var viewModel = new SignUpViewModel();
            return View(viewModel);
        }

        //[Route("/signup")]
        [HttpPost]
        public IActionResult SignUp(SignUpViewModel viewmodel)
        {
            if (ModelState.IsValid)
            {
                return RedirectToAction("Index", "Account");
            }
            
            return View(viewmodel);
        }


        //[Route("/signin")]

        public IActionResult SignIn()
        {
            var viewModel= new SignInViewModel();
            return View(viewModel);
        }

        [HttpPost]
        public IActionResult SignIn(SignInViewModel viewmodel)
        {
            if (ModelState.IsValid)
            {
                return RedirectToAction("Index", "Home");
            }

            return View(viewmodel);
        }
    }
}
