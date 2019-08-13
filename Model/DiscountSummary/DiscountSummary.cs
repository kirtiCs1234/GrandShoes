using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
   public class DiscountSummaryModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage ="Please Enter ProductSKU")]
        public string FromProductSKU { get; set; }
        [Required(ErrorMessage = "Please Enter ProductSKU")]
        public string ToProductSKU { get; set; }
        [Required(ErrorMessage ="Please Enter StyleSKU")]
        public string FromStyleSKU { get; set; }
        [Required(ErrorMessage = "Please Enter StyleSKU")]
        public string ToStyleSKU { get; set; }
        public string FromPrice { get; set; }
        public string ToPrice { get; set; }
        public string[] ProductList { get; set; }
        [Required(ErrorMessage ="Please Enter FromDate")]
        public string FromDate { get; set; }
        [Required(ErrorMessage = "Please Enter ToDate")]
        public string ToDate { get; set; }
        [Required(ErrorMessage ="Please Select Discount Value")]
        public Nullable<decimal> DiscountValue { get; set; }
        [Required(ErrorMessage ="Please Enter Discount Type")]
        public string DiscountType { get; set; }
        public bool? IsActive { get; set; }
        public Nullable<int> TemplateID { get; set; }
        public virtual ICollection<PromotionalDiscountModel> PromotionalDiscounts { get; set; }
    }
}
