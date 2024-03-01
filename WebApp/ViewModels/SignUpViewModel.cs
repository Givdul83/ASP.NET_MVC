using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;
using Infrastructure.Models;

namespace WebApp.ViewModels;

public class SignUpViewModel
{
    public string Title { get; set; } = "Sign up";

    public SignUpForm Form { get; set; } = new SignUpForm();
}