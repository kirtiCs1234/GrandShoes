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
    public class StaffController : BaseController
    {
        // GET: Admin/Staff
        public ActionResult Index(int? page)
        {
            int TotalCount = 0;
            var pageSize = 10;
            var pageNumber = page ?? 1;
            int CurrentPage = pageNumber;
            int endPage = CurrentPage + 4;
            int PagesToShow = 10;
            var StaffModelList = Services.StaffService.GetPaging(page, out TotalCount);
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
            return View(StaffModelList);

        }
       
        public ActionResult _Index1(StaffSearch staffSearch,int? page)
        {
            int TotalCount = 0;
            var pageSize = 10;
            var pageNumber = page ?? 1;

            int CurrentPage = pageNumber;
            int endPage = CurrentPage + 4;
            int PagesToShow = 10;
            
            ViewBag.TotalCount = TotalCount;
            ViewBag.PageSize = pageSize;
           
                var body = JsonConvert.SerializeObject(staffSearch);
                var StaffModelList = Services.StaffService.GetSearchData(staffSearch, page, out TotalCount);
            
            var result = Helper.CommonFunction.GetPages(TotalCount, pageSize, CurrentPage, PagesToShow);
            int totalPages = (TotalCount / pageSize) + (TotalCount % pageSize == 0 ? 0 : 1);
            ViewBag.result = result;
            ViewBag.totalPages = totalPages;
            ViewBag.CurrentPage = CurrentPage;
            var pageCount = result.Count();
            ViewBag.pageCount = pageCount;

            ViewBag.endPage = endPage;

            return View(StaffModelList);

        }
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            List<UserModel> UserModelList = Services.UserService.GetAll();
            List<BranchModel> BranchModelList = Services.BranchService.GetAll();
            List<DesignationModel> DesignationModelList = Services.DesignationService.GetAll();
            ViewBag.UserID = new SelectList(UserModelList, "Id", "FullName");
            ViewBag.BranchID = new SelectList(BranchModelList, "Id", "Name");
            ViewBag.DesignationID = new SelectList(DesignationModelList, "Id", "DesignationName");
            StaffModel StaffModelById = Services.StaffService.GetById(id);
            return View(StaffModelById);
        }
        public ActionResult Create()
        {
            StaffModel staffModel = new StaffModel();
            var dateAndTime = DateTime.Now;
            var date = dateAndTime.Date;
            staffModel.JoiningDate = date;
            List<UserModel> UserModelList = Services.UserService.GetAll();
            List<BranchModel> BranchModelList = Services.BranchService.GetAll();
            List<DesignationModel> DesignationModelList = Services.DesignationService.GetAll();
            ViewBag.UserID = new SelectList(UserModelList, "Id", "FullName");
            //  ViewBag.UserID = new SelectList(UserModelList, "Id", ("FirstName" + "MiddleName" + "LastName"));
            ViewBag.BranchID = new SelectList(BranchModelList, "Id", "Name");
            ViewBag.DesignationID = new SelectList(DesignationModelList, "Id", "DesignationName");
            return View();
        }
        [HttpPost]
        public ActionResult Create(StaffModel staff)
        {
            var errors = ModelState.Values.SelectMany(v => v.Errors);
            if (ModelState.IsValid)
            {
                staff.IsActive = true;
                List<UserModel> UserModelList = Services.UserService.GetAll();
                List<BranchModel> BranchModelList = Services.BranchService.GetAll();
                List<DesignationModel> DesignationModelList = Services.DesignationService.GetAll();
                ViewBag.UserID = new SelectList(UserModelList, "Id", "FullName");
                // ViewBag.UserID = new SelectList(UserModelList, "Id", ("FirstName" + "MiddleName" + "LastName"));
                ViewBag.BranchID = new SelectList(BranchModelList, "Id", "Name");
                ViewBag.DesignationID = new SelectList(DesignationModelList, "Id", "DesignationName");
                StaffModel StaffCreate = Services.StaffService.Create(staff);
                TempData["Success"] = "Data Saved Successfully!";
                return RedirectToAction("Index", "Staff");
            }
            return View(staff);

        }
        public ActionResult Edit(int? id)

        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            List<UserModel> UserModelList = Services.UserService.GetAll();
            List<BranchModel> BranchModelList = Services.BranchService.GetAll();
            List<DesignationModel> DesignationModelList = Services.DesignationService.GetAll();
            ViewBag.UserID = new SelectList(UserModelList, "Id", "FullName");
            //  ViewBag.UserID = new SelectList(UserModelList, "Id", ("FirstName" + "MiddleName" + "LastName"));
            ViewBag.BranchID = new SelectList(BranchModelList, "Id", "Name");
            ViewBag.DesignationID = new SelectList(DesignationModelList, "Id", "DesignationName");
            StaffModel StaffModelById = Services.StaffService.GetById(id);
            return View(StaffModelById);
        }

        [HttpPost]
        public ActionResult Edit(StaffModel staff)
        {
            if (ModelState.IsValid)
            {
                List<UserModel> UserModelList = Services.UserService.GetAll();
                List<BranchModel> BranchModelList = Services.BranchService.GetAll();
                List<DesignationModel> DesignationModelList = Services.DesignationService.GetAll();
                ViewBag.UserID = new SelectList(UserModelList, "Id", "FullName");
                // ViewBag.UserID = new SelectList(UserModelList, "Id", ("FirstName" + "MiddleName" + "LastName"));
                ViewBag.BranchID = new SelectList(BranchModelList, "Id", "Name");
                ViewBag.DesignationID = new SelectList(DesignationModelList, "Id","DesignationName");
                StaffModel StaffEdit = Services.StaffService.Edit(staff);
                TempData["Success"] = "Data Saved Successfully!";
                return RedirectToAction("Index", "Staff");
            }
            return View(staff);
        }
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StaffModel StaffModelById = Services.StaffService.GetById(id);

            if (StaffModelById == null)
            {
                return HttpNotFound();
            }
            return View(StaffModelById);
        }
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(StaffModel staff)
        {
            if (staff.Id > 0)
            {
                StaffModel StaffDelete = Services.StaffService.Delete(staff);
                TempData["Success"] = "Data Saved Successfully!";
                return RedirectToAction("Index", "Staff");
            }
            return View(staff);
        }
    }
}