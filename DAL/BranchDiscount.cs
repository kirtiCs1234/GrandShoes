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
    
    public partial class BranchDiscount
    {
        public int Id { get; set; }
        public Nullable<int> BranchID { get; set; }
        public Nullable<int> DiscountID { get; set; }
        public Nullable<bool> IsActive { get; set; }
    
        public virtual Branch Branch { get; set; }
        public virtual Discount Discount { get; set; }
    }
}