using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.IService
{
   public interface IBuyerService
    {
        List<BuyerModel> GetAll();
        List<BuyerModel> GetPaging(int? page, out int TotalCount);
        List<BuyerModel> GetSearchData(BuyerSearch buyerSearch, int? page, out int TotalCount);
        BuyerModel GetById(int? id);
        BuyerModel Create(BuyerModel buyerModel);
        BuyerModel Edit(BuyerModel buyerModel);
        BuyerModel Delete(BuyerModel buyerModel);
        BuyerModel GetBuyerId(string sku);
    }
}
