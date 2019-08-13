using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class TransactionModel
    {
        public int Id { get; set; }
        public Nullable<int> SalesOrderId { get; set; }
        public Nullable<int> LayBayId { get; set; }
        public Nullable<decimal> Amount { get; set; }
        public string PaymentMode { get; set; }
        public string SalesType { get; set; }
        public Nullable<byte> IsCreditNoteRedeemed { get; set; }
        public string CreditNoteNumber { get; set; }
        public Nullable<int> CashierId { get; set; }
        public Nullable<decimal> AmountTendered { get; set; }
        public string Change { get; set; }
        public Nullable<System.DateTime> TransactionDate { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public virtual LayBaySaleModel LayBaySale { get; set; }
        public virtual SalesOrderModel SalesOrder { get; set; }
        public virtual ICollection<SalesTransactionModel> SalesTransactions { get; set; }
        public virtual StaffMemberModel StaffMember { get; set; }
    }
}
