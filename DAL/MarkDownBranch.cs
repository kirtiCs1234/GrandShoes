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
    
    public partial class MarkDownBranch
    {
        public int Id { get; set; }
        public Nullable<int> MarkDownID { get; set; }
        public Nullable<int> BranchID { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public Nullable<System.DateTime> CreatedOn { get; set; }
        public Nullable<System.DateTime> UpdatedOn { get; set; }
    
        public virtual Branch Branch { get; set; }
        public virtual MarkDown MarkDown { get; set; }
    }
}
