using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
   public class StockEnquiryModel
    {
        public StockEnquiryModel()
        {
            BranchDistribution = new List<StockDistributionModel>();
            ProductsList = new List<ProductModel>();
            ProductInventoryList = new List<StockInventoryModel>();
            ProductStyleModelList = new List<ProductStyleModel>();
            ColorModelList = new List<ColorModel>();
            BranchModelList = new List<BranchModel>();
        }

        public StockDistributionSummaryModel StockDistributionSummaryModel { get; set; }
        public List<StockInventoryModel> ProductInventoryList { get; set; }
       
        public Nullable<bool> IsActive { get; set; }
        public List<StockDistributionModel> BranchDistribution { get; set; }
        public List<ProductModel> ProductsList { get; set; }
        public List<ProductStyleModel> ProductStyleModelList { get; set; }
        public List<ColorModel> ColorModelList { get; set; }
        public List<BranchModel> BranchModelList { get; set; }
        public string ProductSKU { get; set; }
        public string StyleSKU { get; set; }
        public string BranchName { get; set; }
		public string ColorCode { get; set; }

	}
}
