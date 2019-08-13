using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class LayBaySaleModel
    {
       
        public int Id { get; set; }
        public string LayBayNo { get; set; }
        public Nullable<int> SalesOrderId { get; set; }
        public Nullable<System.DateTime> DatePurchased { get; set; }
        public Nullable<decimal> InitValue { get; set; }
        public Nullable<decimal> CountValue { get; set; }
        public Nullable<int> CustomerId { get; set; }
        public Nullable<decimal> DepositeAmount { get; set; }
        public Nullable<decimal> DueAmount { get; set; }
        public string PaymentType { get; set; }
        public Nullable<int> StatusId { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public virtual ICollection<ReleaseLayBayItemModel> ReleaseLayBayItems { get; set; }
        public virtual ICollection<TransactionModel> Transactions { get; set; }
    }
}
