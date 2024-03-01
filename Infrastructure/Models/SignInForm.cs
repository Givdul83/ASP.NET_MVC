using System.ComponentModel.DataAnnotations;

namespace Infrastructure.Models;

public class SignInForm
{
    [Display(Name = "Email", Prompt = "Enter Email address", Order = 0)]
    [DataType(DataType.EmailAddress)]
    [Required(ErrorMessage = "Invalid Email address")]
    [RegularExpression(@"^[a-zA-Z0-9._%+-]{1,}@[a-zA-Z0-9.-]{2,}\.[a-zA-Z]{2,}$", ErrorMessage = "Invalid email address")]
    public string Email { get; set; } = null!;


    [Display(Name = "Password", Prompt = "Enter password", Order = 1)]
    [DataType(DataType.Password)]
    [Required(ErrorMessage = "Password is required")]
    [RegularExpression("^(?=.*[a-z])(?=.*[A-Z])(?=.*\\d)(?=.*[^a-zA-Z\\d]).{8,}$", ErrorMessage = "Invalid password format")]
    public string Password { get; set; } = null!;


    [Display(Name = "Remember me", Order = 2)]
    
    [Range(typeof(bool), "true", "true")]
    public bool RememberMe { get; set; } = false;
}
