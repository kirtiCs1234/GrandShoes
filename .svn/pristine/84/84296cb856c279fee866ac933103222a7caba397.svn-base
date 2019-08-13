using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class StockDistributionStatusModel
    {
        public int Id { get; set; }
        public string Status { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public Nullable<int> LogId { get; set; }

        public virtual LogModel Log { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<StockDistributionModel> StockDistributions { get; set; }
        public virtual StockDistributionStatusModel StockDistributionStatus1 { get; set; }
        public virtual StockDistributionStatusModel StockDistributionStatu1 { get; set; }
    }
}
