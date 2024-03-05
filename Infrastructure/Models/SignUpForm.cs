using System.ComponentModel.DataAnnotations;

namespace Infrastructure.Models;

public class SignUpForm
{

    [Display(Name = "First name", Prompt = "Enter first name", Order = 0)]
    [DataType(DataType.Text)]
    [Required(ErrorMessage = "Invalid first name")]
    public string FirstName { get; set; } = null!;

    [Display(Name = "Last name", Prompt = "Enter last name", Order = 1)]
    [DataType(DataType.Text)]
    [Required(ErrorMessage = "Invalid last name")]
    public string LastName { get; set; } = null!;


    [Display(Name = "Email", Prompt = "Enter Email address", Order = 2)]
    [DataType(DataType.EmailAddress)]
    [Required(ErrorMessage = "Invalid Email address")]
    [RegularExpression(@"^[a-zA-Z0-9._%+-]{1,}@[a-zA-Z0-9.-]{2,}\.[a-zA-Z]{2,}$", ErrorMessage = "Invalid email address")]
    public string Email { get; set; } = null!;


    [Display(Name = "Password", Prompt = "Enter password", Order = 3)]
    [DataType(DataType.Password)]
    [Required(ErrorMessage = "Password is required")]
    [RegularExpression("^(?=.*[a-z])(?=.*[A-Z])(?=.*\\d)(?=.*[^a-zA-Z\\d]).{8,}$", ErrorMessage = "Invalid password format")]
    public string Password { get; set; } = null!;


    [Display(Name = "Confirm Password", Prompt = "Confirm password", Order = 3)]
    [DataType(DataType.Password)]
    [Required(ErrorMessage = "Password must be confirmed")]
    [RegularExpression("^(?=.*[a-z])(?=.*[A-Z])(?=.*\\d)(?=.*[^a-zA-Z\\d]).{8,}$", ErrorMessage = "Invalid password format")]
    [Compare(nameof(Password), ErrorMessage = "password dosent match")]
    public string ConfirmPassword { get; set; } = null!;


    [Display(Name = "I agree to the terms", Order = 5)]
    [Required(ErrorMessage = "You must agree to Terms and Conditions")]
    [Range(typeof(bool), "true", "true", ErrorMessage = "You must agree to Terms and Conditions")]
    public bool TermsAndConditions { get; set; } = false;

}


