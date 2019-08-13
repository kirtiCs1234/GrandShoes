using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.IService
{
   public interface IProductCategoryService
    {
        List<ProductCategoryModel> GetAll();
        List<ProductCategoryModel> GetPaging(int? page, out int TotalCount);
        List<ProductCategoryModel> GetSearchData(ProductCategorySearch productCategorySearch, int? page, out int TotalCount);
        ProductCategoryModel GetById(int? id);
        ProductCategoryModel Create(ProductCategoryModel productCategoryModel);
        ProductCategoryModel Edit(ProductCategoryModel productCategoryModel);
        ProductCategoryModel Delete(ProductCategoryModel productCategoryModel);
        bool CheckProductCategoryCode(ProductCategoryModel model);
        ProductCategoryModel GetCatogoryCodeId(string sku);
        List<ProductCat1Model> GetCat1List();
        List<ProductCat2Model> GetCat2List();
        List<ProductCat3Model> GetCat3List();
        List<ProductCat4Model> GetCat4List();
        ProductCat1Model GetIdCat1(string prodCat1);
        ProductCat2Model GetIdCat2(string prodCat2);
        ProductCat3Model GetIdCat3(string prodCat3);
        ProductCat4Model GetIdCat4(string prodCat4);
    }
}
