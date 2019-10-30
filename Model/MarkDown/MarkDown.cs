﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public partial class MarkDownModel
    {
        public MarkDownModel()
        {
            MarkDownBranches = new List<MarkDownBranchModel>();
        }
        public int Id { get; set; }
        
        public string ProductSKU { get; set; }
        
        public string StyleSKU { get; set; }
        [Required(ErrorMessage = "Please Enter Original Selling Price")]

        public Nullable<decimal> OriginalSellingPrice { get; set; }
        [Required(ErrorMessage = "Please Enter Percentage Decrease.")]
        public Nullable<int> PercentageDecrease { get; set; }
        [Required(ErrorMessage ="Please Enter Is Percentage Original Price.")]
        public Nullable<bool> IsPercentageOriginalPrice { get; set; }
        
        public Nullable<decimal> NewSellingPrice { get; set; }
        
        public Nullable<decimal> NewCashPrice { get; set; }
        [Required(ErrorMessage ="Please Enter Effective Date.")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public string EffectiveDate { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public string Guid { get; set; }
        public string Branches { get; set; }
        public virtual List<MarkDownBranchModel> MarkDownBranches { get; set; }
    }
}