using Model;
using POS.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace POS.Areas.Admin.Controllers
{
    public class ProductStyleController : BaseController
    {
        // GET: Admin/ProductStyle
        public ActionResult Index(int? page)
        {
            int TotalCount = 0;
            var pageSize = 10;
            var pageNumber = page ?? 1;
            int CurrentPage = pageNumber;
            var endPage = CurrentPage + 4;
            int PagesToShow = 10;


            ViewBag.PageSize = pageSize;
            var StyleModelList = Services.ProductStyleService.GetPaging(page, out TotalCount);

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
            return View(StyleModelList);
        }

        public ActionResult _Index1(ProductStyleSearch styleSearch, int? page)
        {
            int TotalCount = 0;
            var pageSize = 10;
            var pageNumber = page ?? 1;
            int CurrentPage = pageNumber;
            var endPage = CurrentPage + 4;
            int PagesToShow = 10;
            ViewBag.PageSize = pageSize;
            var StyleModelList = Services.ProductStyleService.GetSearchData(styleSearch, page, out TotalCount);
            ViewBag.TotalCount = TotalCount;

            var result = Helper.CommonFunction.GetPages(TotalCount, pageSize, CurrentPage, PagesToShow);
            int totalPages = (TotalCount / pageSize) + (TotalCount % pageSize == 0 ? 0 : 1);
            ViewBag.result = result;
            ViewBag.totalPages = totalPages;
            ViewBag.CurrentPage = CurrentPage;
            var pageCount = result.Count();
            ViewBag.pageCount = pageCount;
            ViewBag.endPage = endPage;
            return View(StyleModelList);

        }
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(ProductStyleModel model)
        {
            bool status = Services.ProductStyleService.Create(model);
            return RedirectToAction("Index","ProductStyle");
        }
        public ActionResult Details(int id)
        {
            var ProductStyle = Services.ProductStyleService.Details(id);
            return View(ProductStyle);
        }
        public ActionResult Edit(int id)
        {
            var ProductStyle = Services.ProductStyleService.Details(id);
            return View(ProductStyle);
        }
        [HttpPost]
        public ActionResult Edit(ProductStyleModel model)
        {
            bool status = Services.ProductStyleService.Edit(model);
            return RedirectToAction("Index", "ProductStyle");
        }
        public ActionResult Delete(int id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductStyleModel StyleModelById = Services.ProductStyleService.Details(id);

            if (StyleModelById == null)
            {
                return HttpNotFound();
            }
            return View(StyleModelById);
        }
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(ProductStyleModel model)
        {
            if (model.Id > 0)
            {
                ProductStyleModel status = Services.ProductStyleService.Delete(model);
                TempData["Success"] = "Data Deleted Successfully!";
                return RedirectToAction("Index", "ProductStyle");
            }
            return View(model);
        }
    }
}