﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Model
{
    public partial class UserModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Enter first name")]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Enter alphabets only")]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }
        
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Enter alphabets only")]
       
        public string MiddleName { get; set; }

      
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Enter alphabets only")]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }
        public string FullName { get { return FirstName + " " + MiddleName+ " " + LastName; } }

        [Required(ErrorMessage = "Enter email address")]
        [EmailAddress(ErrorMessage = "Invaid email address")]
        [Remote("CheckUserEmail", "User", "Admin", ErrorMessage = "There is an account that already uses this email", AdditionalFields = "Id")]
        public string Email { get; set; }

        //public string CountryDialingcode { get; set; }
        [Required(ErrorMessage = "Enter mobile phone")]
        [RegularExpression("([0-9]+)", ErrorMessage = "Please enter valid mobile phone")]
        [StringLength(10, MinimumLength = 10, ErrorMessage = "Please enter valid Phone number.")]
        [Remote("CheckUserPhoneNumber", "User", "Admin", ErrorMessage = "There is an account that already uses this phone number", AdditionalFields = "Id")]
        public string Phone { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        public string UserGuid { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public Nullable<bool> IsVerified { get; set; }
        public Nullable<bool> IsPrimaryAccountHolder { get; set; }
        [Required(ErrorMessage = "Please select user role")]
        public int? RoleID { get; set; }
        [Required(ErrorMessage = "Please select branch")]
        public int? BranchID { get; set; }
        [Required(ErrorMessage = "Please enter Email")]
        [EmailAddress(ErrorMessage = "Please enter valid email.")]
        public string xEmail { get; set; }
        [Required(ErrorMessage = "Please enter Password")]
        public string xPassword { get; set; }

        public virtual BranchModel Branch { get; set; }
        [Required(ErrorMessage = "Enter password")]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*(_|[~!@#$%^&*])).+$", ErrorMessage = "password must be at least 8 characters, less than or equal to 32 characters and the password must includes at least one symbol, at least one upper case letter, at least one lower case letter, and at least one number")]
        [StringLength(32, MinimumLength = 8)]
        public string Password { get; set; }
        [Required]
        [System.ComponentModel.DataAnnotations.Compare("Password", ErrorMessage = "Password and Confirm Password doesn't match")]
        public string ConfirmPassword { get; set; }    
        public RoleModel Role { get; set; }

        //for reset 
        [Required(ErrorMessage = "Please enter old password")]
        public string OldPassword { get; set; }


    }


}
