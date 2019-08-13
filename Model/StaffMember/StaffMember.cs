using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public partial class StaffMemberModel
    {
        public int Id { get; set; }
        [Required]
        public Nullable<int> UserId { get; set; }
       
        [Required(ErrorMessage = "please enter date.")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy/MM/dd}")]
        public Nullable<System.DateTime> JoiningDate { get; set; }
        
        public string ProfilePic { get; set; }
        [Required]
        public Nullable<int> StaffStatusId { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public bool IsFingurPrintAccess { get; set; }

        public virtual StaffRoleModel StaffRole { get; set; }
        public virtual StaffStatusModel StaffStatu { get; set; }
        public virtual UserModel User { get; set; }
        public string Name { get; set; }
    }
}
