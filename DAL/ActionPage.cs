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
    
    public partial class ActionPage
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ActionPage()
        {
            this.ActionPage1 = new HashSet<ActionPage>();
        }
    
        public int Id { get; set; }
        public Nullable<int> ActionNameId { get; set; }
        public Nullable<int> CompanyPageId { get; set; }
        public string ActionPages { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public Nullable<int> RoleId { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ActionPage> ActionPage1 { get; set; }
        public virtual ActionPage ActionPage2 { get; set; }
    }
}