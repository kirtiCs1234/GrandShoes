using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Model
{
   public partial class ProductSourceModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage ="Enter Product Source")]
        [RegularExpression(@"[a-zA-Z_]+( [a-zA-Z_]+)*$", ErrorMessage = "Enter alphabets only")]
        [Remote("CheckSource", "ProductSource", "Admin", ErrorMessage = "There is an source that is already.", AdditionalFields = "Id")]

        public string Source { get; set; }
        public Nullable<bool> IsActive { get; set; }
    }
}
