using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class CartonDistributionModel
    {
        public CartonManagementModel CartonManagement { get; set; }
        public CartonManagementDetailModel CartonManagementDetail { get; set; }
        public StockDistributionModel StockDistribution { get; set; }
        public List<ProductModel> Product { get; set; }
        public int? ProductId { get; set; }
    }
}
