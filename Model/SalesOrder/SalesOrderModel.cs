using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class SalesOrderModel
    {
        public int Id { get; set; }
        public Nullable<int> BranchId { get; set; }
        public Nullable<int> SalesId { get; set; }
        public string OrderNumber { get; set; }
        public string PhoneNo { get; set; }
        public Nullable<decimal> TotalAmount { get; set; }
        public Nullable<int> TotalQuantity { get; set; }
        public string SaleType { get; set; }
        public Nullable<decimal> Discount { get; set; }
        public byte[] PromoCode { get; set; }
        public Nullable<System.DateTime> TransactionDate { get; set; }
        public string PaymentType { get; set; }
        public Nullable<int> PacketQuantity { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public virtual BranchModel Branch { get; set; }
        public virtual ICollection<ReleaseLayBayItemModel> ReleaseLayBayItems { get; set; }
        public virtual StaffMemberModel StaffMember { get; set; }
        public virtual ICollection<SalesOrderItemModel> SalesOrderItems { get; set; }
        public virtual ICollection<TransactionModel> Transactions { get; set; }
    }
}
