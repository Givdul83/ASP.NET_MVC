using Infrastructure.Entities;
using Infrastructure.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Security.Claims;
using System.Text;
using WebApp.ViewModels;

namespace WebApp.Controllers;

public class AuthController : Controller
{
   private readonly UserManager<UserEntity> _userManager;

   private readonly SignInManager<UserEntity> _signInManager;

    private readonly HttpClient _httpClient;

	public AuthController(UserManager<UserEntity> userManager, SignInManager<UserEntity> signInManager, HttpClient httpClient)
	{
		_userManager = userManager;
		_signInManager = signInManager;
		_httpClient = httpClient;
	}




	[HttpGet]
    public IActionResult SignUp()
    {
        var viewModel = new SignUpViewModel();
        return View(viewModel);
    }

    
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
                //string apiKey= "MjcyYzdiNzMtYmQ3OS00NTY4LTk5OGQtYjQ4MjgwZDdhMGIx";

                var userDto = new ApiUserModel
                {
                    Email = viewmodel.Form.Email,
                };

				var json = JsonConvert.SerializeObject(userDto);

				var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await _httpClient.PostAsync("https://localhost:7135/api/Auth?key=MjcyYzdiNzMtYmQ3OS00NTY4LTk5OGQtYjQ4MjgwZDdhMGIx", content);
                if (response.IsSuccessStatusCode)
                {
                    
					var responseContent = await response.Content.ReadAsStringAsync();
					var tokenResponse = JsonConvert.DeserializeObject<Dictionary<string, string>>(responseContent);

					if (tokenResponse != null && tokenResponse.TryGetValue("token", out var token) && !string.IsNullOrEmpty(token))
					{

						var cookieOptions = new CookieOptions
                        {
                            HttpOnly = true,
                            Secure = true,
                            Expires = DateTime.UtcNow.AddDays(1)
                        };

                        Response.Cookies.Append("AccessToken", token, cookieOptions);
						return RedirectToAction("Index", "Account");
					}

                    return Unauthorized("no valid token found");
                }


                
            }
           
		}

        ModelState.AddModelError("IncorrectValues", "Incorrect Email or Password");
        ViewData["ErrorMessage"] = "Incorrect Email or Password";
        return View(viewmodel);

       
    }
    [HttpGet]

    public IActionResult Facebook()
    {
        var authProps = _signInManager.ConfigureExternalAuthenticationProperties("Facebook", Url.Action("FacebookCallback"));
        return new ChallengeResult("Facebook", authProps);
    }

    [HttpGet]
    public async Task<IActionResult> FacebookCallback()
    {
        var info = await _signInManager.GetExternalLoginInfoAsync();

        if (info != null)
        {
            var userEntity = new UserEntity
            {
                FirstName = info.Principal.FindFirstValue(ClaimTypes.GivenName)!,
                LastName = info.Principal.FindFirstValue(ClaimTypes.Surname)!,
                Email = info.Principal.FindFirstValue(ClaimTypes.Email)!,
                UserName = info.Principal.FindFirstValue(ClaimTypes.Email)!,
                IsExternalAccount= true,
            };

            var user = await _userManager.FindByEmailAsync(userEntity.Email);
            if (user == null)
            {
                var result = await _userManager.CreateAsync(userEntity);
                if (result.Succeeded)
                {
                    user = await _userManager.FindByEmailAsync(userEntity.Email);
                }
            }
                if(user != null) 
                {
                    if (user.FirstName != userEntity.FirstName || user.LastName != userEntity.LastName || user.Email != userEntity.Email )
                    {
                        user.FirstName= userEntity.FirstName;
                        user.LastName= userEntity.LastName;
                        user.Email= userEntity.Email;
                        
                        await _userManager.UpdateAsync(user);
                    }
                    await _signInManager.SignInAsync(user, isPersistent: false);

                    if(HttpContext.User != null)
                    {
                        return RedirectToAction("Index", "Account");
                    }
                }
            
        }
        return RedirectToAction("Index", "Account");

    }

    [HttpGet]

    public IActionResult Google()
    {
        var authProps = _signInManager.ConfigureExternalAuthenticationProperties("Google", Url.Action("GoogleCallback"));
        return new ChallengeResult("Google", authProps);
    }

    [HttpGet]
    public async Task<IActionResult> GoogleCallback()
    {
        var info = await _signInManager.GetExternalLoginInfoAsync();

        if (info != null)
        {
            var userEntity = new UserEntity
            {
                FirstName = info.Principal.FindFirstValue(ClaimTypes.GivenName)!,
                LastName = info.Principal.FindFirstValue(ClaimTypes.Surname)!,
                Email = info.Principal.FindFirstValue(ClaimTypes.Email)!,
                UserName = info.Principal.FindFirstValue(ClaimTypes.Email)!,
                IsExternalAccount = true,
            };

            var user = await _userManager.FindByEmailAsync(userEntity.Email);
            if (user == null)
            {
                var result = await _userManager.CreateAsync(userEntity);
                if (result.Succeeded)
                {
                    user = await _userManager.FindByEmailAsync(userEntity.Email);
                }
            }
            if (user != null)
            {
                if (user.FirstName != userEntity.FirstName || user.LastName != userEntity.LastName || user.Email != userEntity.Email)
                {
                    user.FirstName = userEntity.FirstName;
                    user.LastName = userEntity.LastName;
                    user.Email = userEntity.Email;

                    await _userManager.UpdateAsync(user);
                }
                await _signInManager.SignInAsync(user, isPersistent: false);

                if (HttpContext.User != null)
                {
                    return RedirectToAction("Index", "Account");
                }
            }

        }
        return RedirectToAction("Index", "Account");

    }
}



