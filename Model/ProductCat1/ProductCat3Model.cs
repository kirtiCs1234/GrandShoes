using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class ProductCat3Model
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Please Enter Code")]
        public string Code { get; set; }
        public Nullable<bool> IsActive { get; set; }
        [Required(ErrorMessage = "Please Enter Category Name")]
        public string CateName { get; set; }
        public Nullable<System.DateTime> CreatedOn { get; set; }
        public Nullable<System.DateTime> UpdatedOn { get; set; }
        public virtual ICollection<ProductModel> Products { get; set; }
    }
}
