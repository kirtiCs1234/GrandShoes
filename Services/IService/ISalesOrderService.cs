using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.IService
{
    public interface ISalesOrderItemService
    {
        List<SalesOrderItemModel> GetWinnerList(WinnerReportModel winner);
        List<SalesOrderItemModel> GetByProduct(int? id);
        List<SalesWeeklyData> GetWeeklySales(int? id);
    }
}
