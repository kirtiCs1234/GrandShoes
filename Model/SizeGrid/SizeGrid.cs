using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Model
{
    public class SizeGridModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage ="Please Enter Grid Number.")]
        [Remote("CheckGridNo", "SizeGrid", "Admin", ErrorMessage = "There is an grid no that is already uses.", AdditionalFields = "Id")]
        public string GridNumber { get; set; }
        public Nullable<decimal> Z01 { get; set; }
        public Nullable<decimal> Z02 { get; set; }
        public Nullable<decimal> Z03 { get; set; }
        public Nullable<decimal> Z04 { get; set; }
        public Nullable<decimal> Z05 { get; set; }
        public Nullable<decimal> Z06 { get; set; }
        public Nullable<decimal> Z07 { get; set; }
        public Nullable<decimal> Z08 { get; set; }
        public Nullable<decimal> Z09 { get; set; }
        public Nullable<decimal> Z10 { get; set; }
        public Nullable<decimal> Z11 { get; set; }
        public Nullable<decimal> Z12 { get; set; }
        public Nullable<decimal> Z13 { get; set; }
        public Nullable<decimal> Z14 { get; set; }
        public Nullable<decimal> Z15 { get; set; }
        public Nullable<decimal> Z16 { get; set; }
        public Nullable<decimal> Z17 { get; set; }
        public Nullable<decimal> Z18 { get; set; }
        public Nullable<decimal> Z19 { get; set; }
        public Nullable<decimal> Z20 { get; set; }
        public Nullable<decimal> Z21 { get; set; }
        public Nullable<decimal> Z22 { get; set; }
        public Nullable<decimal> Z23 { get; set; }
        public Nullable<decimal> Z24 { get; set; }
        public Nullable<decimal> Z25 { get; set; }
        public Nullable<decimal> Z26 { get; set; }
        public Nullable<decimal> Z27 { get; set; }
        public Nullable<decimal> Z28 { get; set; }
        public Nullable<decimal> Z29 { get; set; }
        public Nullable<decimal> Z30 { get; set; }
		public string ExcelFile { get; set; }
		public Nullable<bool> IsActive { get; set; }
       
    }
}
