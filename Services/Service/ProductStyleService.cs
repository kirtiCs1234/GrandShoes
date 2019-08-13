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
   public class ProductStyleService : IProductStyleService
    {
        public List<ProductStyleModel> GetAll()
        {
            var body = "";
            List<ProductStyleModel> ProductStyleModelList = ServerResponse.Invoke<List<ProductStyleModel>>("api/productStyle/getDetails", body, "get");
            return ProductStyleModelList;
        }
        public List<string> GetCode()
        {
            var body = "";
            List<string> ProductStyleCodeList = ServerResponse.Invoke<List<string>>("api/productStyle/getCode", body, "get");
            return ProductStyleCodeList;
        }
        public List<ProductStyleModel> ProductStyleAutocomplete(string name)
        {
            return ServerResponse.Invoke<List<ProductStyleModel>>("api/productStyle/ProductStyleAutocomplete?name=" + name, "", "get");
        }
        public bool Create(ProductStyleModel model)
        {
            bool status = ServerResponse.Invoke<bool>("api/productStyle/create", JsonConvert.SerializeObject(model), "post");
            return status;
        }
        public bool Edit(ProductStyleModel model)
        {
            bool status = ServerResponse.Invoke<bool>("api/productStyle/edit?id=" + model.Id, JsonConvert.SerializeObject(model), "Post");
            return status;
        }
        public ProductStyleModel Details(int id)
        {
            var ProductStyle = ServerResponse.Invoke<ProductStyleModel>("api/productStyle/getDetail?id=" + id, "", "GET");
            return ProductStyle;
        }
        public ProductStyleModel Delete(ProductStyleModel style)
        {
            ProductStyleModel status = ServerResponse.Invoke<ProductStyleModel>("api/productStyle/delete?id=" + style.Id, "", "GET");
            return status;
        }
        
        public List<ProductStyleModel> GetPaging(int? page, out int TotalCount)
        {
            var body = "";

            int pageNumber = (page ?? 1);
            var result = ServerResponse.Invoke<ServiceResult<List<ProductStyleModel>>>("api/productStyle/getProductStylePaging?pageNumber=" + page, body, "GET");
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
        
        
        public List<ProductStyleModel> GetSearchData(ProductStyleSearch styleSearch, int? page, out int TotalCount)
        {
            int pageNumber = (page ?? 1);
            var body = JsonConvert.SerializeObject(styleSearch);
            var result = ServerResponse.Invoke<ServiceResult<List<ProductStyleModel>>>("api/productStyle/getSearchData", body, "Post");
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

        public ProductStyleModel GetStyleSKUId(string sku)
        {
            var getStyleSkuId = ServerResponse.Invoke<ProductStyleModel>("api/productStyle/getStyleSKUId?sku=" + sku, "", "POST");
            return getStyleSkuId;
        }
    }
}
