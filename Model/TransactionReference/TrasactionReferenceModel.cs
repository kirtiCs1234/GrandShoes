using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class TrasactionReferenceModel
    {
            public int Id { get; set; }
            public string Task { get; set; }
            public Nullable<bool> IsActive { get; set; }
            public virtual ICollection<StockWarehouseTransactionModel> StockWarehouseTransactions { get; set; }
    }
}
