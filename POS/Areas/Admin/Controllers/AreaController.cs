﻿using Helper;
using Model;
using Newtonsoft.Json;
using PagedList;
using POS.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using Helper;
using System.Web.Mvc;

namespace POS.Areas.Admin.Controllers
{
    [CustomAuth(PageSession.Area)]
    public class AreaController : BaseController
    {
		// GET: Admin/Area
		
		public ActionResult Index( int? page)
        {
            int TotalCount = 0;
            var pageSize = 10;
            var pageNumber = page ?? 1;
            int CurrentPage = pageNumber;
            var endPage = CurrentPage + 4;
            int PagesToShow = 10;
            ViewBag.PageSize = pageSize;
            var AreaModelList = Services.AreaService.GetPaging(page, out TotalCount);
            ViewBag.TotalCount = TotalCount;
            var result = CommonFunction.GetPages(TotalCount, pageSize, CurrentPage, PagesToShow);
            int totalPages = (TotalCount / pageSize) + (TotalCount % pageSize == 0 ? 0 : 1);
            ViewBag.result = result;
            ViewBag.totalPages = totalPages;
            ViewBag.CurrentPage = CurrentPage;
            var pageCount = result.Count();
            ViewBag.pageCount = pageCount;
            ViewBag.endPage = endPage;
            return View(AreaModelList);
        }
        public ActionResult _Index1( AreaSearch areaSearch,int? page)
        {
            int TotalCount = 0;
            var pageSize = 10;
            var pageNumber = page ?? 1;
            int CurrentPage = pageNumber;
            var endPage = CurrentPage + 4;
            int PagesToShow = 10;           
            ViewBag.PageSize = pageSize;
           var AreaModelList = Services.AreaService.GetSearchData(areaSearch, page, out TotalCount);
           ViewBag.TotalCount = TotalCount;          
            var result = CommonFunction.GetPages(TotalCount, pageSize, CurrentPage, PagesToShow);
            int totalPages = (TotalCount / pageSize) + (TotalCount % pageSize == 0 ? 0 : 1);
            ViewBag.result = result;
            ViewBag.totalPages = totalPages;
            ViewBag.CurrentPage = CurrentPage;
            var pageCount = result.Count();
            ViewBag.pageCount = pageCount;
            ViewBag.endPage = endPage;
            return View(AreaModelList);
        }
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            List<UserModel> UserModelList = Services.UserService.GetAll();
            ViewBag.ManagerID = new SelectList(UserModelList, "Id","FullName");

            AreaModel AreaModelById = Services.AreaService.GetById(id);
            return View(AreaModelById);
        }
        public ActionResult Create()
        {
            List<UserModel> UserModelList = Services.UserService.GetAll();
            ViewBag.ManagerID = new SelectList(UserModelList, "Id", "FullName");
            return View();
        }
        [HttpPost]
        public ActionResult Create(AreaModel area)
        {
            var errors = ModelState.Values.SelectMany(v => v.Errors);
            if (ModelState.IsValid)
            {
                List<UserModel> UserModelList = Services.UserService.GetAll();
                ViewBag.ManagerID = new SelectList(UserModelList, "Id", "FullName");
                // user.IsActive = true;
                bool AreaCreate = Services.AreaService.Create(area);
                TempData["Success"] = "Data Saved Successfully!";
                return RedirectToAction("Index", "Area");
            }
            return View(area);

        }
        public ActionResult Edit(int? id)

        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            List<UserModel> UserModelList = Services.UserService.GetAll();
            ViewBag.ManagerID = new SelectList(UserModelList, "Id", "FullName");
            AreaModel AreaModelById = Services.AreaService.GetById(id);
            return View(AreaModelById);
        }

        [HttpPost]
        public ActionResult Edit(AreaModel area)
        {
            if (ModelState.IsValid)
            {
                List<UserModel> UserModelList = Services.UserService.GetAll();
                ViewBag.ManagerID = new SelectList(UserModelList, "Id", "FullName");
                AreaModel AreaEdit = Services.AreaService.Edit(area);
                TempData["Success"] = "Data Saved Successfully!";
                return RedirectToAction("Index", "Area");
            }
            return View(area);
        }
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AreaModel AreaModelById = Services.AreaService.GetById(id);

            if (AreaModelById == null)
            {
                return HttpNotFound();
            }
            return View(AreaModelById);
        }
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(AreaModel area)
        {
            if (area.Id > 0)
            {
                AreaModel AreaDelete = Services.AreaService.Delete(area);
                TempData["Success"] = "Data Deleted Successfully!";
                return RedirectToAction("Index", "Area");
            }
            return View(area);
        }
    }
}