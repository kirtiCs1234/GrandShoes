using Helper;
using Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Service
{
    public class ProductCat1Service:IProductCat1Service
    {
        public List<ProductCat1Model> GetAll()
        {
            return ServerResponse.Invoke<List<ProductCat1Model>>("api/ProductCat1/getDetails", "", "Get");
        }
        public ProductCat1Model GetById(int? id)
        {
            return ServerResponse.Invoke<ProductCat1Model>("api/ProductCat1/getDetail?id=" + id, "", "GET");
        }
        public bool Create(ProductCat1Model model)
        {
            return ServerResponse.Invoke<bool>("api/productCat1/create", JsonConvert.SerializeObject(model), "POST");
        }
        public bool Edit(ProductCat1Model model)
        {
            return ServerResponse.Invoke<bool>("api/ProductCat1/edit?id=" + model.Id, JsonConvert.SerializeObject(model), "POST");
        }
        public bool Delete(int? id)
        {
            return ServerResponse.Invoke<bool>("api/ProductCat1/delete?id=" + id, "", "POST");
        }
        public List<ProductCat1Model> GetPaging(int? page, out int TotalCount)
        {
            var body = "";
            int pageNumber = (page ?? 1);
            var result = ServerResponse.Invoke<ServiceResult<List<ProductCat1Model>>>("api/ProductCat1/getCat1Paging?pageNumber=" + page, body, "GET");
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
        public List<ProductCat1Model> GetSearchData(ProductCatSearch areaSearch, int? page, out int TotalCount)
        {
            int pageNumber = (page ?? 1);
            var body = JsonConvert.SerializeObject(areaSearch);
            var result = ServerResponse.Invoke<ServiceResult<List<ProductCat1Model>>>("api/ProductCat1/getSearchData", body, "Post");
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
    }
}
