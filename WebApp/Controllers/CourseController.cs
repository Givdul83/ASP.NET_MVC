using Infrastructure.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using WebApp.ViewModels;

namespace WebApp.Controllers;

public class CourseController(HttpClient httpClient) : Controller
{
    private readonly HttpClient _httpClient = httpClient;


    [HttpGet]
    public async Task<IActionResult> Index(string searchString)
    {
        try
        {
            var viewModel = new CoursesViewmodel();
            viewModel.Courses = await PopulateCourses(); 

            if (!String.IsNullOrEmpty(searchString))
            {
                viewModel.Courses = viewModel.Courses.Where(s => s.Title.ToLower().Contains(searchString.ToLower()));
            }


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

        string apiUrl = "https://localhost:7135/api/course";


        var response = await _httpClient.GetAsync(apiUrl);

        var json = await response.Content.ReadAsStringAsync();

        var data = JsonConvert.DeserializeObject<IEnumerable<CourseModel>>(json);
        if(data != null)
        {
            return data;
        }


        else
        {
            return Enumerable.Empty<CourseModel>();
        }
    }

}
