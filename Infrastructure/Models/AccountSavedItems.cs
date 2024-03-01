namespace Infrastructure.Models;

public class AccountSavedItems
{
    public string Title { get; set; } = null!;

    public string ImageUrl { get; set; } = null!;

    public string Author { get; set; } = null!;

    public decimal Price { get; set; } = 0!;

    public string Hours { get; set; } = null!;

    public string Likes { get; set; } = null!;

}
