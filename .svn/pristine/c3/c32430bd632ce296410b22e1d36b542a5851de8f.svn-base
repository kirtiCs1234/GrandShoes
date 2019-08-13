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
    public class BuyerService :IBuyerService
    {
        public List<BuyerModel> GetAll()

        {
            var body = "";
            List<BuyerModel> BuyerModelList = ServerResponse.Invoke<List<BuyerModel>>("api/buyer/getDetails", body, "get");
            return BuyerModelList;
        }
        public List<BuyerModel> GetPaging(int? page, out int TotalCount)
        {
            var body = "";
          
            int pageNumber = (page ?? 1);
            var result = ServerResponse.Invoke<ServiceResult<List<BuyerModel>>>("api/buyer/getBuyerPaging?pageNumber=" + page, body, "GET");
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
        public List<BuyerModel> GetSearchData(BuyerSearch buyerSearch, int? page, out int TotalCount)
        {
           var pageNumber = (page ?? 1);
            var body = JsonConvert.SerializeObject(buyerSearch);
            var result = ServerResponse.Invoke<ServiceResult<List<BuyerModel>>>("api/buyer/getSearchData", body, "Post");
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

        public BuyerModel GetById(int? id)
        {
            var body = "";
            BuyerModel BuyerModelById = ServerResponse.Invoke<BuyerModel>("api/buyer/getDetail?id=" + id, body, "GET");
            return BuyerModelById;
        }

        public BuyerModel GetBuyerId(string sku)
        {
            var GetBuyerId = ServerResponse.Invoke<BuyerModel>("api/buyer/getBuyerId?sku=" + sku, "", "POST");
            return GetBuyerId;
        }
        public BuyerModel Create(BuyerModel buyerModel)
        {
            var body = JsonConvert.SerializeObject(buyerModel);
            BuyerModel BuyerCreate = ServerResponse.Invoke<BuyerModel>("api/buyer/create", body, "Post");
            return BuyerCreate;
        }
        public BuyerModel Edit(BuyerModel buyerModel)
        {
            var body = JsonConvert.SerializeObject(buyerModel);
            BuyerModel BuyerEdit = ServerResponse.Invoke<BuyerModel>("api/buyer/edit?id=" + buyerModel.Id, body, "POST");
            return BuyerEdit;
        }
        public BuyerModel Delete(BuyerModel buyerModel)
        {
            var body = JsonConvert.SerializeObject(buyerModel);
            BuyerModel BuyerDelete = ServerResponse.Invoke<BuyerModel>("api/buyer/Delete?id=" + buyerModel.Id, body, "POST");
            return BuyerDelete;
        }
     
    }
}
