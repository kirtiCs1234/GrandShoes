using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{ 
    public partial class ReceiveOrderModel
    {
		public ReceiveOrderModel()
		{
			purchaseOrderItemList = new List<PurchaseOrderItemModel>();

		}
		public int Id { get; set; }
        public string ReceiptNumber { get; set; }
        public Nullable<int> PurchaseOrderId { get; set; }
        public Nullable<int> TotalQuantity { get; set; }
        public Nullable<decimal> TotalCost { get; set; }
        public Nullable<decimal> TotalVAT { get; set; }
        public string SupplierInvoice { get; set; }
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy/MM/dd}")]
        public Nullable<System.DateTime> ReceiptDate { get; set; }
        public Nullable<bool> ReceiptStatusId { get; set; }
        public Nullable<bool> IsActive { get; set; }
		public Nullable<bool> IsFinalize { get; set; }
		[Required(ErrorMessage = "Enter Product style")]
		public string autoCompleteProductStyleName { get; set; }
		[Required(ErrorMessage = "Enter Product name")]
		public string autoCompleteProductName { get; set; }
		public Nullable<int> ProductStyleId { get; set; }
		public virtual List<PurchaseOrderItemModel> purchaseOrderItemList { get; set; }
		public virtual PurchaseOrderModel PurchaseOrder { get; set; }
        public virtual List<ReceiptOrderItemModel> ReceiptOrderItems { get; set; }
    }
}
