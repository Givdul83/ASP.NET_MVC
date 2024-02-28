using System.ComponentModel.DataAnnotations;

namespace WebApp.Models;

public class AccountDelete
{
    public string Title { get; set; } = "Delete Account";

    public string Description { get; set; } = "When you delete your account, your public profile will be deactivated immediately." +
        " If you change your mind before the 14 days are up, sign in with your email and password, " +
        "and we’ll send you a link to reactivate your account.";

    public string ConfirmDelete { get; set; } = "Yes, I want to delete my account";

    [Display(Name = "Yes i want to delete my account", Order = 3)]
    [Required(ErrorMessage = "You must agree check the box to delete the account")]
    [Range(typeof(bool), "true", "true", ErrorMessage = "You must agree check the box to delete the account")]
    public bool DeleteAccount { get; set; } = false;


}
