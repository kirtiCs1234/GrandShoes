using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Mvc;
using Model;
using POS.Controllers;
using Helper;

namespace POS.Areas.Admin.Controllers
{
    [CustomAuth(PageSession.PurchaseOrderReport)]
    public class PurchaseOrderReportController : BaseController
    {
        public ActionResult Index(int? page)
        
{
            var OrderStatus = Services.PurchaseOrderService.GetPurchaseOrderStatus();
            ViewBag.PurchaseOrderStatusId = new SelectList(OrderStatus, "Id", "OrderStatus");           
            int TotalCount = 0;
            var pageSize = 10;
            var pageNumber = page ?? 1;
            int CurrentPage = pageNumber;
            var endPage = CurrentPage + 4;
            int PagesToShow = 10;
            ViewBag.PageSize = pageSize;
            var AreaModelList = Services.PurchaseOrderService.GetPaging1(page, out TotalCount);
            foreach (var date in AreaModelList)
            {
                    var str = date.OrderDate;
                if (!string.IsNullOrEmpty(str)) { 
                    str = str.Substring(0, str.Length - 9);
                    date.OrderDate = str;
                }
            }
            ViewBag.TotalCount = TotalCount;
            var result = Helper.CommonFunction.GetPages(TotalCount, pageSize, CurrentPage, PagesToShow);
            int totalPages = (TotalCount / pageSize) + (TotalCount % pageSize == 0 ? 0 : 1);
            ViewBag.result = result;
            ViewBag.totalPages = totalPages;
            ViewBag.CurrentPage = CurrentPage;
            var pageCount = result.Count();
            ViewBag.pageCount = pageCount;
            ViewBag.endPage = endPage;
            return View(AreaModelList);
        }

        public ActionResult _Index1(PurchaseOrderSearchModel search ,int? page)
        {

            int TotalCount = 0;
            var pageSize = 10;
            var pageNumber = page ?? 1;
            int CurrentPage = pageNumber;
            var endPage = CurrentPage + 4;
            int PagesToShow = 10;
            ViewBag.PageSize = pageSize;
            var AreaModelList = Services.PurchaseOrderService.GetPurchaseOrderSearchData1(search, page, out TotalCount);
            foreach (var date in AreaModelList)
            {
                var str = date.OrderDate;
                if (!string.IsNullOrEmpty(str))
                {
                    str = str.Substring(0, str.Length - 9);
                    date.OrderDate = str;
                }
            }
            ViewBag.TotalCount = TotalCount;
            var result = Helper.CommonFunction.GetPages(TotalCount, pageSize, CurrentPage, PagesToShow);
            int totalPages = (TotalCount / pageSize) + (TotalCount % pageSize == 0 ? 0 : 1);
            ViewBag.result = result;
            ViewBag.totalPages = totalPages;
            ViewBag.CurrentPage = CurrentPage;
            var pageCount = result.Count();
            ViewBag.pageCount = pageCount;
            ViewBag.endPage = endPage;
            return View(AreaModelList);
        }  
    }
}
