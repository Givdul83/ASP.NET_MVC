using WebApp.Models;

namespace WebApp.ViewModels;

public class NotFoundViewModel
{
    public string Title { get; set; } = "Not Found";

    public NotFound NotFound= new NotFound();
}
