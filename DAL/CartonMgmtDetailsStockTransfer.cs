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
    
    public partial class CartonMgmtDetailsStockTransfer
    {
        public int Id { get; set; }
        public Nullable<int> CartonManagementID { get; set; }
        public Nullable<int> ProductID { get; set; }
        public Nullable<int> Z01 { get; set; }
        public Nullable<int> Z02 { get; set; }
        public Nullable<int> Z03 { get; set; }
        public Nullable<int> Z04 { get; set; }
        public Nullable<int> Z05 { get; set; }
        public Nullable<int> Z06 { get; set; }
        public Nullable<int> Z07 { get; set; }
        public Nullable<int> Z08 { get; set; }
        public Nullable<int> Z09 { get; set; }
        public Nullable<int> Z10 { get; set; }
        public Nullable<int> Z11 { get; set; }
        public Nullable<int> Z12 { get; set; }
        public Nullable<int> Z13 { get; set; }
        public Nullable<int> Z14 { get; set; }
        public Nullable<int> Z15 { get; set; }
        public Nullable<int> Z16 { get; set; }
        public Nullable<int> Z17 { get; set; }
        public Nullable<int> Z18 { get; set; }
        public Nullable<int> Z19 { get; set; }
        public Nullable<int> Z20 { get; set; }
        public Nullable<int> Z21 { get; set; }
        public Nullable<int> Z22 { get; set; }
        public Nullable<int> Z23 { get; set; }
        public Nullable<int> Z24 { get; set; }
        public Nullable<int> Z25 { get; set; }
        public Nullable<int> Z26 { get; set; }
        public Nullable<int> Z27 { get; set; }
        public Nullable<int> Z28 { get; set; }
        public Nullable<int> Z29 { get; set; }
        public Nullable<int> Z30 { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public Nullable<System.DateTime> CreatedOn { get; set; }
        public Nullable<System.DateTime> UpdatedOn { get; set; }
    
        public virtual CartonMgmtStockTransfer CartonMgmtStockTransfer { get; set; }
        public virtual Product Product { get; set; }
    }
}