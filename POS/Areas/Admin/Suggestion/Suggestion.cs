using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS.Areas.Admin.Suggestion
{
	public class Suggestion
	{
        public String BranchName { get; set; }
        public int BranchID { get; set; }
		public int ProductID { get; set; }
		public double SoldTotal { get; set; }
		public double SaleRate { get; set; }
		public double SalePerDay { get; set; }
		public double AvgSalePerMonth { get; set; }
		public double RemainingTotal { get; set; }
		public double StockToTransfer { get; set; }
		public DateTime ReceivedDate { get; set; }
		public DateTime FromDate { get; set; }
		public DateTime ToDate { get; set; }
		public int EffectiveDays { get; set; }
		public double DaysMinStockSold { get; set; }
		public double MaxDaysToStockSold { get; set; }

	}
}
