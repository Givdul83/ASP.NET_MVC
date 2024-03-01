using Infrastructure.Models;

namespace WebApp.ViewModels
{
    public class SignInViewModel
    {
        public string Title { get; set; } = "Sign In";

        public SignInForm Form {get; set; } = new SignInForm();
    }
}
