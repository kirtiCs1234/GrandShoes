using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.IService
{
    public interface IDiscountService
    {
        DiscountSummaryModel Create(DiscountSummaryModel discountAdd);
        List<DiscountModel> GetAll();
        List<DiscountSummaryModel> GetDataSummary();
        List<PromotionalDiscountModel> GetProtionalDiscount(int? id);
        List<PromotionalDiscountModel> GetProductList(int? id);
        bool AllowDiscount(PromotionalDiscountModel model);
        DiscountSummaryModel GetByIdSummary(int? id);
        bool Delete(DiscountSummaryModel model);
    }
}
