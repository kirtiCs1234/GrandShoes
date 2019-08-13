using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Report
{
    public class DailySellReport
    {
        //public string OrderNumber { get; set; }
        public string Product { get; set; }
        public int SalesQuantity { get; set; }
        public decimal SalesPrice { get; set; }
        public int LayBayQuantity { get; set; }
        public decimal LayBayPrice { get; set; }
        public int TotalQuantity { get; set; }
        public decimal TotalPrice { get; set; }
    }
}
