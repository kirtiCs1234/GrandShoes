using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class BranchStockReportModel
    {
        public BranchStockReportModel()
        {
            BranchList = new List<BranchModel>();
        }
        public int Id { get; set; }
        public string BranchCode { get; set; }
        public string Name { get; set; }
        public string Telephone { get; set; }
        public Nullable<int> ManagerId { get; set; }
        public Nullable<System.DateTime> DateOpen { get; set; }
        public Nullable<System.DateTime> DateClosed { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine3 { get; set; }
        public string PostalCode { get; set; }
        public string AreaCode { get; set; }
        public Nullable<bool> IsSendStock { get; set; }
        public Nullable<bool> IsClosed { get; set; }
        public Nullable<bool> IsHeadOffice { get; set; }
        public string StoreSize { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public virtual StockDistributionModel StockDistributionModel { get; set; }
        public virtual UserModel User { get; set; }
        public List<SMIBranchDefaultModel> BranchDestination { get; set; }
        public List<BranchModel> BranchList { get; set; }
        public int? BranchId { get; set; }
    }
}
