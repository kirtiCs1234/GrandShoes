using DAL;
using Helper.ExtensionMethod;
using Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace POSApi.Controllers.Admin
{
    [RoutePrefix("api/salesOrderItems")]
    public class SalesOrderItemsController : ApiController
    {
        GrandShoesEntities db = new GrandShoesEntities();
        public SalesOrderItemsController()
        {
            db.Configuration.ProxyCreationEnabled = false;
            db.Configuration.LazyLoadingEnabled = false;
        }
        [HttpPost]
        [Route("getWinnerList")]
        public IHttpActionResult GetWinnerList(WinnerReportModel winner)
        {
            var list = db.SalesOrderItems.Where(m => m.IsActive == true).Include(m => m.SalesOrder).Include(m => m.Product);
            if (!string.IsNullOrEmpty(winner.BranchCode))
            {
                list = list.Where(x => x.SalesOrder.Branch.BranchCode.Contains(winner.BranchCode));
            }
            if (winner.IsMarkDown == true)
            {
                list = list.Where(x => x.Product.IsMarkDown == true);
            }
            if (winner.FromReportDate != null && winner.ToReportDate != null)
            {
                var FromDate = Convert.ToDateTime(winner.FromReportDate).Date;
                var ToDate = Convert.ToDateTime(winner.ToReportDate).AddDays(1).Date;
                list = list.Where(k => k.SalesOrder.TransactionDate >= FromDate && k.SalesOrder.TransactionDate < ToDate);
            }
            if(winner.FromDistributionDate!=null && winner.ToDistributionDate != null)
            {
                var FromDate = Convert.ToDateTime(winner.FromDistributionDate).Date;
                var ToDate = Convert.ToDateTime(winner.ToDistributionDate).AddDays(1).Date;
                list = list.Where(k => k.SalesOrder.TransactionDate >= FromDate && k.SalesOrder.TransactionDate < ToDate);
            }
            return Ok(list.ToList().RemoveReferences());
        }
        [Route("getByProduct")]
        public IHttpActionResult getByProduct(int id)
        {
            var data = db.SalesOrderItems.Where(x => x.IsActive == true && x.ProductId == id).Include(x=>x.SalesOrder).Include(x=>x.SalesOrder.Branch).ToList();
            return Ok(data);
        }
        [Route("getWeeklyReportByProduct")]
        public IHttpActionResult getWeeklyReportByProduct(int id)
        {
            List<SalesWeeklyData> list = new List<SalesWeeklyData>();
            var salesItem = db.SalesOrderItems.Where(x => x.IsActive == true && x.ProductId == id).Include(x=>x.SalesOrder).ToList();
            var branchInventory = db.StockBranchInventories.Where(x => x.IsActive == true && x.ProductId == id).ToList();
            if (salesItem.Count > 0)
            {
                DateTime LastDate = salesItem.LastOrDefault().SalesOrder.TransactionDate ?? DateTime.Now;
                var nextSeven = LastDate.AddDays(7);
                foreach (var element in salesItem)
                {
                    var branchInventoryList = new List<StockBranchInventory>();
                    var sevenDaysData = new List<SalesOrderItem>();
                    if (list.Count > 0)
                    {
                        sevenDaysData = salesItem.Where(x => x.SalesOrder.TransactionDate > Convert.ToDateTime(list.LastOrDefault().WeekDate) && x.SalesOrder.TransactionDate <= Convert.ToDateTime(list.LastOrDefault().WeekDate).AddDays(7)).ToList();
                        branchInventoryList = branchInventory.Where(x => x.UpdateTime > Convert.ToDateTime(list.LastOrDefault().WeekDate) && x.UpdateTime <= Convert.ToDateTime(list.LastOrDefault().WeekDate).AddDays(7)).ToList();
                    }
                    else
                    {
                        sevenDaysData = salesItem.Where(x => x.SalesOrder.TransactionDate >= LastDate && x.SalesOrder.TransactionDate <= nextSeven).ToList();
                        branchInventoryList = branchInventory.Where(x => x.UpdateTime >= LastDate && x.UpdateTime <= nextSeven).ToList();
                    }
                    if (sevenDaysData.Count > 0 && branchInventoryList.Count > 0)
                    {
                        int? Quantity = 0;
                        int? Qunatity1 = 0;
                        SalesWeeklyData model = new SalesWeeklyData();
                        foreach (var item in sevenDaysData)
                        {
                            Quantity += item.Quantity;
                        }
                        foreach (var item2 in branchInventoryList)
                        {
                            Qunatity1 += (item2.Quantity01 + item2.Quantity02 + item2.Quantity03 + item2.Quantity04 + item2.Quantity05 + item2.Quantity06 +
                                item2.Quantity07 + item2.Quantity08 + item2.Quantity09 + item2.Quantity10 + item2.Quantity11 + item2.Quantity12 + item2.Quantity13 +
                                item2.Quantity14 + item2.Quantity15 + item2.Quantity16 + item2.Quantity17 + item2.Quantity18 + item2.Quantity19 +
                                item2.Quantity20 + item2.Quantity21 + item2.Quantity22 + item2.Quantity23 + item2.Quantity24 + item2.Quantity25 + item2.Quantity26 + item2.Quantity27 + item2.Quantity28 + item2.Quantity29 + item2.Quantity30);
                        }
                        int? count = 0;
                        model.TotalData = Qunatity1;
                        model.SoldData = Quantity;
                        model.percent = (model.SoldData * 100) / model.TotalData;
                        count += model.SoldData;
                        model.Totalsold = count;
                        int? TCount = 0;
                        TCount += model.TotalData;
                        model.TotalPercent = (model.Totalsold * 100) / TCount;
                        model.WeekDate = nextSeven.ToString().Substring(0, 10);
                        list.Add(model);
                    }
                }
            }
            return Ok(list);
        }
    }
}
