using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class DiscountAddModel
    {
        [Required]
        public DiscountModel Discounts { get; set; }
        [Required]
        public List<BranchDiscountModel> BranchDiscountList { get; set; }
        [Required]
        public List<BranchModel> BranchList1 { get; set; }
        [Required]
        public List<DiscountModel> DiscountList { get; set; }
        public DiscountAddModel()
        {
            BranchDiscountList = new List<BranchDiscountModel>();
            BranchList1 = new List<BranchModel>();
            DiscountList = new List<DiscountModel>();
        }
        public int? FromProductSKU { get; set; }
        public int? ToProductSKU { get; set; }
        public int? FromStyleSKU { get; set; }
        public int? ToStyleSKU { get; set; }
        [Required]
        public string[] BranchList { get; set; }
        [Required]
        public string AutocompleteProductSKU { get; set; }
        [Required]
        public string AutocompleteProductStyleSKU { get; set; }
        public bool IsActive { get; set; }
    }
}
