using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public partial class DropDownProductListModel
    {
        public DropDownProductListModel()
        {
            ColorModelList = new List<ColorModel>();
            TemplateModelList = new List<TemplateModel>();
            SupplierModelList = new List<SupplierModel>();
            ProductCategoryModelList = new List<ProductCategoryModel>();
            ProductGroupModelList = new List<ProductGrpModel>();
            BuyerModelList = new List<BuyerModel>();
            SeasonModelList = new List<SeasonModel>();
            freeGiftList = new List<ProductModel>();
            Cat1List = new List<ProductCat1Model>();
            Cat2List = new List<ProductCat2Model>();
            Cat3List = new List<ProductCat3Model>();
            Cat4List = new List<ProductCat4Model>();
        }
        public List<ColorModel> ColorModelList { get; set; }
        public List<TemplateModel> TemplateModelList { get; set; }
        public List<SupplierModel> SupplierModelList { get; set; }
        public List<SizeGridModel> SizeGridModelList { get; set; }
        public List<ProductCategoryModel> ProductCategoryModelList { get; set; }
        public List<ProductGrpModel> ProductGroupModelList { get; set; }
        public List<ProductSourceModel> ProductSourceModelList { get; set; }
        public List<BuyerModel> BuyerModelList { get; set; }
        public List<SeasonModel> SeasonModelList { get; set; }
        public List<ProductModel> freeGiftList { get; set; }
        public List<ProductCat1Model> Cat1List { get; set; }
        public List<ProductCat2Model> Cat2List { get; set; }
        public List<ProductCat3Model> Cat3List { get; set; }
        public List<ProductCat4Model> Cat4List { get; set; }
    }
}
