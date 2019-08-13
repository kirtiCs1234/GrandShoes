using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class PromotionalDiscountModel
    {
        public int Id { get; set; }
        public Nullable<int> ProductID { get; set; }
        public Nullable<int> DiscountSummaryID { get; set; }
        public string MarkDownPrice { get; set; }
        public string Branches { get; set; }
        public string[] ProductList { get; set; }
        public string DiscountedPrice { get; set; }
        public virtual ICollection<DiscountBranchModel> DiscountBranches { get; set; }
        public virtual DiscountSummaryModel DiscountSummary { get; set; }
        public string[] BranchList { get; set; }
        public bool? IsActive { get; set; }
        public virtual ProductModel Product { get; set; }
        public List<BranchModel> BranchList1 { get; set; }
    }
}
