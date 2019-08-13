//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DAL
{
    using System;
    using System.Collections.Generic;
    
    public partial class PagePermission
    {
        public int Id { get; set; }
        public Nullable<int> PageId { get; set; }
        public Nullable<int> UserId { get; set; }
        public Nullable<int> RoleId { get; set; }
        public string PageAction { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public Nullable<bool> IsAdminPage { get; set; }
    
        public virtual PageName PageName { get; set; }
        public virtual Role Role { get; set; }
        public virtual User User { get; set; }
    }
}
