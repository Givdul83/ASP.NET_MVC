using Infrastructure.Entities;
using Infrastructure.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Newtonsoft.Json;
using System.Diagnostics.Eventing.Reader;
using System.Net.Http.Headers;
using System.Runtime.CompilerServices;
using System.Text;
using WebApp.ViewModels;

namespace WebApp.Controllers;

public class CourseController(HttpClient httpClient, UserManager<UserEntity> userManager) : Controller
{
    private readonly HttpClient _httpClient = httpClient;
    private readonly UserManager<UserEntity> _userManager = userManager;

    [HttpGet]
    public async Task<IActionResult> Index(string searchString, int? category, int pageNumber = 1, int pageSize = 6)
    {
        try
        {
            var viewModel = new CoursesViewmodel();
            viewModel.Courses = await PopulateCourses();
            viewModel.SavedCourses = await CheckForSavedCourse();

            if (!String.IsNullOrEmpty(searchString))
            {
                viewModel.Courses = viewModel.Courses.Where(s => s.Title.ToLower().Contains(searchString.ToLower()));
            }
            if (category.HasValue)
            {
                switch (category)
                {
                    case 1:
                        viewModel.Courses = viewModel.Courses.Where(c => c.IsBestSeller == true);
                        break;
                    case 2:
                        viewModel.Courses = viewModel.Courses.Where(c => c.DiscountPrice != null);
                        break;
                    default:
                        break;
                }
            }

            viewModel.Pagination.CurrentPage = pageNumber;
            viewModel.Pagination.PageSize = pageSize;
            viewModel.Pagination.TotalItems = viewModel.Courses.Count();

            viewModel.Courses = viewModel.Courses.Skip((pageNumber - 1) * pageSize)
                                                 .Take(pageSize)
                                                 .ToList();

            return View(viewModel);
        }
        catch
        {
            return BadRequest();
        }
    }

    [HttpGet]
    public async Task<IEnumerable<CourseModel>> PopulateCourses()
    {
        if (HttpContext.Request.Cookies.TryGetValue("AccessToken", out var token))
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            string apiUrl = "https://localhost:7135/api/course?key=MjcyYzdiNzMtYmQ3OS00NTY4LTk5OGQtYjQ4MjgwZDdhMGIx";


            var response = await _httpClient.GetAsync(apiUrl);

            var json = await response.Content.ReadAsStringAsync();

            var data = JsonConvert.DeserializeObject<IEnumerable<CourseModel>>(json);
            if (data != null)
            {
                return data;
            }


            else
            {
                return Enumerable.Empty<CourseModel>();
            }
        }
        return Enumerable.Empty<CourseModel>();

    }
    [HttpPost]

    public async Task<IActionResult> SaveCourse(int CourseId)
    {
        string apiUrl = "https://localhost:7135/api/SavedCourse";

        var user = await _userManager.GetUserAsync(User);

        if (user != null)
        {
            var saveCourse = new SaveCourseModel
            {
                UserEmail = user.Email!,
                CourseId = CourseId,

            };

            var json = JsonConvert.SerializeObject(saveCourse);

            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync(apiUrl, content);

            if (response.IsSuccessStatusCode)
            {
                TempData["Saved"] = "Course saved";
                return RedirectToAction("Index", "Course");
            }

            else
            {
                TempData["Failed"] = "Something went wrong";
                return RedirectToAction("Index", "Course");





            }

        }
        TempData["Failed"] = "Something went wrong";
        return RedirectToAction("Index", "Course");
    }


[HttpGet]
public async Task<IActionResult> SingleCourse(int id)
{

    string apiUrl = "https://localhost:7135/api/course/" + id + "?key=MjcyYzdiNzMtYmQ3OS00NTY4LTk5OGQtYjQ4MjgwZDdhMGIx";

    var response = await _httpClient.GetAsync(apiUrl);

    if (response.IsSuccessStatusCode)
    {
        var json = await response.Content.ReadAsStringAsync();
        var courseModel = JsonConvert.DeserializeObject<CourseModel>(json);

        if (courseModel != null)
        {
            var viewModel = new CoursesViewmodel
            {
                Course = courseModel
            };
            return View(viewModel);
        }
    }

    return RedirectToAction("Index", "Course");
}

[HttpPost]

public async Task<IActionResult> SaveSingleCourse(int CourseId)
{


    string apiUrl = "https://localhost:7135/api/MyCourses";

    var user = await _userManager.GetUserAsync(User);


    if (user != null)
    {
        var saveCourse = new SaveCourseModel
        {
            UserEmail = user.Email!,
            CourseId = CourseId,

        };

        var json = JsonConvert.SerializeObject(saveCourse);

        var content = new StringContent(json, Encoding.UTF8, "application/json");
        var response = await _httpClient.PostAsync(apiUrl, content);

        if (response.IsSuccessStatusCode)
        {
            TempData["Saved"] = "Course saved";
            return RedirectToAction("MyCourses", "Account");
        }

        else
        {
            TempData["Failed"] = "Something went wrong";
            return NoContent();
        }

    }
    return BadRequest();
}

private async Task<IEnumerable<CourseModel>> CheckForSavedCourse()
{
    string apiUrl = "https://localhost:7135/api/savedcourse/";
    var user = await _userManager.GetUserAsync(User);

    if (user != null)
    {
        var userDto = new UserToGetCoursesModel
        {
            Email = user.Email!
        };

        if (userDto.Email != null)
        {


            var response = await _httpClient.GetAsync($"{apiUrl}{userDto.Email}");

            var json = await response.Content.ReadAsStringAsync();

            var data = JsonConvert.DeserializeObject<IEnumerable<CourseModel>>(json);
            if (data != null)
            {
                return data;
            }


            else
            {
                return Enumerable.Empty<CourseModel>();
            }
        }

        return Enumerable.Empty<CourseModel>();
    }
    return Enumerable.Empty<CourseModel>();
}

}


