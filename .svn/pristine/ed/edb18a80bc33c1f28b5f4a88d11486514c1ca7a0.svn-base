using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class StockDistributionConfirmData
    {

        public StockDistributionConfirmData()
        {
            BranchDistribution = new List<StockDistributionModel>();
            ProductsList = new List<ProductModel>();
            ProductInventory = new List<StockInventoryModel>();
            
        }
        
        public StockDistributionSummaryModel StockDistributionSummaryModel { get; set; }
        public List<StockInventoryModel> ProductInventory { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public List<StockDistributionModel> BranchDistribution { get; set; }
        public List<ProductModel> ProductsList { get; set; }
        public int ProductId { get; set; }

    }
}
