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
    
    public partial class IBTBranch
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public IBTBranch()
        {
            this.IBTDetails = new HashSet<IBTDetail>();
        }
    
        public int Id { get; set; }
        public string IBtNumber { get; set; }
        public Nullable<int> TotalCarton { get; set; }
        public Nullable<int> TotalOrderedProduct { get; set; }
        public Nullable<int> AcceptedProduct { get; set; }
        public Nullable<System.DateTime> DateReceive { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public Nullable<int> UserId { get; set; }
        public Nullable<int> BranchId { get; set; }
        public Nullable<int> IBTSatusId { get; set; }
    
        public virtual Branch Branch { get; set; }
        public virtual User User { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<IBTDetail> IBTDetails { get; set; }
    }
}