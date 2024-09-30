using System.ComponentModel.DataAnnotations;

namespace mvc2.ViewModels
{
    public class SignUpViewModel
    {
        public string FName { get; set; }
        public string LName { get; set; }

        [Required(ErrorMessage ="Email is required")]
        [EmailAddress(ErrorMessage ="Invalid email")]
        public string Email { get; set; }

        [Required(ErrorMessage ="Password is required")]
        [DataType(DataType.Password)]
        public string Password { get; set; }


        [Required(ErrorMessage = "Confirm password is required")]
        [DataType(DataType.Password)]
        [Compare(nameof(Password),ErrorMessage ="Confirm password does not match password")]
        public string ConfirmPassword { get; set; }


        [Required(ErrorMessage ="Its required to agree on the terms and conditions")]
        public bool IsAgree { get; set; } 


    }
}
