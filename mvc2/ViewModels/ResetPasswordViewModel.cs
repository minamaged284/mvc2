using System.ComponentModel.DataAnnotations;

namespace mvc2.ViewModels
{
	public class ResetPasswordViewModel
	{
		[Required(ErrorMessage = "Password is required")]
		[DataType(DataType.Password)]
		public string Password { get; set; }


		[Required(ErrorMessage = "Confirm password is required")]
		[DataType(DataType.Password)]
		[Compare(nameof(Password), ErrorMessage = "Confirm password does not match password")]
		public string ConfirmPassword { get; set; }
	}
}
