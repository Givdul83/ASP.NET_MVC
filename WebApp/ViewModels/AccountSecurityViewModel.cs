using WebApp.Models;

namespace WebApp.ViewModels;

public class AccountSecurityViewModel
{

    public string Title { get; set; } = "Security";  
    
    public AccountSecurity AccountSecurity { get; set; } = new AccountSecurity();

    public AccountDelete AccountDelete { get; set; } = new AccountDelete();
}
