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
    [CustomAuth(PageSession.ProductCat2)]
    public class ProductCat2Controller : BaseController
    {
        // GET: Admin/ProductCat2
        public ActionResult Index(int? page)
        {
            int TotalCount = 0;
            var pageSize = 10;
            var pageNumber = page ?? 1;
            int CurrentPage = pageNumber;
            var endPage = CurrentPage + 4;
            int PagesToShow = 10;
            ViewBag.PageSize = pageSize;
            var AreaModelList = Services.ProductCat2Service.GetPaging(page, out TotalCount);
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
            var AreaModelList = Services.ProductCat2Service.GetSearchData(areaSearch, page, out TotalCount);
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
            var data = Services.ProductCat2Service.GetById(Id);
            return View(data);
        }
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(ProductCat2Model model)
        {
            bool status = Services.ProductCat2Service.Create(model);
            return RedirectToAction("Index");
        }
        public ActionResult Edit(int? Id)
        {
            var data = Services.ProductCat2Service.GetById(Id);
            return View(data);
        }
        [HttpPost]
        public ActionResult Edit(ProductCat2Model model)
        {
            bool status = Services.ProductCat2Service.Edit(model);
            return RedirectToAction("Index");
        }
        public ActionResult Delete(int? Id)
        {
            var data = Services.ProductCat2Service.GetById(Id);
            return View(data);
        }
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int? Id)
        {
            bool ststus = Services.ProductCat2Service.Delete(Id);
            return RedirectToAction("Index");
        }
    }
}