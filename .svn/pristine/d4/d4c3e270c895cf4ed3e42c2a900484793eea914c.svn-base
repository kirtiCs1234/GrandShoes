using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class CartonManagementModel
    {
        public CartonManagementModel()
        {
            StockDistributionList = new List<StockDistributionModel>();
        }
        public int Id { get; set; }
        [Required]
        public Nullable<int> DistributionSummaryID { get; set; }
        public Nullable<int> TotalItems { get; set; }
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy/MM/dd}")]
        public string PackDate { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public Nullable<int> BranchID { get; set; }
        public string BranchName { get; set; }
        public string IBTNumber { get; set; }
        public int? CartonCount { get; set; }
        public int? ScanCount { get; set; }
        [Required]
        public Nullable<int> CartonNumber { get; set; }
        public Nullable<bool> IsDispatched { get; set; }
        public string Barcode { get; set; }
        public int? IBTTotalStock { get; set; }
        public int? Page { get; set; }
        public string From { get; set; }
        public string Address { get; set; }
        public string Address1 { get; set; }
        public virtual List<StockDistributionModel> StockDistributionList { get; set; }
        public virtual BranchModel Branch { get; set; }
        public virtual List<CartonManagementDetailModel> CartonManagementDetails { get; set; }
        public virtual StockDistributionSummaryModel StockDistributionSummary { get; set; }
    
    }
}
