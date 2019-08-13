﻿using Helper;
using Model;
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
    [CustomAuth(PageSession.CartonManagementReport)]
    public class CartonManagementReportController : BaseController
    {
        
        public ActionResult Index(int? page)
        {
            CartonManagementReportModel POR = new CartonManagementReportModel();
            var StockDistributionSummaryId = Services.StockDistributionSummaryService.GetAll();
            ViewBag.StockDistributionSummaryId = new SelectList(StockDistributionSummaryId, "Id", "Id");
            var BranchModelList = Services.BranchService.GetAll();
            ViewBag.BranchId = new SelectList(BranchModelList, "Id", "Name");
            int TotalCount = 0;
            var pageSize = 10;
            var pageNumber = page ?? 1;
            int CurrentPage = pageNumber;
            var endPage = CurrentPage + 4;
            int PagesToShow = 10;
            ViewBag.PageSize = pageSize;
            var CartonList = Services.CartonManagementService.GetPaging(page, out TotalCount);
            POR.CartonManagement = CartonList;
            //  var AreaModelList = Services.AreaService.GetSearchData(areaSearch, page, out TotalCount);
            ViewBag.TotalCount = TotalCount;

            var result = Helper.CommonFunction.GetPages(TotalCount, pageSize, CurrentPage, PagesToShow);
            int totalPages = (TotalCount / pageSize) + (TotalCount % pageSize == 0 ? 0 : 1);
            ViewBag.result = result;
            ViewBag.totalPages = totalPages;
            ViewBag.CurrentPage = CurrentPage;
            var pageCount = result.Count();
            ViewBag.pageCount = pageCount;

            ViewBag.endPage = endPage;
            return View(POR);
        }
        public ActionResult _Index1(CartonManagementModel CartonManagement, int? page)
        {
            CartonManagementReportModel POR = new CartonManagementReportModel();
            int TotalCount = 0;
            var pageSize = 10;
            var pageNumber = page ?? 1;
            int CurrentPage = pageNumber;
            var endPage = CurrentPage + 4;
            int PagesToShow = 10;
            ViewBag.PageSize = pageSize;
            var CartonList = Services.CartonManagementService.GetSearchData(CartonManagement, page, out TotalCount);
            POR.CartonManagement = CartonList;
            ViewBag.TotalCount = TotalCount;

            var result = Helper.CommonFunction.GetPages(TotalCount, pageSize, CurrentPage, PagesToShow);
            int totalPages = (TotalCount / pageSize) + (TotalCount % pageSize == 0 ? 0 : 1);
            ViewBag.result = result;
            ViewBag.totalPages = totalPages;
            ViewBag.CurrentPage = CurrentPage;
            var pageCount = result.Count();
            ViewBag.pageCount = pageCount;
            ViewBag.endPage = endPage;
            return View(POR);
        }
    }
}
