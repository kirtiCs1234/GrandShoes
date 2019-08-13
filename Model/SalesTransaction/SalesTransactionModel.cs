using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
   public class SalesTransactionModel
    {
        public int Id { get; set; }
        public Nullable<int> TransactionId { get; set; }
        public Nullable<int> SalesMenId { get; set; }
        public Nullable<decimal> Value { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public virtual StaffMemberModel StaffMember { get; set; }
        public virtual TransactionModel Transaction { get; set; }
    }
}
