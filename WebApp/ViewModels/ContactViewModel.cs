using Infrastructure.Models;

namespace WebApp.ViewModels;

public class ContactViewModel
{
    public string Title { get; set; } = "Contact Us";

    public ContactUs ContactUs { get; set; } = new ContactUs();

    public ContactForm ContactForm { get; set; } = new ContactForm();


}
