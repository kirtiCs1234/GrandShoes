using Helper;
using Model;
using Newtonsoft.Json;
using POS.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Mvc;

namespace POS.Areas.Admin.Controllers
{
    [CustomAuth(PageSession.BranchStockReport)]
    public class BranchStockReportController : BaseController
    {
        public ActionResult Index(int? page)
        {
            int TotalCount = 0;
            var pageSize = 10;
            var pageNumber = page ?? 1;
            int CurrentPage = pageNumber;
            var endPage = CurrentPage + 4;
            int PagesToShow = 10;
            var BranchModelList = Services.BranchService.GetPaging(page, out TotalCount);
            foreach (var date1 in BranchModelList)
            {
                if (date1.DateOpen != null && date1.DateClosed != null)
                {
                    var DateOpen = date1.DateOpen.Substring(0, date1.DateOpen.Length - 9);
                    date1.DateOpen = DateOpen;
                    var DateClosed = date1.DateClosed.Substring(0, date1.DateClosed.Length - 9);
                    date1.DateClosed = DateClosed;
                }
            }
            ViewBag.TotalCount = TotalCount;
            ViewBag.PageSize = pageSize;
            var result = Helper.CommonFunction.GetPages(TotalCount, pageSize, CurrentPage, PagesToShow);
            int totalPages = (TotalCount / pageSize) + (TotalCount % pageSize == 0 ? 0 : 1);
            ViewBag.result = result;
            ViewBag.totalPages = totalPages;
            ViewBag.CurrentPage = CurrentPage;
            var pageCount = result.Count();
            ViewBag.pageCount = pageCount;
            ViewBag.endPage = endPage;
            return View(BranchModelList);
        }
        public ActionResult _Index1(BranchSearch branchSearch, int? page)
        {
            int TotalCount = 0;
            var pageSize = 10;
            var pageNumber = page ?? 1;
            int CurrentPage = pageNumber;
            var endPage = CurrentPage + 4;
            int PagesToShow = 10;
            var BranchModelList = Services.BranchService.GetSearchData(branchSearch, page, out TotalCount);
            foreach (var date1 in BranchModelList)
            {
                if (date1.DateOpen != null && date1.DateClosed != null)
                {
                    var DateOpen = date1.DateOpen.Substring(0, date1.DateOpen.Length - 9);
                    date1.DateOpen = DateOpen;
                    var DateClosed = date1.DateClosed.Substring(0, date1.DateClosed.Length - 9);
                    date1.DateClosed = DateClosed;
                }
            }
            ViewBag.TotalCount = TotalCount;
            ViewBag.PageSize = pageSize;
            var result = Helper.CommonFunction.GetPages(TotalCount, pageSize, CurrentPage, PagesToShow);
            int totalPages = (TotalCount / pageSize) + (TotalCount % pageSize == 0 ? 0 : 1);
            ViewBag.result = result;
            ViewBag.totalPages = totalPages;
            ViewBag.CurrentPage = CurrentPage;
            var pageCount = result.Count();
            ViewBag.pageCount = pageCount;
            ViewBag.endPage = endPage;
            return View(BranchModelList);
        }
    }
}
