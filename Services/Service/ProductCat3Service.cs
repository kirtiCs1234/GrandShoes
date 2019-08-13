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
    public class ProductCat3Service:IProductCat3Service
    {
        public List<ProductCat3Model> GetAll()
        {
            return ServerResponse.Invoke<List<ProductCat3Model>>("api/ProductCat3/getDetails", "", "Get");
        }
        public ProductCat3Model GetById(int? id)
        {
            return ServerResponse.Invoke<ProductCat3Model>("api/ProductCat3/getDetail?id=" + id, "", "GET");
        }
        public bool Create(ProductCat3Model model)
        {
            return ServerResponse.Invoke<bool>("api/ProductCat3/create", JsonConvert.SerializeObject(model), "POST");
        }
        public bool Edit(ProductCat3Model model)
        {
            return ServerResponse.Invoke<bool>("api/ProductCat3/edit?id=" + model.Id, JsonConvert.SerializeObject(model), "POST");
        }
        public bool Delete(int? id)
        {
            return ServerResponse.Invoke<bool>("api/ProductCat3/delete?id=" + id, "", "POST");
        }
        public List<ProductCat3Model> GetPaging(int? page, out int TotalCount)
        {
            var body = "";
            int pageNumber = (page ?? 1);
            var result = ServerResponse.Invoke<ServiceResult<List<ProductCat3Model>>>("api/ProductCat3/getCat3Paging?pageNumber=" + page, body, "GET");
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
        public List<ProductCat3Model> GetSearchData(ProductCatSearch areaSearch, int? page, out int TotalCount)
        {
            int pageNumber = (page ?? 1);
            var body = JsonConvert.SerializeObject(areaSearch);
            var result = ServerResponse.Invoke<ServiceResult<List<ProductCat3Model>>>("api/ProductCat3/getSearchData", body, "Post");
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
