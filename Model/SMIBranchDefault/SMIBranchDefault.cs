using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
  public partial  class SMIBranchDefaultModel
  {
        public SMIBranchDefaultModel()
        {
            branches = new List<BranchModel>();
            BranchDestination = new List<SMIBranchDefaultModel>();
         
        }

        public int Id { get; set; }
        public Nullable<int> FromBranchId { get; set; }
        public Nullable<int> ToBranchId { get; set; }
        public Nullable<decimal> Distance { get; set; }
        public Nullable<bool> IsPreferredRoute { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public Nullable<int> LogId { get; set; }

        public virtual BranchModel Branch { get; set; }
        public virtual BranchModel Branch1 { get; set; }
       // public List<UserModel> mydata { get; set; }

        public List<BranchModel> branches { get; set; }
        //public List<SMIBranchDefaultModel> smiListModel = new List<SMIBranchDefaultModel>();
        public List<SMIBranchDefaultModel> BranchDestination { get; set; }

    }
}
