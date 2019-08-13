using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public partial class AreaModel
    {
        
        public int Id { get; set; }

        [Required(ErrorMessage = "Please Enter Area Name")]
        public string Name { get; set; }
        [Required(ErrorMessage ="Please Enter Manager Name")]
        public Nullable<int> ManagerID { get; set; }
        public Nullable<bool> IsActive { get; set; }
        
        public virtual UserModel User { get; set; }
           }
}
