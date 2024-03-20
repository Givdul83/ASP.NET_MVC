

using System.ComponentModel.DataAnnotations;

namespace Infrastructure.Models;

public class SubscribeEmail
{
    [Display(Name = "Daily Newsletter", Order = 0)]
    public bool DailyNewsletter { get; set; }

    [Display(Name = "Advertising Updates", Order = 1)]
    public bool AdvertisingUpdates { get; set; }

    [Display(Name = "Week in Review", Order = 2)]
    public bool WeekinReview { get; set; }

    [Display(Name = "Event Updates", Order = 3)]
    public bool EventUpdates { get; set; }

    [Display(Name = "StartupsWeekly", Order = 4)]
    public bool StartupsWeekly { get; set; }

    [Display(Name = "Podcasts", Order = 5)]
    public bool Podcasts { get; set; }

    [Display(Prompt = "Your Email", Order = 6)]
    [DataType(DataType.EmailAddress)]
    [Required(ErrorMessage = "Email is required.")]
    [RegularExpression(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$", ErrorMessage = "Your email address is invalid.")]
    public string Email { get; set; } = null!;

}
