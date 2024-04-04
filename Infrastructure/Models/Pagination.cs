namespace Infrastructure.Models;

public class Pagination
{
    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 10;
    public int TotalItems { get; set; }
    public int CurrentPage { get; set; }
    public int TotalPages => (int)Math.Ceiling(decimal.Divide(TotalItems, PageSize));
}