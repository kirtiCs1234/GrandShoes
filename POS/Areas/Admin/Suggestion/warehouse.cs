using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS.Areas.Admin.Suggestion
{
	public class wareHouse
	{
		public wareHouse()
		{
			Data = new List<Suggestion>();
		}
		public double AvgSalePerDay { get; set; }
		public double AvgSaleDays { get; set; }
		public double TotalStockSaleDays { get; set; }
		public double MaxStockSaleDays { get; set; }
		public virtual List<Suggestion> Data { get; set; }
		public string Response { get; set; }
	}
}
