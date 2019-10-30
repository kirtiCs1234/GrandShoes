﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class BuyerModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Enter buyer name")]
        [RegularExpression(@"[a-zA-Z_]+( [a-zA-Z_]+)*$", ErrorMessage = "Enter alphabets only")]
        [Display(Name = "Buyer Name")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Enter buy Limit")]
        [RegularExpression("^[0-9]+\\.?[0-9]*$", ErrorMessage = "Please enter only digits")]
        public Nullable<decimal> BuyLimit { get; set; }
        public Nullable<bool> IsActive { get; set; }
    }
}