using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Model
{
   public partial class ProductStyleModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Please Enter Product SKU.")]
        [Remote("CheckProductSKU", "Product", "Admin", ErrorMessage = "There is an product sku that is already uses.", AdditionalFields = "Id")]
        [RegularExpression("\\d{4}", ErrorMessage = "Please Enter Four Digits Only.")]
        public string StyleSKU { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public virtual List<ProductModel> Products { get; set; }
        public virtual ICollection<SearchData> SearchDatas { get; set; }
        public virtual List<PurchaseOrderItemModel> PurchaseOrderItems { get; set; }
        public virtual ICollection<StockInventoryModel> StockInventories { get; set; }

    }
}
