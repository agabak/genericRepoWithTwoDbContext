using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Identity.ViewModels
{
    public class LoginViewModel
    {
        [Required]
        [Display(Name ="Username")]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
        [Display(Name ="Remember Me")]
        public bool IsRememberMe { get; set; }
    }
}
