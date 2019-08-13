using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class DictModel
    {
        public int? ProductId { get; set; }
        public int? PurchaseOrderId { get; set; }
        public string SupplierStyle { get; set; }
        public Dictionary<string, string> QuantitySize { get; set; }
        public Dictionary<string, string> ItemSize { get; set; }
        public Dictionary<string, string> CostSize { get; set; }
        public  ProductModel Product { get; set; }
        public  PurchaseOrderModel PurchaseOrder { get; set; }
    }
}
