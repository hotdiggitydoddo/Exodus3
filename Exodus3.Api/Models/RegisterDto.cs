using System;
using System.ComponentModel.DataAnnotations;

namespace Exodus3.Api.Models
{
    public class RegisterDto
    {
        [Required, Display(Name = "Email")]
        public string Email { get; set; }

        [Required, Display(Name = "Password")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm Password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }
}
