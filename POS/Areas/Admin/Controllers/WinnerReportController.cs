using Helper;
using Model;
using POS.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace POS.Areas.Admin.Controllers
{
    [CustomAuth(PageSession.WinnerReport)]
    public class WinnerReportController : BaseController
    {
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult GetWinnerList(WinnerReportModel winner)
        {
            var list = Services.SalesOrderItemService.GetWinnerList(winner);
            var markDown = Services.MarkDownService.GetAll();
            var IBTBranch = Services.IBTService.GetIBTDetails(winner);
            var list1 = list.GroupBy(x => x.ProductId).Select(x => new SalesOrderItemModel
            {
                ProductId = x.First().ProductId,
                Product = Services.ProductService.GetAll().Where(m => m.Id == x.First().ProductId).FirstOrDefault(),
                Quantity = x.Sum(m => m.Quantity),
                CashSaleCount=x.Where(q=>q.SalesOrder.SaleType.Contains("Cash")).Sum(l=>l.Quantity),
                LayBuySaleCount=x.Where(y=>y.SalesOrder.SaleType.Contains("LayBaySale")).Sum(j=>j.Quantity),
                RemainingQuantity = x.Last().RemainingQuantity,
                PricePerUnit = x.First().PricePerUnit,
                TotalPriceAll = x.Sum(m => m.TotalPrice),
                IBTStock = IBTBranch.Where(u => u.ProductId == x.First().ProductId).Sum(p => p.ItemCount),
                Sold = (x.Sum(m => m.Quantity) * 100) /(IBTBranch.Where(u => u.ProductId == x.First().ProductId).Sum(p => p.ItemCount)),
                MarkDownCount = markDown.Where(k => k.ProductSKU ==x.First().Product.ProductSKU && k.StyleSKU == x.First().Product.StyleSKU).Count(),
        }).ToList();
            return View(list1);
        }
    }
}