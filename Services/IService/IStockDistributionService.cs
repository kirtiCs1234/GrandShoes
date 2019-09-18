using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.IService
{
    public interface IStockDistributionService
    {
        List<StockDistributionModel> GetAll();
        StockDistributionModel GetByBranchId(int? BranchId);
        StockDistributionModel GetById(int? Id);
        List<StockDistributionModel> GetValue(Model.SearchData searchdata);
        List<StockDistributionCartonModel> GetProducts(int? StockDistributionSummaryId, int? BranchId);
        List<StockDistributionModel> GetPaging(int? page, out int TotalCount);
        List<StockDistributionModel> GetSearchData(StockDistributionSearch order, int? page, out int TotalCount);
        List<AddItemModel> GetProductForCarton(SearchForCarton model);
        List<StockDistributionModel> GetByProductId(int? id);
        List<StockDistributionModel> GetStockData(int? DistributionSummaryID, string BranchName);
        List<StockDistributionModel> GetByProduct(int? id);
        List<StockDistributionModel> GetLastSummaryData();
        List<StockDistributionModel> GetBySummaryId(int id);
    }

}
