using Infrastructure.Models;

namespace WebApp.ViewModels
{
    public class AccountSavedItemsViewModel
    {

        public string Title { get; set; } = "Saved Items";

        public AccountSavedItems AccountSavedItems { get; set; } = new AccountSavedItems();

        public AccountBasic AccountBasic { get; set; } = new AccountBasic();
    }
}