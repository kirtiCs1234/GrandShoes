using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Report
{
    public class DailySellSummaryModel
    {
        public int BranchId { get; set; }
        public string BranchCode { get; set; }
        public decimal CashSell { get; set; }
        public decimal LayBaySell { get; set; }
        public decimal Total { get; set; }
    }
}
