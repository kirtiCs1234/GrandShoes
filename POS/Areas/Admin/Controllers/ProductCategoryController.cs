using Helper;
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
    [CustomAuth]
    public class ProductCategoryController : BaseController
    {
        // GET: Admin/ProductCategory
        public ActionResult Index(int? page)
        {
            int TotalCount = 0;
            var pageSize = 10;
            var pageNumber = page ?? 1;
            int CurrentPage = pageNumber;
            var endPage = CurrentPage + 4;
            int PagesToShow = 10;


            ViewBag.PageSize = pageSize;
            var ProductCategoryModelList = Services.ProductCategoryService.GetPaging(page, out TotalCount);

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
            return View(ProductCategoryModelList);

        }
       
        public ActionResult _Index1(ProductCategorySearch productCategorySearch,int? page)
        {
            int TotalCount = 0;
            var pageSize = 10;
            var pageNumber = page ?? 1;
            int CurrentPage = pageNumber;
            var endPage = CurrentPage + 4;
            int PagesToShow = 10;

            ViewBag.PageSize = pageSize;
            var ProductCategoryModelList = Services.ProductCategoryService.GetSearchData(productCategorySearch, page, out TotalCount);
            ViewBag.TotalCount = TotalCount;

            ViewBag.TotalCount = TotalCount;
            var result = Helper.CommonFunction.GetPages(TotalCount, pageSize, CurrentPage, PagesToShow);
            int totalPages = (TotalCount / pageSize) + (TotalCount % pageSize == 0 ? 0 : 1);
            ViewBag.result = result;
            ViewBag.totalPages = totalPages;
            ViewBag.CurrentPage = CurrentPage;
            var pageCount = result.Count();
            ViewBag.pageCount = pageCount;

            ViewBag.endPage = endPage;
           

            return View(ProductCategoryModelList);

        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
          
            ProductCategoryModel ProductCategoryModelById = Services.ProductCategoryService.GetById(id);
            return View(ProductCategoryModelById);
        }
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(ProductCategoryModel productCategory)
        {
            var errors = ModelState.Values.SelectMany(v => v.Errors);
            if (ModelState.IsValid)
            {
                ProductCategoryModel ProductCategoryCreate = Services.ProductCategoryService.Create(productCategory);
                TempData["Success"] = "Data Saved Successfully!";
                return RedirectToAction("Index", "ProductCategory");
            }
            return View(productCategory);

        }
        public ActionResult Edit(int? id)

        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            ProductCategoryModel ProductCategoryModelById = Services.ProductCategoryService.GetById(id);
            return View(ProductCategoryModelById);
        }

        [HttpPost]
        public ActionResult Edit(ProductCategoryModel productCategory)
        {
            if (ModelState.IsValid)
            {

                ProductCategoryModel ProductCategoryEdit = Services.ProductCategoryService.Edit(productCategory);
                TempData["Success"] = "Data Saved Successfully!";
                return RedirectToAction("Index", "ProductCategory");
            }
            return View(productCategory);
        }
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductCategoryModel ProductCategoryModelById = Services.ProductCategoryService.GetById(id);

            if (ProductCategoryModelById == null)
            {
                return HttpNotFound();
            }
            return View(ProductCategoryModelById);
        }
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(ProductCategoryModel productCategory)
        {
            if (productCategory.Id > 0)
            {
                ProductCategoryModel ProductCategoryDelete = Services.ProductCategoryService.Delete(productCategory);
                TempData["Success"] = "Data Deleted Successfully!";
                return RedirectToAction("Index", "ProductCategory");
            }
            return View(productCategory);
        }
        public JsonResult CheckProductCategoryCode(ProductCategoryModel model)
        {
            var iExist = Services.ProductCategoryService.CheckProductCategoryCode(model);
            return Json(!iExist, JsonRequestBehavior.AllowGet);
        }
    }
}