using System.ComponentModel.DataAnnotations;

namespace mvc2.ViewModels
{
	public class ForgetPasswordViewModel
	{
		[Required(ErrorMessage = "Email is required")]
		[EmailAddress(ErrorMessage = "Invalid email")]
		public string Email { get; set; }
	}
}
