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
    
    public partial class Staff
    {
        public int Id { get; set; }
        public Nullable<int> UserID { get; set; }
        public Nullable<int> DesignationID { get; set; }
        public Nullable<int> BranchID { get; set; }
        public Nullable<System.DateTime> JoiningDate { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public Nullable<int> LogId { get; set; }
        public Nullable<bool> FingerPrintAccess { get; set; }
    
        public virtual Branch Branch { get; set; }
        public virtual Designation Designation { get; set; }
        public virtual User User { get; set; }
    }
}