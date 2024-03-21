
namespace Infrastructure.Models;

public class CourseModel
{
    public string Title { get; set; } = null!;

    public string? Price { get; set; }

    public string? DiscountPrice { get; set; }

    public string? Hours { get; set; }

    public bool IsBestSeller { get; set; }

    public string? LikesInNumbers { get; set; }

    public string? LikesInPercent { get; set; }

    public string? Author { get; set; }

    public string? CoursePictureUrl { get; set; }
}
