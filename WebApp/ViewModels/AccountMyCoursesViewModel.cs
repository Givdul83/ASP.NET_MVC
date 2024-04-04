using Infrastructure.Models;

namespace WebApp.ViewModels;

public class AccountMyCoursesViewModel
{
    public string Title { get; set; } = "My Courses";

    public CourseModel? Course { get; set; }

    public IEnumerable<CourseModel> Courses { get; set; } = new List<CourseModel>();

    public AccountBasic AccountBasic { get; set; } = new AccountBasic();
}