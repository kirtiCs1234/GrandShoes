using Helper;
using Model;
using Newtonsoft.Json;
using POS.Controllers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace POS.Areas.Admin.Controllers
{
    [CustomAuth(PageSession.StaffMember)]
    public class StaffMemberController : BaseController
    {        
        public ActionResult Index(int? page)
        {
            List<UserModel> UserModelList = Services.UserService.GetAll();
            ViewBag.UserId = new SelectList(UserModelList, "Id", "FullName");
            List<StaffStatusModel> StaffStatusModelList = Services.StaffStatusService.GetAll();
            ViewBag.StaffStatusId = new SelectList(StaffStatusModelList, "Id", "StatusName");
            int TotalCount = 0;
            var pageSize = 10;
            var pageNumber = page ?? 1;
            int CurrentPage = pageNumber;
            var endPage = CurrentPage + 4;
            int PagesToShow = 10;
            ViewBag.PageSize = pageSize;
            var StaffMemberModelList = Services.StaffMemberService.GetPaging(page, out TotalCount);
            ViewBag.TotalCount = TotalCount;
            var result = Helper.CommonFunction.GetPages(TotalCount, pageSize, CurrentPage, PagesToShow);
            int totalPages = (TotalCount / pageSize) + (TotalCount % pageSize == 0 ? 0 : 1);
            ViewBag.result = result;
            ViewBag.totalPages = totalPages;
            ViewBag.CurrentPage = CurrentPage;
            var pageCount = result.Count();
            ViewBag.pageCount = pageCount;
            ViewBag.endPage = endPage;
            return View(StaffMemberModelList);
        }
       
        public ActionResult _Index1(StaffMemberSearch staffMemberSearch, int? page)
        {
            List<UserModel> UserModelList = Services.UserService.GetAll();
            ViewBag.UserId = new SelectList(UserModelList, "Id", "FullName");
            List<StaffStatusModel> StaffStatusModelList = Services.StaffStatusService.GetAll();
            ViewBag.StaffStatusId = new SelectList(StaffStatusModelList, "Id", "StatusName");
            int TotalCount = 0;
            var pageSize = 10;
            var pageNumber = page ?? 1;
            int CurrentPage = pageNumber;
            var endPage = CurrentPage + 4;
            int PagesToShow = 10;
            ViewBag.PageSize = pageSize;
            var StaffMemberModelList = Services.StaffMemberService.GetSearchData(staffMemberSearch, page, out TotalCount);
            ViewBag.TotalCount = TotalCount;
            var result = Helper.CommonFunction.GetPages(TotalCount, pageSize, CurrentPage, PagesToShow);
            int totalPages = (TotalCount / pageSize) + (TotalCount % pageSize == 0 ? 0 : 1);
            ViewBag.result = result;
            ViewBag.totalPages = totalPages;
            ViewBag.CurrentPage = CurrentPage;
            var pageCount = result.Count();
            ViewBag.pageCount = pageCount;
            ViewBag.endPage = endPage;
            return View(StaffMemberModelList);
        }
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            List<UserModel> UserModelList = Services.UserService.GetAll();
            ViewBag.UserId = new SelectList(UserModelList, "Id", "FullName");
            List<StaffStatusModel> StaffStatusModelList = Services.StaffStatusService.GetAll();
            ViewBag.StaffStatusId = new SelectList(StaffStatusModelList, "Id", "StatusName");
            StaffMemberModel StaffMemberModelById = Services.StaffMemberService.GetById(id);
            return View(StaffMemberModelById);
        }
        public ActionResult Create()
        {
            List<UserModel> UserModelList = Services.UserService.GetAll();
            ViewBag.UserId = new SelectList(UserModelList, "Id", "FullName");

            List<StaffStatusModel> StaffStatusModelList = Services.StaffStatusService.GetAll();
            ViewBag.StaffStatusId = new SelectList(StaffStatusModelList, "Id", "StatusName");
            return View();
        }
        [HttpPost]
        public ActionResult Create(StaffMemberModel staffMember,HttpPostedFileBase file)
        {
            var errors = ModelState.Values.SelectMany(v => v.Errors);
            if (ModelState.IsValid)
            {
                if (file != null)
                {
                    var allowedExtensions = new[] { ".jfif", ".Jpg", ".png", ".jpg", "jpeg" };
                    staffMember.ProfilePic = file.ToString();
                    var fileName = Path.GetFileName(file.FileName);
                    var ext = Path.GetExtension(file.FileName);
                    if (allowedExtensions.Contains(ext))
                    {
                        string name1 = Path.GetFileNameWithoutExtension(fileName); //getting file name without extension  
                        string myfile1 = name1 + "_" + staffMember.Id + ext; //appending the name with id                                                                             
                        var path = Path.Combine(Server.MapPath("~/Image/StaffMember"), myfile1);  // store the file inside ~/project folder(Img)
                        var path1 = myfile1;
                        staffMember.ProfilePic = path1;
                        file.SaveAs(path);
                    }
                }
              

                staffMember.IsActive = true;
                bool StaffMemberCreate = Services.StaffMemberService.Create(staffMember);
                TempData["Success"] = "Data Saved Successfully!";
                return RedirectToAction("Index", "StaffMember");
            }
            return View(staffMember);
        }
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            List<UserModel> UserModelList = Services.UserService.GetAll();
            ViewBag.UserId = new SelectList(UserModelList, "Id", "FullName");
            List<StaffStatusModel> StaffStatusModelList = Services.StaffStatusService.GetAll();
            ViewBag.StaffStatusId = new SelectList(StaffStatusModelList, "Id", "StatusName");
            StaffMemberModel StaffMemberModelById = Services.StaffMemberService.GetById(id);
            return View(StaffMemberModelById);
        }

        [HttpPost]
        public ActionResult Edit(StaffMemberModel staffMember, HttpPostedFileBase file)
        {
                if (file != null)
                {
                    var allowedExtensions = new[] { ".jfif", ".Jpg", ".png", ".jpg", "jpeg" };
                    staffMember.ProfilePic = file.ToString();
                    var fileName = Path.GetFileName(file.FileName);
                    var ext = Path.GetExtension(file.FileName);
                    if (allowedExtensions.Contains(ext))
                    {
                        string name1 = Path.GetFileNameWithoutExtension(fileName); //getting file name without extension  
                        string myfile1 = name1 + "_" + staffMember.Id + ext; //appending the name with id                                                                              // store the file inside ~/project folder(Img) 
                        var path = Path.Combine(Server.MapPath("~/Image/StaffMember"), myfile1);
                        var path1 = myfile1;
                        staffMember.ProfilePic = path1;
                        file.SaveAs(path);
                    }
                }
                else
                {
                    var getImage = Services.StaffMemberService.GetById(staffMember.Id);
                    staffMember.ProfilePic = getImage.ProfilePic;
                }
                bool staff = Services.StaffMemberService.Edit(staffMember);

                TempData["Success"] = "Data Saved successfully!";
                return RedirectToAction("Index", "StaffMember");
           
          //  return View(staffMember);
        }
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StaffMemberModel StaffMemberModelById = Services.StaffMemberService.GetById(id);

            if (StaffMemberModelById == null)
            {
                return HttpNotFound();
            }
            return View(StaffMemberModelById);
        }
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(StaffMemberModel staffMember)
        {
            if (staffMember.Id > 0)
            {
                bool StaffMemberDelete = Services.StaffMemberService.Delete(staffMember);
                TempData["Success"] = "Data saved successfully!";
                return RedirectToAction("Index", "StaffMember");
            }
            return View(staffMember);
        }
        //Disable Chechk
        [HttpPost]
        public ActionResult DisableChechk(StaffMemberModel model)
        {
            bool Status = Services.StaffMemberService.Disable(model);
			return RedirectToAction("Index","StaffMember");
		}
        //Enable Chechk
        [HttpPost]
        public ActionResult EnableChechk(StaffMemberModel model)
        {
            bool status = Services.StaffMemberService.Enable(model);
			return RedirectToAction("Index","StaffMember");
        }
    }
}
