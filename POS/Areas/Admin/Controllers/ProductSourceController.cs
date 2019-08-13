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
    [CustomAuth(PageSession.ProductSource)]
    public class ProductSourceController : BaseController
    {
        // GET: Admin/ProductSource
        public ActionResult Index(int? page)
        {
            int TotalCount = 0;
            var pageSize = 10;
            var pageNumber = page ?? 1;
            int CurrentPage = pageNumber;
            var endPage = CurrentPage + 4;
            int PagesToShow = 10;
            var ProductSourceModelList = Services.ProductSourceService.GetPaging(page, out TotalCount);
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
            return View(ProductSourceModelList);

        }
        public ActionResult _Index1(ProductSourceSearch productSourceSearch, int? page)
        {
            int TotalCount = 0;
            var pageSize = 10;
            var pageNumber = page ?? 1;
            int CurrentPage = pageNumber;
            var endPage = CurrentPage + 4;
            int PagesToShow = 10;
            
                var body = JsonConvert.SerializeObject(productSourceSearch);
             var ProductSourceModelList = Services.ProductSourceService.GetSearchData(productSourceSearch, page, out TotalCount);
           
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
            return View(ProductSourceModelList);

        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            ProductSourceModel ProductSourceModelById = Services.ProductSourceService.GetById(id);
            return View(ProductSourceModelById);
        }
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(ProductSourceModel productSource)
        {
            var errors = ModelState.Values.SelectMany(v => v.Errors);
            if (ModelState.IsValid)
            {
                bool ProductSourceCreate = Services.ProductSourceService.Create(productSource);
                TempData["Success"] = "Data Saved Successfully!";
                return RedirectToAction("Index", "ProductSource");
            }
            return View(productSource);

        }
        public ActionResult Edit(int? id)

        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var ProductSourceModelById = Services.ProductSourceService.GetById(id);
            return View(ProductSourceModelById);
        }

        [HttpPost]
        public ActionResult Edit(ProductSourceModel productSource)
        {
            if (ModelState.IsValid)
            {

                bool ProductSourceEdit = Services.ProductSourceService.Edit(productSource);
                TempData["Success"] = "Data Saved Successfully!";
                return RedirectToAction("Index", "ProductSource");
            }
            return View(productSource);
        }
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductSourceModel ProductSourceModelById = Services.ProductSourceService.GetById(id);

            if (ProductSourceModelById == null)
            {
                return HttpNotFound();
            }
            return View(ProductSourceModelById);
        }
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(ProductSourceModel productSource)
        {
            if (productSource.Id > 0)
            {
                bool ProductSourceDelete = Services.ProductSourceService.Delete(productSource);
                TempData["Success"] = "Data Deleted Successfully!";
                return RedirectToAction("Index", "ProductSource");
            }
            return View(productSource);
        }
        public ActionResult CheckSource(ProductSourceModel model)
        {
            var iExist = Services.ProductSourceService.CheckSource(model);
            return Json(!iExist, JsonRequestBehavior.AllowGet);
        }
    }
}