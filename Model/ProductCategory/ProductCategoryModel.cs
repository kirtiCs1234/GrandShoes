using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Model
{
   public partial class ProductCategoryModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage ="Please Enter Product Category Code.")]
        [Remote("CheckProductCategoryCode","ProductCategory", "Admin", ErrorMessage = "There is an code that is already uses.", AdditionalFields="Id")]
        public string Code { get; set; }
        public string Category1 { get; set; }
		public string Category2 { get; set; }
		public string Category3 { get; set; }
		public string Category4 { get; set; }
		public Nullable<bool> IsActive { get; set; }
    }
}
