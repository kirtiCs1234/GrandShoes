using Services.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
using Helper;
using Newtonsoft.Json;

namespace Services.Service
{
    public class StockInventoryService : IStockInventoryService
    {
       
        public StockDistributionViewModel GetAll(int? ProductId)
        {
            var body = "";
            var list = ServerResponse.Invoke<StockDistributionViewModel>("api/stockInventory/getDetails?ProductId=" + ProductId, body, "get");
            return list;
        }
        public StockDistributionViewModel GetByProductId(int? ProductId,  int DisributionSummaryId)
        {
            var body = "";
            var StockInventoryModelById = ServerResponse.Invoke<StockDistributionViewModel>("api/stockinventory/getproduct?ProductId="+ProductId+"&DisributionSummaryId=" + DisributionSummaryId+"", body, "GET");
            return StockInventoryModelById;
         }
        
        public List<StockInventoryModel> GetProduct()
        {
            var ProductList = ServerResponse.Invoke<List<StockInventoryModel>>("api/stockInventory/getProducts", "", "GET");
            return ProductList;
        }
        //public StockDistributionViewModel SaveStock(StockDistributionViewModel model)
        //{
        //    var body = JsonConvert.SerializeObject(model);
        //    var StockInventoryModelById = ServerResponse.Invoke<StockDistributionViewModel>("api/stockinventory/SaveStockData", body, "POST");
        //    return StockInventoryModelById;
        //}

        public bool SaveStock(StockDistributionViewModel model)
        {
           return ServerResponse.Invoke<bool>("/api/stockinventory/SaveStockData", JsonConvert.SerializeObject(model), "post");
            
        }
      
        public bool CheckQuantity(StockDistributionModel stock)
        {
            var body = JsonConvert.SerializeObject(stock);
            var check = ServerResponse.Invoke<bool>("api/stockInventory/checkQuantities", body, "post");
            return check;
        }
        public StockInventoryModel GetByProduct(int? id)
        {
            return ServerResponse.Invoke<StockInventoryModel>("api/stockInventory/getByProduct?ProductId=" +id, "", "Get");
        }

    }
}
