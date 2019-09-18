using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class StockTransactionTypeModel
    {
        public int Id { get; set; }
        public string Status { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public Nullable<int> LogId { get; set; }
        public virtual ICollection<StockBranchTransactionModel> StockBranchTransactions { get; set; }
       
    }
}
