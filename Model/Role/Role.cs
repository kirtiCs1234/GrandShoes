using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public partial  class RoleModel
    {
        
        public int Id { get; set; }
        public string RoleName { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public int? Page { get; set; }
        public virtual UserModel User { get; set; }
    }
}
