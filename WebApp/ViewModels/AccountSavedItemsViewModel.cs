using Infrastructure.Models;

namespace WebApp.ViewModels
{
    public class AccountSavedItemsViewModel
    {

        public string Title { get; set; } = "Saved Items";

        public CourseModel? Course { get; set; }

        public IEnumerable<CourseModel> Courses { get; set; } = new List<CourseModel>();

        public AccountBasic AccountBasic { get; set; } = new AccountBasic();
    }
}