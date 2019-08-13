using Helper;
using Model;
using Newtonsoft.Json;
using Services.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Service
{
   public class ProductCategoryService : IProductCategoryService
    {
        public List<ProductCategoryModel> GetAll()
        {
            var body = "";
            List<ProductCategoryModel> ProductCategoryModelList = ServerResponse.Invoke<List<ProductCategoryModel>>("api/productCategory/getDetails", body, "get");
            return ProductCategoryModelList;
        }
        public List<ProductCategoryModel> GetPaging(int? page, out int TotalCount)
        {
            var body = "";

            int pageNumber = (page ?? 1);
            var result = ServerResponse.Invoke<ServiceResult<List<ProductCategoryModel>>>("api/productCategory/getProductCategoryPaging?pageNumber=" + page, body, "GET");
            TotalCount = result.TotalCount;
            if (result.data != null)
            {
                var model = result.data.ToList();
                return model;
            }
            else
            {

            }
            return result.data.ToList();
        }
        public List<ProductCategoryModel> GetSearchData(ProductCategorySearch areaSearch, int? page, out int TotalCount)
        {
            int pageNumber = (page ?? 1);
            var body = JsonConvert.SerializeObject(areaSearch);
            var result = ServerResponse.Invoke<ServiceResult<List<ProductCategoryModel>>>("api/productCategory/getSearchData", body, "Post");
            TotalCount = result.TotalCount;

            if (result.data != null)
            {
                var model = result.data.ToList();
                return model;
            }
            else
            {

            }
            return result.data.ToList();
        }
        public ProductCategoryModel GetCatogoryCodeId(string sku)
        {
            var getStyleSkuId = ServerResponse.Invoke<ProductCategoryModel>("api/productCategory/getCatagoryCodeId?sku=" + sku, "", "POST");
            return getStyleSkuId;
        }
        public ProductCategoryModel GetById(int? id)
        {
            var body = "";
            ProductCategoryModel ProductCategoryById = ServerResponse.Invoke<ProductCategoryModel>("api/productCategory/getDetail?id=" + id, body, "get");
            return ProductCategoryById;
        }
		
        public ProductCategoryModel Create(ProductCategoryModel productCategoryModel)
        {
            var body = JsonConvert.SerializeObject(productCategoryModel);
            ProductCategoryModel ProductCategoryCreate = ServerResponse.Invoke<ProductCategoryModel>("api/productCategory/create", body, "Post");
            return ProductCategoryCreate;
        }
        public ProductCategoryModel Edit(ProductCategoryModel productCategoryModel)
        {
            var body = JsonConvert.SerializeObject(productCategoryModel);
            ProductCategoryModel ProductCategoryEdit = ServerResponse.Invoke<ProductCategoryModel>("api/productCategory/edit?id=" + productCategoryModel.Id, body, "POST");
            return ProductCategoryEdit;
        }
        public ProductCategoryModel Delete(ProductCategoryModel productCategoryModel)
        {
            var body = JsonConvert.SerializeObject(productCategoryModel);
            ProductCategoryModel ProductCategoryDelete = ServerResponse.Invoke<ProductCategoryModel>("api/productCategory/Delete?id=" + productCategoryModel.Id, body, "POST");
            return ProductCategoryDelete;
        }
        public bool CheckProductCategoryCode(ProductCategoryModel model)
        {
            var body = JsonConvert.SerializeObject(model);
            var CheckCode = ServerResponse.Invoke<bool>("api/productCategory/checkCode", body, "POST");
            return CheckCode;
        }
		public List<ProductCat1Model> GetCat1List()
        {
            return ServerResponse.Invoke<List<ProductCat1Model>>("api/productCategory/getProdCat1", "", "GET");
        }
        public List<ProductCat2Model> GetCat2List()
        {
            return ServerResponse.Invoke<List<ProductCat2Model>>("api/productCategory/getProdCat2", "", "GET");
        }
        public List<ProductCat3Model> GetCat3List()
        {
            return ServerResponse.Invoke<List<ProductCat3Model>>("api/productCategory/getProdCat3", "", "GET");
        }
        public List<ProductCat4Model> GetCat4List()
        {
            return ServerResponse.Invoke<List<ProductCat4Model>>("api/productCategory/getProdCat4", "", "GET");
        }
        public ProductCat1Model GetIdCat1(string prodCat1)
        {
            return ServerResponse.Invoke<ProductCat1Model>("api/productCategory/getIdCate1?prodCat1=" + prodCat1, "", "GET");
        }
        public ProductCat2Model GetIdCat2(string prodCat2)
        {
            return ServerResponse.Invoke<ProductCat2Model>("api/productCategory/getIdCate2?prodCat2=" + prodCat2, "", "GET");
        }
        public ProductCat3Model GetIdCat3(string prodCat3)
        {
            return ServerResponse.Invoke<ProductCat3Model>("api/productCategory/getIdCate3?prodCat3=" + prodCat3, "", "GET");
        }
        public ProductCat4Model GetIdCat4(string prodCat4)
        {
            return ServerResponse.Invoke<ProductCat4Model>("api/productCategory/getIdCate4?prodCat4=" + prodCat4, "", "GET");
        }

    }
}
