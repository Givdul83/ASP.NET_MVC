using Infrastructure.Models;

namespace WebApp.ViewModels;

public class UnSubscribeViewModel
{
    public string Title { get; set; } = "Unsubscribe";

    public string Description { get; set; } = "Please enter the email address you want to unsubscribe";
    public UnSubscribeModel? UnSubscribeModel { get; set; }
}
