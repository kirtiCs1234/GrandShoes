using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
   public class OffersModel
    {
        public List<ProductModel> ProductList { get; set; }
        public OffersModel()
        {
            ProductList = new List<ProductModel>();
        }
        public string Colors { get; set; }
        public Nullable<int> ProductStyleId { get; set; }
        public Nullable<int> ColorId { get; set; }
        public Nullable<int> SeasonId { get; set; }
        public string AutocompleteProductSKU { get; set; }
		public string AutocompleteStyleSKU { get; set; }
		public string AutocompleteColorCode { get; set; }
        public string AutocompleteSeason { get; set; }
        public string AutocompleteSize { get; set; }
        public int? ProdCat1ID { get; set; }
        public int? ProdCat2ID { get; set; }
        public int? ProdCat3ID { get; set; }
        public int? ProdCat4ID { get; set; }
        public virtual ProductCat1Model ProductCat1 { get; set; }
        public virtual ProductCat2Model ProductCat2 { get; set; }
        public virtual ProductCat3Model ProductCat3 { get; set; }
        public virtual ProductCat4Model ProductCat4 { get; set; }
    }
}
