using Helper;
using Model;
using POS.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Mvc;

namespace POS.Areas.Admin.Controllers
{
    [CustomAuth(PageSession.StoreDelieveryReport)]
    public class StoreDeliveryReportController : BaseController
    {
        public  ActionResult Index(int? page)
        {
            StoreDeliveryReportModel SDR = new StoreDeliveryReportModel();
            var BranchList = Services.BranchService.GetAll();
            ViewBag.BranchId = new SelectList(BranchList, "Id", "Name");
            //var allDeliverList = Services.StockDistributionService.GetAll();          
            int TotalCount = 0;
            var pageSize = 10;
            var pageNumber = page ?? 1;
            int CurrentPage = pageNumber;
            var endPage = CurrentPage + 4;
            int PagesToShow = 10;
            ViewBag.PageSize = pageSize;
            var AreaModelList = Services.StockDistributionService.GetPaging(page, out TotalCount);
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
       
            public ActionResult _Index1(StockDistributionSearch StoreModel, int? page)
            {
            int TotalCount = 0;
            var pageSize = 10;
            var pageNumber = page ?? 1;
            int CurrentPage = pageNumber;
            var endPage = CurrentPage + 4;
            int PagesToShow = 10;
            ViewBag.PageSize = pageSize;
            var AreaModelList = Services.StockDistributionService.GetSearchData(StoreModel, page, out TotalCount);
            ViewBag.TotalCount = TotalCount;
            var result = CommonFunction.GetPages(TotalCount, pageSize, CurrentPage, PagesToShow);
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
