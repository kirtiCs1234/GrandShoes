using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Model
{
    public partial class ProdSearch
    {
        public int Id { get; set; }
        public string ProductSKU { get; set; }
        public Nullable<decimal> CostPrice { get; set; }
        public int? SeasonID { get; set; }
        public string Colors { get; set; }
        public string StyleSKU { get; set; }
        // public int? ColorID { get; set; }
        public string AutocompleteProductSKU { get; set; }
		public string AutocompleteStyleSKU { get; set; }
		public string AutocompleteColorCode { get; set; }
        public string AutocompleteSeason { get; set; }
        public string AutocompleteSize { get; set; }
        // public List<ColorModel> AvailableColours { get; set; }
        public int? ProdCat1ID { get; set; }
        public int? ProdCat2ID { get; set; }
        public int? ProdCat3ID { get; set; }
        public int? ProdCat4ID { get; set; }
        public virtual ProductCat1Model ProductCat1 { get; set; }
        public virtual ProductCat2Model ProductCat2 { get; set; }
        public virtual ProductCat3Model ProductCat3 { get; set; }
        public virtual ProductCat4Model ProductCat4 { get; set; }
        public string PrimaryImage { get; set;}
        public Nullable<decimal> MinPrice { get; set; }
        public Nullable<decimal> MaxPrice { get; set; }
        public string AnyItem { get; set; }
        
        public Nullable<bool> IsActive { get; set; }
        public virtual SeasonModel Season { get; set; }
        public virtual ICollection<ColorModel> Color { get; set; }
        public virtual ProductStyleModel ProductStyle { get; set; }       
        public ProdSearch()
        {
            Color = new List<ColorModel>();
        }
        
        public int? Page { get; set; }
        public int PageSize { get; set; }
    }
}
