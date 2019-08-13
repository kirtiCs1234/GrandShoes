using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class WinnerReportModel
    {
        public WinnerReportModel()
        {
            salesOrderItemModels = new List<SalesOrderItemModel>();
        }
        public string FromProductSKU { get; set; }
        public string ToProductSKU { get; set; }
        public string FromStyleSKU { get; set; }
        public string ToStyleSKU { get; set; }
        public string BranchCode{get;set;}
        public bool IsMarkDown { get; set; }
        public string MarkDownCount { get; set; }
        public string FromReportDate { get; set; }
        public string ReportType { get; set; }
        public string ToReportDate { get; set; }
        public bool IsDistributionDate { get; set; }
        public string[] ProductList { get;set; }
        public string FromDistributionDate { get; set; }
        public string ToDistributionDate { get; set; }
        public List<BranchModel> Branches { get; set; }
        public List<SalesOrderItemModel> salesOrderItemModels { get; set; }
        public int GP { get; set; }
    }
}
