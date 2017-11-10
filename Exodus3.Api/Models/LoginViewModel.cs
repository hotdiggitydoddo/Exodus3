using System;
using System.ComponentModel.DataAnnotations;

namespace Exodus3.Api.Models
{
    public class LoginViewModel
    {
        [Required, Display(Name = "Email")]
        public string Email { get; set; }

        [Required, Display(Name = "Password")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
