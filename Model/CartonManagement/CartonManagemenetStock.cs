using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class CartonManagementStockModel
    {
        public CartonManagementStockModel()
        {
            CartonList = new List<CartonManagementModel>();
            StockList = new List<StockDistributionModel>();
        }
        public List<StockDistributionModel> StockList { get; set; }
        public List<CartonManagementModel> CartonList { get; set; }
        public int StockDistributionSummaryId { get; set; }
        public int BranchId { get; set; }
    }
}
