using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Report
{
    public class GNRReciept
    {
        public string RecieptNumber { get; set; }
        public int TotalQuantity { get; set; }
        public decimal TotalCost { get; set; }
        public decimal TotalVat { get; set; }
        public Nullable<DateTime> RecieptDate { get; set; }
    }
}
