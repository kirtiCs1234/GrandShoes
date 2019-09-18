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
   public class StockDistributionSummaryService: IStockDistributionSummaryService
    {

        public List<StockDistributionSummaryModel> GetAll()
        {
           var StockDistributionSummaryModelList= ServerResponse.Invoke<List<StockDistributionSummaryModel>>("/api/stockDistributionSummaries/getAll", "", "get");
            return StockDistributionSummaryModelList;
        }
        public bool Create(StockDistributionSummaryModel model)
        {
            return ServerResponse.Invoke<bool>("/api/stockDistributionSummaries/create", JsonConvert.SerializeObject(model), "post");

        }
        public List<StockDistributionSummaryModel> GetAllSummary()
        {
            return ServerResponse.Invoke<List<StockDistributionSummaryModel>>("api/stockDistributionSummaries/getDetails", "", "GET");
        }
        public List<StockDistributionSummaryModel> GetAllid()
        {
            var StockDistributionSummaryModelList = ServerResponse.Invoke<List<StockDistributionSummaryModel>>("/api/stockDistributionSummaries/getAll", "", "get");
            return StockDistributionSummaryModelList;
        }
        public bool Edit(StockDistributionSummaryModel model)
        {
            return ServerResponse.Invoke<bool>("/api/stockDistributionSummaries/edit?id="+model.Id, JsonConvert.SerializeObject(model), "post");

        }
    }
}
