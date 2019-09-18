using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Model
{
    public class PurchaseOrderModel
    {
        public PurchaseOrderModel()
        {
            AllPurchaseList = new List<PurchaseOrderModel>();
             OrderedItems = new List<PurchaseOrderItemModel>();
        }

        public int Id { get; set; }
        public string OrderNumber { get; set; }
        [Required(ErrorMessage ="Please Enter Client Invoice Number.")]
        public string ClientInvoiceNumber { get; set; }
        [Required(ErrorMessage ="Please Enter Supplier.")]
        public int SupplierId { get; set; }
        [Required(ErrorMessage ="Please Enter Buyer.")]
        public int BuyerId { get; set; }
        [Required(ErrorMessage ="Please Enter Order Date.")]
        public string OrderDate { get; set; }
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public Nullable <DateTime> ExpectedDeliveryDate { get; set; }
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public Nullable<DateTime> FirstDeliveryDate { get; set; }
        [Required(ErrorMessage = "Please Enter Order Completion Date.")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public Nullable<DateTime> OrderCompletionDate { get; set; }
        public int? Quantity { get; set; }
        public decimal? Amount { get; set; }
        public decimal VatAmount { get; set; }
        public int PurchaseOrderStatusId { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public string Comment { get; set; }
        public string BuyerName { get; set; }
        public string SupplierName { get; set; }
		public Nullable<bool> IsFinalize { get; set; }
		public List<PurchaseOrderModel> AllPurchaseList { get; set; }
        public List<PurchaseOrderItemModel> OrderedItems = new List<PurchaseOrderItemModel>();
        public virtual SupplierModel Supplier { get; set; }
        public virtual BuyerModel Buyer { get; set; }
        public virtual PurchaseOrderStatus PurchaseOrderStatu { get; set; }
    }

    [MetadataType(typeof(PurchaseOrderModel))]
    public partial class PurchaseOrderClass
    {

    }
}
