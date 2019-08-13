using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Model
{
    public partial class DesignationModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Enter designation name")]
        [RegularExpression(@"[a-zA-Z_]+( [a-zA-Z_]+)*$", ErrorMessage = "Enter alphabets only")]
        [Remote("CheckDesignationName", "Designation", "Admin", ErrorMessage = "There is an designation name that already uses this name", AdditionalFields = "Id")]
        public string DesignationName { get; set; }
        public Nullable<bool> IsActive { get; set; }

    }
}
