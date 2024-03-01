using System.ComponentModel.DataAnnotations;

namespace Infrastructure.Models;

public class AccountSecurity
{
    public string Title { get; set; } = "Password";

    

    [Display(Name = "Current Password", Prompt = "Enter password", Order = 0)]
    [DataType(DataType.Password)]
    [Required(ErrorMessage = "Password is required")]
    [RegularExpression("^(?=.*[a-z])(?=.*[A-Z])(?=.*\\d)(?=.*[^a-zA-Z\\d]).{8,}$", ErrorMessage = "Invalid password format")]
    public string CurrentPassword { get; set; } = null!;



    [Display(Name = "New Password", Prompt = "Enter new password", Order = 1)]
    [DataType(DataType.Password)]
    [Required(ErrorMessage = "Password is required")]
    [RegularExpression("^(?=.*[a-z])(?=.*[A-Z])(?=.*\\d)(?=.*[^a-zA-Z\\d]).{8,}$", ErrorMessage = "Invalid password format")]
    public string NewPassword { get; set; } = null!;

    [Display(Name = "Confirm New Password", Prompt = "Confirm new password", Order = 2)]
    [DataType(DataType.Password)]
    [Required(ErrorMessage = "Password must be confirmed")]
    [RegularExpression("^(?=.*[a-z])(?=.*[A-Z])(?=.*\\d)(?=.*[^a-zA-Z\\d]).{8,}$", ErrorMessage = "Invalid password format")]
    [Compare(nameof(NewPassword), ErrorMessage = "password dosent match")]
    public string ConfirmNewPassword { get; set; } = null!;
}
