using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
   public partial class StaffRoleModel
    {
        public int Id { get; set; }
        public string RoleName { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public virtual ICollection<StaffMemberModel> StaffMembers { get; set; }
    }
}
