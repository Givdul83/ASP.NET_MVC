using System.ComponentModel.DataAnnotations;

namespace WebApp.Models;

public class SignUpForm
{

    [Display(Name = "First name", Prompt = "Enter first name", Order = 0)]
    [Required(ErrorMessage = "Invalid first name")]
    public string FirstName { get; set; } = null!;

    [Display(Name = "Last name", Prompt = "Enter last name", Order = 1)]
    [Required(ErrorMessage = "Invalid last name")]
    public string LastName { get; set; } = null!;

    [DataType(DataType.EmailAddress)]
    [Display(Name = "Email", Prompt = "Enter Email address", Order = 2)]

    [RegularExpression(@"^[a-zA-Z0-9._%+-]{1,}@[a-zA-Z0-9.-]{2,}\.[a-zA-Z]{2,}$", ErrorMessage = "Invalid email address")]
    public string Email { get; set; } = null!;


    [Display(Name = "Password", Prompt = "Enter password", Order = 3)]
    [DataType(DataType.Password)]
    [RegularExpression("^(?=.*[a-z])(?=.*[A-Z])(?=.*\\d)(?=.*[^a-zA-Z\\d]).{8,}$\r\n", ErrorMessage = "Invalid password format")]
    public string Password { get; set; } = null!;


    [Display(Name = "Confirm Password", Prompt = "Confirm password", Order = 3)]
        [DataType(DataType.Password)]
    [RegularExpression("^(?=.*[a-z])(?=.*[A-Z])(?=.*\\d)(?=.*[^a-zA-Z\\d]).{8,}$\r\n", ErrorMessage = "Invalid password format")]
    [Compare(nameof(Password), ErrorMessage = "password dosent match")]
    public string ConfirmPassword { get; set; } = null!;


    [Display(Name = "I agree to the terms", Order = 5)]
    [Required(ErrorMessage = "Invalid last name")]
    public bool TermsAndConditions { get; set; } = false;
}


