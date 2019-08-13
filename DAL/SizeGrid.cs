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
    
    public partial class SizeGrid
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public SizeGrid()
        {
            this.Products = new HashSet<Product>();
            this.PurchaseOrderItems = new HashSet<PurchaseOrderItem>();
        }
    
        public int Id { get; set; }
        public string GridNumber { get; set; }
        public Nullable<decimal> Z01 { get; set; }
        public Nullable<decimal> Z02 { get; set; }
        public Nullable<decimal> Z03 { get; set; }
        public Nullable<decimal> Z04 { get; set; }
        public Nullable<decimal> Z05 { get; set; }
        public Nullable<decimal> Z06 { get; set; }
        public Nullable<decimal> Z07 { get; set; }
        public Nullable<decimal> Z08 { get; set; }
        public Nullable<decimal> Z09 { get; set; }
        public Nullable<decimal> Z10 { get; set; }
        public Nullable<decimal> Z11 { get; set; }
        public Nullable<decimal> Z12 { get; set; }
        public Nullable<decimal> Z13 { get; set; }
        public Nullable<decimal> Z14 { get; set; }
        public Nullable<decimal> Z15 { get; set; }
        public Nullable<decimal> Z16 { get; set; }
        public Nullable<decimal> Z17 { get; set; }
        public Nullable<decimal> Z18 { get; set; }
        public Nullable<decimal> Z19 { get; set; }
        public Nullable<decimal> Z20 { get; set; }
        public Nullable<decimal> Z21 { get; set; }
        public Nullable<decimal> Z22 { get; set; }
        public Nullable<decimal> Z23 { get; set; }
        public Nullable<decimal> Z24 { get; set; }
        public Nullable<decimal> Z25 { get; set; }
        public Nullable<decimal> Z26 { get; set; }
        public Nullable<decimal> Z27 { get; set; }
        public Nullable<decimal> Z28 { get; set; }
        public Nullable<decimal> Z29 { get; set; }
        public Nullable<decimal> Z30 { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public Nullable<System.DateTime> CreatedOn { get; set; }
        public Nullable<System.DateTime> UpdatedOn { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Product> Products { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PurchaseOrderItem> PurchaseOrderItems { get; set; }
    }
}
