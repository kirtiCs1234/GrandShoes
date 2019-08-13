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
   public class SalesOrderItemService: ISalesOrderItemService
    {
        public List<SalesOrderItemModel> GetWinnerList(WinnerReportModel winner)
        {
            var list4= new List<SalesOrderItemModel>();
            var list2= new List<SalesOrderItemModel>();
            var list = ServerResponse.Invoke<List<SalesOrderItemModel>>("api/salesOrderItems/getWinnerList", JsonConvert.SerializeObject(winner), "POST");
            if (winner.FromProductSKU != null && winner.ToProductSKU != null)
            {
                var x = Convert.ToInt32(winner.FromProductSKU);
                var y = Convert.ToInt32(winner.ToProductSKU);
                for (var i = x; i <= y; i++)
                {
                    var list1 = new List<SalesOrderItemModel>();
                    var str = "";
                    if (i < 10)
                    {
                        str = "00" + i;

                    }
                    else if (i < 100)
                    {
                        str = "0" + i;
                    }
                    else
                    {
                        str = "" + i;
                    }
                  list1 = list.Where(k => k.Product.ProductSKU.Contains(str)).ToList();
                    list2.AddRange(list1);
                }
            }
            if (winner.FromStyleSKU != null && winner.ToStyleSKU != null)
            {
                var style1 = Convert.ToInt32(winner.FromStyleSKU);
                var style2 = Convert.ToInt32(winner.ToStyleSKU);
                for (int j = style1; j <= style2; j++)
                {
                    var list3 = new List<SalesOrderItemModel>();
                    var str1 = "";
                    if (j < 10)
                    {
                        str1 = "000" + j;
                    }
                    else if (j < 100)
                    {
                        str1 = "00" + j;
                    }
                    else if (j < 1000)
                    {
                        str1 = "0" + j;
                    }
                    else
                    {
                        str1 = "" + j;
                    }
                    list3= list2.Where(m => m.Product.StyleSKU.Contains(str1)).ToList();
                    list4.AddRange(list3);
                }
            }
            return list2;
        }
    }
}
