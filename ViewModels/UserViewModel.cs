using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Nackowskis.ViewModels
{
    public class UserViewModel
    {

        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string Email { get; set; }

        [DisplayName("Password")]
        [Required(ErrorMessage = "Password is required.")]
        [StringLength(100, ErrorMessage = "Must contain at least 8 characters.", MinimumLength = 6)]
        public string Password { get; set; }

        [Compare("Password", ErrorMessage = "Password does not match")]
        [StringLength(100, ErrorMessage = "Must contain at least 8 characters.", MinimumLength = 6)]
        public string ConfirmPassword { get; set; }


    }
}
