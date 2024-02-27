using System.ComponentModel.DataAnnotations;

namespace WebApp.Models;

public class AccountBasic
{
    public string Title { get; set; } = "Basic Info";

    [Display(Name = "First name", Prompt = "Enter first name", Order = 0)]
    [Required(ErrorMessage = "Invalid first name")]
    public string FirstName { get; set; } = null!;

    [Display(Name = "Last name", Prompt = "Enter last name", Order = 1)]
    [Required(ErrorMessage = "Invalid last name")]
    public string LastName { get; set; } = null!;


    [Display(Name = "Email", Prompt = "Enter Email address", Order = 2)]
    [DataType(DataType.EmailAddress)]
    [Required(ErrorMessage = "Invalid Email address")]
    [RegularExpression(@"^[a-zA-Z0-9._%+-]{1,}@[a-zA-Z0-9.-]{2,}\.[a-zA-Z]{2,}$", ErrorMessage = "Invalid email address")]
    public string Email { get; set; } = null!;

    [Display(Name ="Phone", Prompt ="Enter you Phone", Order =3)]
    public string? Phone {  get; set; }

    [Display(Name ="Bio", Prompt="Add a short bio", Order =4)]
    public string? Bio { get; set;}
    
}
