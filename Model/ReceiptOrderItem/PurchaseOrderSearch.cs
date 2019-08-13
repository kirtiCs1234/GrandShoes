using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
   public class PurchaseOrderSearch
    {
        public int PurchaseOrderId { get; set; }
        public string OrderNumber { get; set; }
        public Nullable<int> ProductStyleId { get; set; }
        public int ProductId { get; set; }
        public string SupplierName { get; set; }
        public bool IsActive { get; set; }
        public string autoCompleteProductStyleName { get; set; }
        public string autoCompleteProductName { get; set; }
        public virtual ProductStyleModel ProductStyle { get; set; }
    }
}
