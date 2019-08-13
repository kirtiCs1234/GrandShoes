//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DAL
{
    using System;
    using System.Collections.Generic;
    
    public partial class PurchaseOrder
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public PurchaseOrder()
        {
            this.PendingItemReceipts = new HashSet<PendingItemReceipt>();
            this.PurchaseOrderItems = new HashSet<PurchaseOrderItem>();
            this.ReceiveOrders = new HashSet<ReceiveOrder>();
        }
    
        public int ID { get; set; }
        public string OrderNumber { get; set; }
        public string ClientInvoiceNumber { get; set; }
        public int SupplierId { get; set; }
        public int BuyerId { get; set; }
        public System.DateTime OrderDate { get; set; }
        public Nullable<System.DateTime> ExpectedDeliveryDate { get; set; }
        public Nullable<System.DateTime> FirstDeliveryDate { get; set; }
        public Nullable<System.DateTime> OrderCompletionDate { get; set; }
        public int Quantity { get; set; }
        public Nullable<decimal> Amount { get; set; }
        public decimal VatAmount { get; set; }
        public int PurchaseOrderStatusId { get; set; }
        public bool IsActive { get; set; }
        public Nullable<int> LogId { get; set; }
        public Nullable<bool> IsFinalize { get; set; }
    
        public virtual Buyer Buyer { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PendingItemReceipt> PendingItemReceipts { get; set; }
        public virtual PurchaseOrderStatu PurchaseOrderStatu { get; set; }
        public virtual Supplier Supplier { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PurchaseOrderItem> PurchaseOrderItems { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ReceiveOrder> ReceiveOrders { get; set; }
    }
}
