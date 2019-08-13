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
   public class StockEnquiryService: IStockEnquiryService
    {
      public List<StockDistributionModel> GetSelectedData(Model.StockEnquiryModel searchData)
        {
            var body = JsonConvert.SerializeObject(searchData);
            var searchDataList = ServerResponse.Invoke<List<StockDistributionModel>>("api/stockDistributions/getSelectedData", body, "POST");
            return searchDataList;
        }
        public List<StockInventoryModel> GetAll(Model.StockEnquiryModel searchData)
        {
            var body = JsonConvert.SerializeObject(searchData);
            var Searchlist = ServerResponse.Invoke<List<StockInventoryModel>>("api/stockInventory/getDetailList", body, "POST");
            return Searchlist;
        }
    }
}
