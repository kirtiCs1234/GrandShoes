using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class ReceiptOrderViewModel
    {
        public ReceiptOrderViewModel()
        {
            IPagedList<PurchaseOrderModel> stu = null;
            PurchaseOrderList = stu;
            ReceiptOrderItems = new List<ReceiptOrderItemModel>();

        }

        public int Id { get; set; }
        public string ReceiptNumber { get; set; }
        public Nullable<int> PurchaseOrderId { get; set; }
        public Nullable<int> TotalQuantity { get; set; }
        public Nullable<decimal> TotalCost { get; set; }
        public Nullable<decimal> TotalVAT { get; set; }
        public string SupplierInvoice { get; set; }
        public Nullable<System.DateTime> ReceiptDate { get; set; }
        public Nullable<bool> ReceiptStatusId { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public Nullable<int> LogId { get; set; }

        public virtual LogModel Log { get; set; }
        public virtual PurchaseOrderModel PurchaseOrder { get; set; }
        public virtual ICollection<ReceiptOrderItemModel> ReceiptOrderItems { get; set; }


        public IPagedList<PurchaseOrderModel> PurchaseOrderList { get; set; }
    }
}
