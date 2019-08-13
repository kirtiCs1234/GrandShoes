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
   public class ProductSourceService :IProductSourceService
    {
        public List<ProductSourceModel> GetAll()
        {
            var body = "";
            List<ProductSourceModel> ProductSourceModelList = ServerResponse.Invoke<List<ProductSourceModel>>("api/productSource/getDetails", body, "get");
            return ProductSourceModelList;
        }
        public List<ProductSourceModel> GetPaging(int? page, out int TotalCount)
        {
            var body = "";
           // int pageSize = 10;
            int pageNumber = (page ?? 1);
            var result = ServerResponse.Invoke<ServiceResult<List<ProductSourceModel>>>("api/productSource/getProductSourcePaging?pageNumber=" + page, body, "GET");
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
        public List<ProductSourceModel> GetSearchData(ProductSourceSearch productSourceSearch, int? page, out int TotalCount)
        {

            //int pageSize = 10;
            int pageNumber = (page ?? 1);
            var body = JsonConvert.SerializeObject(productSourceSearch);
            var result = ServerResponse.Invoke<ServiceResult<List<ProductSourceModel>>>("api/productSource/getSearchData", body, "Post");
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

        public ProductSourceModel GetById(int? id)
        {
            var body = "";
            ProductSourceModel ProductSourceById = ServerResponse.Invoke<ProductSourceModel>("api/productSource/getDetail?id=" + id, body, "get");
            return ProductSourceById;
        }
        public bool Create(ProductSourceModel productSourceModel)
        {
            var body = JsonConvert.SerializeObject(productSourceModel);
			bool ProductSourceCreate = ServerResponse.Invoke<bool>("api/productSource/create", body, "Post");
            return ProductSourceCreate;
        }
        public bool Edit(ProductSourceModel productSourceModel)
        {
            var body = JsonConvert.SerializeObject(productSourceModel);
            bool ProductSourceEdit = ServerResponse.Invoke<bool>("api/productSource/edit?id=" + productSourceModel.Id, body, "POST");
            return ProductSourceEdit;
        }
        public bool Delete(ProductSourceModel productSourceModel)
        {
            var body = JsonConvert.SerializeObject(productSourceModel);
            bool ProductSourceDelete = ServerResponse.Invoke<bool>("api/productSource/Delete?id=" + productSourceModel.Id, body, "POST");
            return ProductSourceDelete;
        }
        public bool CheckSource(ProductSourceModel model)
        {
            var body = JsonConvert.SerializeObject(model);
            var CheckSource = ServerResponse.Invoke<bool>("api/productSource/checkSource", body, "POST");
            return CheckSource;
        }
        
        public ProductSourceModel GetProductSourceId(string sku)
         {
            var getStyleSkuId = ServerResponse.Invoke<ProductSourceModel>("api/productSource/getProductSourceId?sku=" + sku, "", "POST");
            return getStyleSkuId;
        }

    }
}
