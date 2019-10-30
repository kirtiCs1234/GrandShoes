﻿using Helper;
using Model;
using Newtonsoft.Json;
using POS.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace POS.Areas.Admin.Controllers
{
    [CustomAuth(PageSession.Buyer)]
    public class BuyerController : BaseController
    {
       
        public ActionResult Index(int? page)
        {
            int TotalCount = 0;
            var pageSize = 10;
            var pageNumber = page ?? 1;
            int CurrentPage = pageNumber;
            var endPage = CurrentPage + 4;
            int PagesToShow = 10;
            ViewBag.PageSize = pageSize;
            var BuyerModelList = Services.BuyerService.GetPaging(page, out TotalCount);
            ViewBag.TotalCount = TotalCount;       
            var result = Helper.CommonFunction.GetPages(TotalCount, pageSize, CurrentPage, PagesToShow);
            int totalPages = (TotalCount / pageSize) + (TotalCount % pageSize == 0 ? 0 : 1);
            ViewBag.result = result;
            ViewBag.totalPages = totalPages;
            ViewBag.CurrentPage = CurrentPage;
            var pageCount = result.Count();
            ViewBag.pageCount = pageCount;
            ViewBag.endPage = endPage;
            return View(BuyerModelList);
        }
       
        public ActionResult _Index1(BuyerSearch buyerSearch, int? page)
        {
            int TotalCount = 0;
            var pageSize = 10;
            var pageNumber = page ?? 1;
            int CurrentPage = pageNumber;
            var endPage = CurrentPage + 4;
            int PagesToShow = 10;
            ViewBag.PageSize = pageSize;
            var BuyerModelList = Services.BuyerService.GetSearchData(buyerSearch, page, out TotalCount);
            ViewBag.TotalCount = TotalCount;
            var result = Helper.CommonFunction.GetPages(TotalCount, pageSize, CurrentPage, PagesToShow);
            int totalPages = (TotalCount / pageSize) + (TotalCount % pageSize == 0 ? 0 : 1);
            ViewBag.result = result;
            ViewBag.totalPages = totalPages;
            ViewBag.CurrentPage = CurrentPage;
            var pageCount = result.Count();
            ViewBag.pageCount = pageCount;
            ViewBag.endPage = endPage;
            return View(BuyerModelList);
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            BuyerModel BuyerModelById = Services.BuyerService.GetById(id);
            return View(BuyerModelById);
        }
        public ActionResult Create()
        {
           
            return View();
        }
        [HttpPost]
        public ActionResult Create(BuyerModel buyer)
        {
            var errors = ModelState.Values.SelectMany(v => v.Errors);
            if (ModelState.IsValid)
            {

                buyer.IsActive = true;
                BuyerModel BuyerCreate = Services.BuyerService.Create(buyer);
                TempData["Success"] = "Data Saved Successfully!";
                return RedirectToAction("Index", "Buyer");
            }
            return View(buyer);

        }
        public ActionResult Edit(int? id)

        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            BuyerModel BuyerModelById = Services.BuyerService.GetById(id);
            return View(BuyerModelById);
        }

        [HttpPost]
        public ActionResult Edit(BuyerModel buyer)
        {
            if (ModelState.IsValid)
            {

                BuyerModel BuyerEdit = Services.BuyerService.Edit(buyer);
                TempData["Success"] = "Data Saved Successfully!";
                return RedirectToAction("Index", "Buyer");
            }
            return View(buyer);
        }
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BuyerModel BuyerModelById = Services.BuyerService.GetById(id);

            if (BuyerModelById == null)
            {
                return HttpNotFound();
            }
            return View(BuyerModelById);
        }
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(BuyerModel buyer)
        {
            if (buyer.Id > 0)
            {
                BuyerModel BuyerDelete = Services.BuyerService.Delete(buyer);
                TempData["Success"] = "Data Deleted successfully!";
                return RedirectToAction("Index", "Buyer");
            }
            return View(buyer);
        }
    }
}