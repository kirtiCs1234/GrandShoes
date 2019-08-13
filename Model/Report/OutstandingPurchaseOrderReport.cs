using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Report
{
    public class OutstandingPurchaseOrderReport
    {
        public string Product { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public string PurchaseOrderNumber { get; set; }
        public int PurchaseOrderId { get; set; }
        public string Supplier { get; set; }
        public int SupplierId { get; set; }
    }
}
