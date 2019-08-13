using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public partial class StaffSearch
    {
        public int Id { get; set; }
        public Nullable<int> UserID { get; set; }
        public string FirstName { get; set;}
        public bool IsActive { get; set; }
        public int? Page { get; set; }
        public int PageSize { get; set; }
        public bool IsAdvanceSearch { get; set; }
        public virtual UserModel User { get; set; }
    }
}
