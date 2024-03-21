
namespace Infrastructure.Models;

public class CourseModel
{
    public string Title { get; set; } = null!;

    public decimal? Price { get; set; }

    public decimal? DiscountPrice { get; set; }

    public int? Hours { get; set; }

    public bool IsBestSeller { get; set; }

    public decimal? LikesInNumbers { get; set; }

    public decimal? LikesInPercent { get; set; }

    public string? Author { get; set; }

    public string? CoursePictureUrl { get; set; }
}
