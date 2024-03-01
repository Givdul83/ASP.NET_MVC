

using Infrastructure.Models;

namespace WebApp.ViewModels;

public class AccountDetailsViewModel
{
    public string Title { get; set; } = "Account Details";

    public AccountBasic AccountBasic { get; set; } = new AccountBasic();

    public AccountAddress AccountAddress { get; set; } = new AccountAddress();


}
