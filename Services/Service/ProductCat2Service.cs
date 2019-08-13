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
    public class ProductCat2Service:IProductCat2Service
    {
        public List<ProductCat2Model> GetAll()
        {
            return ServerResponse.Invoke<List<ProductCat2Model>>("api/ProductCat2/getDetails", "", "Get");
        }
        public ProductCat2Model GetById(int? id)
        {
            return ServerResponse.Invoke<ProductCat2Model>("api/ProductCat2/getDetail?id=" + id, "", "GET");
        }
        public bool Create(ProductCat2Model model)
        {
            return ServerResponse.Invoke<bool>("api/ProductCat2/create", JsonConvert.SerializeObject(model), "POST");
        }
        public bool Edit(ProductCat2Model model)
        {
            return ServerResponse.Invoke<bool>("api/ProductCat2/edit?id=" + model.Id, JsonConvert.SerializeObject(model), "POST");
        }
        public bool Delete(int? id)
        {
            return ServerResponse.Invoke<bool>("api/ProductCat2/delete?id=" + id, "", "POST");
        }
        public List<ProductCat2Model> GetPaging(int? page, out int TotalCount)
        {
            var body = "";
            int pageNumber = (page ?? 1);
            var result = ServerResponse.Invoke<ServiceResult<List<ProductCat2Model>>>("api/ProductCat2/getCat2Paging?pageNumber=" + page, body, "GET");
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
        public List<ProductCat2Model> GetSearchData(ProductCatSearch areaSearch, int? page, out int TotalCount)
        {
            int pageNumber = (page ?? 1);
            var body = JsonConvert.SerializeObject(areaSearch);
            var result = ServerResponse.Invoke<ServiceResult<List<ProductCat2Model>>>("api/ProductCat2/getSearchData", body, "Post");
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
