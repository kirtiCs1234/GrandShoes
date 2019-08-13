using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.IService
{
  public interface IStockInventoryService
    {
        StockDistributionViewModel GetAll(int? ProductId);
        StockDistributionViewModel GetByProductId(int? id, int DisributionSummaryId);
        bool SaveStock(StockDistributionViewModel model);
        List<StockInventoryModel> GetProduct();
        StockInventoryModel GetByProduct(int? id);
        bool CheckQuantity(StockDistributionModel quantity);
    }
}
