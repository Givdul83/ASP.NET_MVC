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
            
            return View(viewmodel);
        }


        //[Route("/signin")]

        public IActionResult SignIn()
        {
            return View();
        }
    }
}
