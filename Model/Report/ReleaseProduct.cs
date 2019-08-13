using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class ReleaseProduct
    {
        public int? ProductId { get; set; }
        public decimal? Amount { get; set; }
        public int? Quantity { get; set; }
        public decimal? TotalAmount { get; set; }
        public bool IsRelease { get; set; }

        public decimal? PaidAmount { get; set; }
        public int? SalesOrderId { get; set; }

        public SalesOrderModel SalesOrder { get; set; }
    }
}
