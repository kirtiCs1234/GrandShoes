using Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public partial class StaffModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage ="Please Enter User.")]
        public Nullable<int> UserID { get; set; }
        [Required(ErrorMessage ="Please Enter Degignation.")]
        public Nullable<int> DesignationID { get; set; }
        [Required(ErrorMessage ="Please Enter Finger Print Access.")]
        public string FingerPrintAccess { get; set; }
        [Required(ErrorMessage ="Please Enter Branch.")]
        public Nullable<int> BranchID { get; set; }
        [Required(ErrorMessage ="Please Enter Joinig Date.")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy/MM/dd}")]
        public Nullable<System.DateTime> JoiningDate { get; set; }
        public Nullable<bool> IsActive { get; set; }

        public virtual BranchModel Branch { get; set; }
        public virtual DesignationModel Designation { get; set; }
        public virtual UserModel User { get; set; }
    }
}
