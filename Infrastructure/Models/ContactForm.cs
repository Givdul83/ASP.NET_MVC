using System.ComponentModel.DataAnnotations;

namespace Infrastructure.Models;

public class ContactForm
{
    public string Title = "Get In Contact With Us";

    [Display(Name = "Full name", Prompt = "Enter your full name", Order = 0)]
    [Required(ErrorMessage = "Cannot leave this empty")]
    public string FullName { get; set; } = null!;

    [Display(Name = "Email address", Prompt = "Enter your email address", Order = 1)]
    [DataType(DataType.EmailAddress)]
    [Required(ErrorMessage = "Invalid Email address")]
    [RegularExpression(@"^[a-zA-Z0-9._%+-]{1,}@[a-zA-Z0-9.-]{2,}\.[a-zA-Z]{2,}$", ErrorMessage = "Invalid email address")]
    public string Email { get; set; } = null!;


    [Display(Name = "Service", Prompt = "Choose the service you are interested in", Order = 2)]
    public string? Service { get; set; }

    [Display(Name = "Message", Prompt = "Enter your message here", Order = 3)]
    [Required(ErrorMessage = "Cannot leave this empty")]
    public string Message { get; set; } = null!;

    public string ServiceTitle { get; set; } = "Choose the service you are interested in";

}
