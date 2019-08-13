using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.IService
{
   public interface IStockBranchInventoryService
    {
        List<StockBranchInventoryModel> GetAll();
        List<StockBranchInventoryModel> GetByBranch(int? BranchId);
        bool Update(int? BranchId, List<StockBranchInventoryModel> stockList);
        List<StockBranchInventoryModel> GetByProduct(int? id);
    }
}
