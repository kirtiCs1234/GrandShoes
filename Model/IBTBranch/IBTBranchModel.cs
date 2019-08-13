using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
   public class IBTBranchModel
    {
        public int Id { get; set; }
        public string IBtNumber { get; set; }
        public Nullable<int> TotalCarton { get; set; }
        public Nullable<int> TotalOrderedProduct { get; set; }
        public Nullable<int> AcceptedProduct { get; set; }
        public Nullable<System.DateTime> DateReceive { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public Nullable<int> UserId { get; set; }
        public Nullable<int> BranchId { get; set; }
        public Nullable<int> IBTSatusId { get; set; }
        public virtual BranchModel Branch { get; set; }
        public virtual UserModel User { get; set; }
        public virtual ICollection<IBTDetailModel> IBTDetails { get; set; }
    }
}
