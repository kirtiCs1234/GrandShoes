﻿using Helper;
using Model;
using Newtonsoft.Json;
using POS.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace POS.Areas.Admin.Controllers
{
    [CustomAuth(PageSession.Offers)]
    public class OffersController : BaseController
    {
        // GET: Admin/Offers
        public ActionResult Index(int? page)
        {
            List<ColorModel> ColorModelList = Services.ColorService.GetAll();
            ViewBag.ColorId = new SelectList(ColorModelList, "Id", "ColorShort");
            List<ProductCat1Model> Cat1List = Services.ProductCategoryService.GetCat1List();
            List<ProductCat2Model> Cat2List = Services.ProductCategoryService.GetCat2List();
            List<ProductCat3Model> Cat3List = Services.ProductCategoryService.GetCat3List();
            List<ProductCat4Model> Cat4List = Services.ProductCategoryService.GetCat4List();
            ViewBag.ProdCat1ID = new SelectList(Cat1List, "Id", "CateName");
            ViewBag.ProdCat2ID = new SelectList(Cat2List, "Id", "CateName");
            ViewBag.ProdCat3ID = new SelectList(Cat3List, "Id", "CateName");
            ViewBag.ProdCat4ID = new SelectList(Cat4List, "Id", "CateName");
            int TotalCount = 0;
            var pageSize = 6;
            var pageNumber = page ?? 1;
            int CurrentPage = pageNumber;
            var endPage = CurrentPage + 4;
            int PagesToShow = 10;
            ViewBag.PageSize = pageSize;
            var ProductModelList = Services.ProductService.GetPaging(page, out TotalCount);
            ViewBag.TotalCount = TotalCount;
            var result = Helper.CommonFunction.GetPages(TotalCount, pageSize, CurrentPage, PagesToShow);
            int totalPages = (TotalCount / pageSize) + (TotalCount % pageSize == 0 ? 0 : 1);
            ViewBag.result = result;
            ViewBag.totalPages = totalPages;
            ViewBag.CurrentPage = CurrentPage;
            var pageCount = result.Count();
            ViewBag.pageCount = pageCount;
            ViewBag.endPage = endPage;
            return View(ProductModelList);
        }
       [HttpPost]
        public ActionResult _Index(int? page, ProdSearch prodSearch)
        {
            int TotalCount = 0;
            var pageSize = 6;
            var pageNumber = page ?? 1;
            int CurrentPage = pageNumber;
            var endPage = CurrentPage + 4;
            int PagesToShow = 10;
            ViewBag.PageSize = pageSize;
            OffersModel model = new OffersModel();
            var ProductModelList= Services.ProductService.GetProduct(prodSearch,page, out TotalCount);
            ViewBag.TotalCount = TotalCount;
            model.ProductList = ProductModelList;
            var result = Helper.CommonFunction.GetPages(TotalCount, pageSize, CurrentPage, PagesToShow);
            int totalPages = (TotalCount / pageSize) + (TotalCount % pageSize == 0 ? 0 : 1);
            ViewBag.result = result;
            ViewBag.totalPages = totalPages;
            ViewBag.CurrentPage = CurrentPage;
            var pageCount = result.Count();
            ViewBag.pageCount = pageCount;
            ViewBag.endPage = endPage;
            return View(model);
        }
     
        [HttpGet]
        public JsonResult AutoCompleteColorList(string name)
        {
            var ColorLists = Services.ColorService.ColoeAutocomplete(name);
            return Json(ColorLists, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public JsonResult AutoCompleteSizeList(string name)
        {
            var SizeGridLists = Services.SizeGridService.SizeGridAutocomplete(name);
            return Json(SizeGridLists, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public JsonResult AutoCompleteSeasonList(string name)
       {
            var ColorLists = Services.SeasonService.SeasonAutocomplete(name);
            return Json(ColorLists, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public JsonResult AutoCompleteStyleSKUList(string name)
        {
            var StyleLists = Services.ProductService.StyleAutocomplete(name);
            return Json(StyleLists, JsonRequestBehavior.AllowGet);
        }
		
    }
}