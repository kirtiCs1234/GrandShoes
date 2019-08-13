using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
   public partial class UserResetPassword
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Enter password")]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*(_|[~!@#$%^&*])).+$", ErrorMessage = "password must be at least 8 characters, less than or equal to 32 characters and the password must includes at least one symbol, at least one upper case letter, at least one lower case letter, and at least one number")]
        [StringLength(32, MinimumLength = 8)]
        public string Password { get; set; }     //for string password

        [Required]
        [System.ComponentModel.DataAnnotations.Compare("Password", ErrorMessage = "Password and Confirm Password doesn't match")]
        public string ConfirmPassword { get; set; }     //for confirm password, will be userd on registration and reset password page   

        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }


        public string FirstName { get; set; }

        public string MiddleName { get; set; }
        public string LastName { get; set; }


        public string Email { get; set; }

        public string Phone { get; set; }

        public string CurrentEncryption { get; set; }
        public string ValidationCode { get; set; }
        public string UserGuid { get; set; }
        public bool IsActive { get; set; }
        public bool IsVerified { get; set; }

        public int? RoleId { get; set; }
        public int? BranchId { get; set; }
    }
}
