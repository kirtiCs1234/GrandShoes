using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Model
{
   public class DiscountBranchModel
    {
        public int Id { get; set; }
        public Nullable<int> DiscountID { get; set; }
        public Nullable<int> BranchID { get; set; }
        public string[] Branches { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public virtual BranchModel Branch { get; set; }
        public virtual PromotionalDiscountModel PromotionalDiscount { get; set; }
    }
}
