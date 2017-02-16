using System.ComponentModel.DataAnnotations;

namespace Eventer.Model.BindingModel.Account
{
    public class ChangePasswordBindingModel
    {
        [Required(ErrorMessage = "OldPassword is required")]
        [DataType(DataType.Password)]
        [Display(Name = "Current password")]
        public string OldPassword { get; set; }

        [Required(ErrorMessage = "NewPassword is required")]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "New password")]
        public string NewPassword { get; set; }

        [Required(ErrorMessage = "ConfirmPassword is required")]
        [DataType(DataType.Password)]
        [Display(Name = "Confirm new password")]
        [Compare("NewPassword", ErrorMessage = "The new password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }

    public class RegisterBindingModel
    {
        [Required(ErrorMessage = "Email is required")]
        [Display(Name = "Email")]
        [DataType(DataType.EmailAddress, ErrorMessage = "This is not valid email address")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Required(ErrorMessage = "ConfirmPassword is required")]
        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }

    public class AuthorizationBindingModel
    {
        [Required(ErrorMessage = "Email is required")]
        [Display(Name = "Email")]
        [DataType(DataType.EmailAddress, ErrorMessage = "This is not valid email address")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [Display(Name = "Password")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
