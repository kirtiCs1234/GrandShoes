﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Model
{
   public class SupplierModel
    {
        public int Id { get; set; }
        [Required]
        [Remote("CheckExistingSupplier", "Supplier", "Admin", ErrorMessage = "There is an supplier Code that already uses.", AdditionalFields = "Id")]
        public string Code { get; set; }
        [Required]
        //[RegularExpression(@"[a-zA-Z_]+( [a-zA-Z_]+)*$", ErrorMessage = "Enter alphabets only")]
        public string Name { get; set; }
        [Required]
        public string PermanentAddress1 { get; set; }
        [Required]
        public string PermanentAddress2 { get; set; }
        [Required]
        public string PermanentAddress3 { get; set; }
        [Required]
        public string PermanentCity { get; set; }
        [Required]
        public string PermanentCountry { get; set; }
        [Required]
        [RegularExpression("([0-9]+)", ErrorMessage = "Please enter only number")]
        public string PermanentPostalCode { get; set; }
        [Required]
        public string CorrespondanceAddress1 { get; set; }
        [Required]
        public string CorrespondanceAddress2 { get; set; }
        [Required]
        public string CorrespondanceAddress3 { get; set; }
        [Required]

        public string CorrespondanceCity { get; set; }
        [Required]
        public string CorrespondanceCountry { get; set; }
        [Required]
        [RegularExpression("([0-9]+)", ErrorMessage = "Please enter only number")]
        public string CorrespondancePostalCode { get; set; }
        [Required]
        [RegularExpression("([0-9]+)", ErrorMessage = "Please enter only number")]
        public Nullable<decimal> Limit { get; set; }
        [Required(ErrorMessage = "Enter Contact Number")]
        //[RegularExpression("([0-9]+)", ErrorMessage = "Please enter valid contact number")]
        [StringLength(10, MinimumLength = 10, ErrorMessage = "Please enter valid contact number.")]
       
        public string ContactNumber { get; set; }
        public string FaxNumber { get; set; }
        public int TotalCount { get; set; }
		public string ExcelFile { get; set; }
		//[DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyy/MM/dd}")]
        public string RegistrationDate { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public string Model1 { get; set; }
    }
}