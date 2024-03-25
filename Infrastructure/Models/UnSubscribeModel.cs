
using System.ComponentModel.DataAnnotations;

namespace Infrastructure.Models;

public class UnSubscribeModel
{
    

    [Display(Prompt = "Your Email", Order = 0)]
    [DataType(DataType.EmailAddress)]
    [Required(ErrorMessage = "Email is required.")]
    [RegularExpression(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$", ErrorMessage = "Your email address is invalid.")]
    public string Email { get; set; } = null!;

    [Display(Name = "Yes, I want to be unsubscribed", Order = 1)]
    [Required(ErrorMessage = "You must confirm to be unsubscribed")]
    [Range(typeof(bool), "true", "true", ErrorMessage = "You must confirm to be unsubscribed")]
    public bool ConfirmBox { get; set; } = false;
}
