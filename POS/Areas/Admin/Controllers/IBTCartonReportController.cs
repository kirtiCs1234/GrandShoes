﻿using Helper;
using Model;
using POS.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace POS.Areas.Admin.Controllers
{
    [CustomAuth(PageSession.IBTCartonReport)]
    public class IBTCartonReportController : BaseController
    {
        // GET: Admin/IBTCartonReport
        public ActionResult Index(int? page)
        {
            int TotalCount = 0;
            var pageSize = 10;
            var pageNumber = page ?? 1;
            int CurrentPage = pageNumber;
            var endPage = CurrentPage + 4;
            int PagesToShow = 10;


            ViewBag.PageSize = pageSize;
            var CartonModelList = Services.CartonManagementService.GetPaging(page, out TotalCount);

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
            return View(CartonModelList);
           
        }
        public ActionResult _Index1(CartonManagementModel search, int? page)
        {
            int TotalCount = 0;
            var pageSize = 10;
            var pageNumber = page ?? 1;
            int CurrentPage = pageNumber;
            var endPage = CurrentPage + 4;
            int PagesToShow = 10;
            ViewBag.PageSize = pageSize;
            var CartonModelList = Services.CartonManagementService.GetSearchData(search, page, out TotalCount);
            ViewBag.TotalCount = TotalCount;

            var result = Helper.CommonFunction.GetPages(TotalCount, pageSize, CurrentPage, PagesToShow);
            int totalPages = (TotalCount / pageSize) + (TotalCount % pageSize == 0 ? 0 : 1);
            ViewBag.result = result;
            ViewBag.totalPages = totalPages;
            ViewBag.CurrentPage = CurrentPage;
            var pageCount = result.Count();
            ViewBag.pageCount = pageCount;
            ViewBag.endPage = endPage;
            return View(CartonModelList);

        }
        [HttpGet]
        public ActionResult GetCartonById(int? id)
        {
            var list = Services.CartonManagementDetailService.ByCartonID(id);
            return View(list);
        }
    }
}