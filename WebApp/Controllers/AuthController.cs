using Infrastructure.Entities;
using Infrastructure.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApp.ViewModels;

namespace WebApp.Controllers
{
    public class AuthController : Controller
    {
       private readonly UserManager<UserEntity> _userManager;

       private readonly SignInManager<UserEntity> _signInManager;

        public AuthController(UserManager<UserEntity> userManager, SignInManager<UserEntity> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }



        //[Route("/signup")]
        [HttpGet]
        public IActionResult SignUp()
        {
            var viewModel = new SignUpViewModel();
            return View(viewModel);
        }

        //[Route("/signup")]
        [HttpPost]
        public async Task<IActionResult> SignUp(SignUpViewModel viewmodel)
        {
            if (ModelState.IsValid)
            {
                var exists = await _userManager.Users.AnyAsync(x => x.Email == viewmodel.Form.Email); 

                if(exists)
                {
                    ModelState.AddModelError("AlreadyExists", "User with this email address already exists");
                    ViewData["ErrorMessage"] = "User with this email address already exists";
                    return View(viewmodel);
                }

                var user = new UserEntity
                {
                    FirstName = viewmodel.Form.FirstName,
                    LastName = viewmodel.Form.LastName,
                    Email = viewmodel.Form.Email,
                    UserName = viewmodel.Form.Email,
                };



              var result =  await _userManager.CreateAsync(user, viewmodel.Form.Password);
                if (result.Succeeded)
                {
                    return RedirectToAction("SignIn", "Auth");
                }
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
        public async Task<IActionResult> SignIn(SignInViewModel viewmodel)
        {
            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(viewmodel.Form.Email, viewmodel.Form.Password, viewmodel.Form.RememberMe, false);

                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Account");
                }
            }

            ModelState.AddModelError("IncorrectValues", "Incorrect Email or Password");
            ViewData["ErrorMessage"] = "Incorrect Email or Password";
            return View(viewmodel);

           
        }
    }
}
