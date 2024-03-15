

using System.ComponentModel.DataAnnotations;

namespace Infrastructure.Models;

public class SubscribeEmail
{
	[Display(Name = "Email", Prompt = "Enter Email address", Order = 0)]
	[DataType(DataType.EmailAddress)]
	[Required(ErrorMessage = "Invalid Email address")]
	[RegularExpression(@"^[a-zA-Z0-9._%+-]{1,}@[a-zA-Z0-9.-]{2,}\.[a-zA-Z]{2,}$", ErrorMessage = "Invalid email address")]
	public string Email { get; set; } = null!;
}
