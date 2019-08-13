using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class PagePermissionModel
    {
        public int Id { get; set; }
        public Nullable<int> PageId { get; set; }
        public Nullable<int> UserId { get; set; }
        public Nullable<int> RoleId { get; set; }
        public string PageAction { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public Nullable<bool> IsAdminPage { get; set; }
        public virtual PageNameModel PageName { get; set; }
        public virtual RoleModel Role { get; set; }
        public virtual UserModel User { get; set; }
    }
}
