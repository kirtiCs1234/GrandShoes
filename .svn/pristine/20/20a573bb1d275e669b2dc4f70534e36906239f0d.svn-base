using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class DiscountModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage ="Please Enter Product SKU.")]
        public string ProductSKU { get; set; }
        [Required(ErrorMessage ="Please Enter Product Style SKU.")]
        public string StyleSKU { get; set; }
        public Nullable<bool> IsFlatDiscount { get; set; }
        public Nullable<bool> IsQuantityDiscount { get; set; }

        public string MinimumQuantity { get; set; }
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy/MM/dd}")]
        [Required(ErrorMessage ="Please Enter Start Date.")]
        public Nullable<System.DateTime> DateFrom { get; set; }
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy/MM/dd}")]
        [Required(ErrorMessage ="Please Enter End Date.")]
        public Nullable<System.DateTime> DateTo { get; set; }
        [Required(ErrorMessage ="Please Enter Percent Discount.")]
        public string PercentDiscount { get; set; }
        [Required(ErrorMessage ="Please Enter Text.")]
        public string Text { get; set; }
        public string Branches { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public virtual ICollection<BranchDiscountModel> BranchDiscounts { get; set; }
    }
}
