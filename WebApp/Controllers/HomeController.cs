using Infrastructure.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http;
using System.Text;
using WebApp.ViewModels;

namespace WebApp.Controllers;

public class HomeController(HttpClient httpClient) : Controller
{
    private readonly HttpClient _httpClient = httpClient;

    public IActionResult Index()
    {
        ViewData["Title"] = "Ultimate Task Management Assistant";

        return View();
    }

    [Route("/error")]
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
    public async Task<IActionResult> Contact(ContactViewModel viewmodel)
    {
        if (ModelState.IsValid)
        {
            try
            {
                var contactForm = new ContactForm
                {
                    FullName = viewmodel.ContactForm.FullName,
                    Email = viewmodel.ContactForm.Email,
                    Service = viewmodel.ContactForm.Service,
                    Message = viewmodel.ContactForm.Message,
                };

                if (contactForm != null)
                {
                    var json = JsonConvert.SerializeObject(contactForm);

                    if (json != null)
                    {
                        var content = new StringContent(json, Encoding.UTF8, "application/json");
                        var response = await _httpClient.PostAsync("https://localhost:7135/api/contact?key=MjcyYzdiNzMtYmQ3OS00NTY4LTk5OGQtYjQ4MjgwZDdhMGIx", content);

                        if (response.IsSuccessStatusCode)
                        {
                            TempData["Status"] = "Your message has been sent";
                            return RedirectToAction("Contact", "Home");
                        }
                        else
                        {
                            TempData["StatusFail"] = "Message was not sent";
                            return RedirectToAction("Contact", "Home");
                        }

                    }

                }

            }

            catch
            {
                TempData["StatusFail"] = "Connection failed";
                return RedirectToAction("Contact", "Home");
            }
        }
        TempData["StatusFail"] = "Something went wrong";
        return RedirectToAction("Contact", "Home");
    }

    [HttpGet]
    public IActionResult Subscribe()
    {
        var viewModel = new SubscribeViewModel();
        return View(viewModel);
    }

    [HttpPost]
    public async Task<IActionResult> Subscribe(SubscribeViewModel viewModel)
    {
        if (ModelState.IsValid)
        {
            try
            {
                var newSub = new SubscribeEmail
                {
                    Email = viewModel.SubscribeEmail!.Email,
                    DailyNewsletter = viewModel.SubscribeEmail.DailyNewsletter,
                    AdvertisingUpdates = viewModel.SubscribeEmail.AdvertisingUpdates,
                    WeekinReview = viewModel.SubscribeEmail.WeekinReview,
                    StartupsWeekly = viewModel.SubscribeEmail.StartupsWeekly,
                    Podcasts = viewModel.SubscribeEmail.Podcasts,
                    EventUpdates = viewModel.SubscribeEmail.EventUpdates,
                };
                if (newSub != null)
                {
                    var json = JsonConvert.SerializeObject(newSub);

                    if (json != null)
                    {
                        var content = new StringContent(json, Encoding.UTF8, "application/json");
                        var response = await _httpClient.PostAsync("https://localhost:7135/api/subscriber?key=MjcyYzdiNzMtYmQ3OS00NTY4LTk5OGQtYjQ4MjgwZDdhMGIx", content);

                        if (response.IsSuccessStatusCode)
                        {
                            TempData["Status"] = "You have succesfully subscribed";
                            return RedirectToAction("Index", "Home", null, "subscribe-action");

                        }
                        else if (response.StatusCode == System.Net.HttpStatusCode.Conflict)
                        {
                            TempData["StatusFail"] = "Email address is already subscribed";
                            return RedirectToAction("Index", "Home", null, "subscribe-action"); 

                        }
                    }

                }


            }
            catch
            {
                TempData["StatusFail"] = "ConnectionFailed";
                return RedirectToAction("Index", "Home", null, "subscribe-action");

            }
        }
        else
        {
            TempData["StatusFail"] = "Failed";
            return RedirectToAction("Index", "Home", null, "subscribe-action");

        }

        return RedirectToAction("Index", "Home", null, "subscribe-action");
    }

    [HttpGet]

    public IActionResult UnSubscribe()
    {
        var viewModel = new UnSubscribeViewModel();
        return View(viewModel);
    }

    [HttpPost]

    public async Task<IActionResult> Unsubscribe(UnSubscribeViewModel unSubscribe)
    {
        if (ModelState.IsValid)
        {
            if (unSubscribe != null)
            {
                var unSub = new UnSubscribeModel
                {
                    Email = unSubscribe.UnSubscribeModel!.Email,
                    ConfirmBox = unSubscribe.UnSubscribeModel.ConfirmBox,
                };


                string apiKey = "MjcyYzdiNzMtYmQ3OS00NTY4LTk5OGQtYjQ4MjgwZDdhMGIx";
                string apiUrl = $"https://localhost:7135/api/Subscriber/email?email={unSub.Email} &key={apiKey}";
                var response = await _httpClient.DeleteAsync(apiUrl);


                if (response.IsSuccessStatusCode)
                {
                    TempData["Status"] = "Successfully Unsubscribed";
                    return View(unSubscribe);
                }
                else if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    TempData["StatusFail"] = "Your Email address was not found ,please try again";
                    return RedirectToAction("Unsubscribe", "Home");
                }
                else
                {
                    TempData["StatusFail"] = "Something went wrong with email or checkbox";
                    return RedirectToAction("Unsubscribe", "Home");
                } 
            }
            TempData["StatusFail"] = "You must enter a Email address ";
            return RedirectToAction("Unsubscribe", "Home");
        }
        TempData["StatusFail"] = "Checkbox must be confirmed";
        return RedirectToAction("Unsubscribe", "Home");

    }
}
