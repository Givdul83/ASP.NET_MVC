using Infrastructure.Models;

namespace WebApp.ViewModels;

public class CoursesViewmodel
{
    public string Title { get; set; } = "Courses";
    public CourseModel? Course { get; set; }
    
    public IEnumerable<CourseModel> Courses { get; set; } = new List<CourseModel>();

    public Pagination Pagination { get; set; } = new Pagination();

    public IEnumerable<CourseModel> SavedCourses { get; set; } = new List<CourseModel>();

}
