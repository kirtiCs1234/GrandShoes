using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Model
{
   public class UserLoginModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Enter email address.")]
        [EmailAddress(ErrorMessage = "Invaid email address.")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Enter password.")]
           public string Password { get; set; }     //for string password

        [Required]
        [System.ComponentModel.DataAnnotations.Compare("Password", ErrorMessage = "Password and Confirm Password doesn't match")]
        public string ConfirmPassword { get; set; }
        [Required(ErrorMessage = "Please enter old password")]
        public string OldPassword { get; set; }

        public bool IsActive { get; set; }
    }
}
