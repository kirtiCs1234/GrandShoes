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
    public class ProductCat4Service:IProductCat4Service
    {
        public List<ProductCat4Model> GetAll()
        {
            return ServerResponse.Invoke<List<ProductCat4Model>>("api/ProductCat4/getDetails", "", "Get");
        }
        public ProductCat4Model GetById(int? id)
        {
            return ServerResponse.Invoke<ProductCat4Model>("api/ProductCat4/getDetail?id=" + id, "", "GET");
        }
        public bool Create(ProductCat4Model model)
        {
            return ServerResponse.Invoke<bool>("api/ProductCat4/create", JsonConvert.SerializeObject(model), "POST");
        }
        public bool Edit(ProductCat4Model model)
        {
            return ServerResponse.Invoke<bool>("api/ProductCat4/edit?id=" + model.Id, JsonConvert.SerializeObject(model), "POST");
        }
        public bool Delete(int? id)
        {
            return ServerResponse.Invoke<bool>("api/ProductCat4/delete?id=" + id, "", "POST");
        }
        public List<ProductCat4Model> GetPaging(int? page, out int TotalCount)
        {
            var body = "";
            int pageNumber = (page ?? 1);
            var result = ServerResponse.Invoke<ServiceResult<List<ProductCat4Model>>>("api/ProductCat4/getCat4Paging?pageNumber=" + page, body, "GET");
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
        public List<ProductCat4Model> GetSearchData(ProductCatSearch areaSearch, int? page, out int TotalCount)
        {
            int pageNumber = (page ?? 1);
            var body = JsonConvert.SerializeObject(areaSearch);
            var result = ServerResponse.Invoke<ServiceResult<List<ProductCat4Model>>>("api/ProductCat4/getSearchData", body, "Post");
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
