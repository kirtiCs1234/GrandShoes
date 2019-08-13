using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class CartonManagementReportModel
    {
        public CartonManagementReportModel()
        {
            CartonManagement = new List<CartonManagementModel>();
            CartonManagementDetails = new List<CartonManagementDetailModel>();
        }
        public int Id { get; set; }
        public Nullable<int> StockDistributionSummaryId { get; set; }
        public Nullable<int> BranchId { get; set; }
        public Nullable<int> TotalItems { get; set; }
        public Nullable<System.DateTime> PackDate { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public List<CartonManagementModel> CartonManagement { get; set; }
        public virtual BranchModel Branch { get; set; }
        public virtual List<CartonManagementDetailModel> CartonManagementDetails { get; set; }
        public virtual StockDistributionSummaryModel StockDistributionSummary { get; set; }
    }
}
