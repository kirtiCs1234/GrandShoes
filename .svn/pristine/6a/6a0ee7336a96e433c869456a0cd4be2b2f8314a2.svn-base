using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Model
{
   public class StockDistributionViewModel
    {
        public StockDistributionViewModel()
        {
            BranchDistribution = new List<StockDistributionModel>();
            ProductsList = new List<ProductModel>();
            BranchList = new List<BranchModel>(); 
        }
        
        public StockDistributionSummaryModel StockDistributionSummaryModel { get; set; }
       public  StockInventoryModel ProductInventory { get; set; }
        //public  string BranchName { get; set; }
        //public string BranchCode { get; set; }
        public Nullable<bool> IsActive { get; set; }
       public  List<StockDistributionModel> BranchDistribution { get; set; }
       public List<ProductModel> ProductsList{ get ; set ; }
        public List<BranchModel> BranchList { get; set; }
       public int ProductId { get; set; }
       public ProductModel Product { get; set; } 
    }
}
