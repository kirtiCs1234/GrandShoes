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
    [CustomAuth(PageSession.ProductCat1)]
    public class ProductCat1Controller : BaseController
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
            var AreaModelList = Services.ProductCat1Service.GetPaging(page, out TotalCount);

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
            return View(AreaModelList);
        }

        public ActionResult _Index1(ProductCatSearch areaSearch, int? page)
        {
            int TotalCount = 0;
            var pageSize = 10;
            var pageNumber = page ?? 1;
            int CurrentPage = pageNumber;
            var endPage = CurrentPage + 4;
            int PagesToShow = 10;
            ViewBag.PageSize = pageSize;
            var AreaModelList = Services.ProductCat1Service.GetSearchData(areaSearch, page, out TotalCount);
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
        public ActionResult Details(int? Id)
        {
            var data = Services.ProductCat1Service.GetById(Id);
            return View(data);
        }
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(ProductCat1Model model)
        {
            bool status = Services.ProductCat1Service.Create(model);
            return RedirectToAction("Index");
        }
        public ActionResult Edit(int? Id)
        {
            var data = Services.ProductCat1Service.GetById(Id);
            return View(data);
        }
        [HttpPost]
        public ActionResult Edit(ProductCat1Model model)
        {
            bool status = Services.ProductCat1Service.Edit(model);
            return RedirectToAction("Index");
        }
        public ActionResult Delete(int? Id)
        {
            var data = Services.ProductCat1Service.GetById(Id);
            return View(data);
        }
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int? Id)
        {
            bool ststus = Services.ProductCat1Service.Delete(Id);
            return RedirectToAction("Index");
        }
    }
}