using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
   public partial class StaffMemberSearch
   {
        public int Id { get; set; }
        public Nullable<int> UserId { get; set; }
        public Nullable<int> StaffRoleId { get; set; }
       
        public Nullable<int> StaffStatusId { get; set; }
        public Nullable<bool> IsActive { get; set; }
        
        public virtual StaffRoleModel StaffRole { get; set; }
        public virtual StaffStatusModel StaffStatus{ get; set; }
        public virtual UserModel User { get; set; }
        public string UserName { get; set; }
        public int? Page { get; set; }
        public int PageSize { get; set; }
        public bool IsAdvanceSearch { get; set; }
    }
}
