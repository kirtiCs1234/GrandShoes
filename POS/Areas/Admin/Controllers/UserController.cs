using Helper;
using POS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Model;
using System.Web.Mvc;
using Newtonsoft.Json;
using PagedList;
using System.Net;
using Model.User;
using POS.Controllers;
using OfficeOpenXml;
using System.IO;

namespace POS.Areas.Admin.Controllers
{
    [CustomAuth(PageSession.User)]
    public class UserController : BaseController
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        ServiceClass sc = new ServiceClass();
        public ActionResult Index(int? page)
        {
            var pData = TempData["ProcessData"];
            if (pData != null)
            {
                ViewBag.processData = pData;
            }
            int TotalCount = 0;
            var pageSize = 10;
            var pageNumber = page ?? 1;
            int CurrentPage = pageNumber;
            var endPage = CurrentPage + 4;
            int PagesToShow = 10;
            var UserModelList = Services.UserService.GetPaging(page, out TotalCount);
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

            return View(UserModelList);
        }
       
        public ActionResult _Index1(UserSearch userSearch, int? page)
        {
            int TotalCount = 0;
            var pageSize = 10;
            var pageNumber = page ?? 1;
            int CurrentPage = pageNumber;
            var endPage = CurrentPage + 4;
            int PagesToShow = 10;
            
            ViewBag.TotalCount = TotalCount;
            ViewBag.PageSize = pageSize;
            
                var body = JsonConvert.SerializeObject(userSearch);
               var UserModelList = Services.UserService.GetSearchData(userSearch, page, out TotalCount);
            
            var result = CommonFunction.GetPages(TotalCount, pageSize, CurrentPage, PagesToShow);
            int totalPages = (TotalCount / pageSize) + (TotalCount % pageSize == 0 ? 0 : 1);
            ViewBag.result = result;
            ViewBag.totalPages = totalPages;
            ViewBag.CurrentPage = CurrentPage;
            var pageCount = result.Count();
            ViewBag.pageCount = pageCount;

            ViewBag.endPage = endPage;

            return View(UserModelList);
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            List<BranchModel> BranchModelList = Services.BranchService.GetAll();
            List<RoleModel> RoleModelList = Services.RoleService.GetAll();
            ViewBag.BranchId = new SelectList(BranchModelList, "Id", "Name");
            ViewBag.RoleId = new SelectList(RoleModelList, "Id", "RoleName");

            UserModel UserModelById = Services.UserService.GetById(id);
            return View(UserModelById);
        }
        public ActionResult Create()
        {
            List<BranchModel> BranchModelList = Services.BranchService.GetAll();
            List<RoleModel> RoleModelList = Services.RoleService.GetAll();
            ViewBag.BranchId = new SelectList(BranchModelList, "Id", "Name");
            ViewBag.RoleId = new SelectList(RoleModelList, "Id", "RoleName");
            return View();
        }
        [HttpPost]
        public ActionResult Create(UserModel user)
        {
           
                user.IsActive = true;
                List<BranchModel> BranchModelList = Services.BranchService.GetAll();
                List<RoleModel> RoleModelList = Services.RoleService.GetAll();
                ViewBag.BranchId = new SelectList(BranchModelList, "Id", "Name");
                ViewBag.RoleId = new SelectList(RoleModelList, "Id", "RoleName");
                bool UserCreate = Services.UserService.Create(user);
                TempData["Success"] = "Data Saved Successfully!";
                return RedirectToAction("Index", "User");
           
        }
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            List<BranchModel> BranchModelList = Services.BranchService.GetAll();
            List<RoleModel> RoleModelList = Services.RoleService.GetAll();
            ViewBag.BranchId = new SelectList(BranchModelList, "Id", "Name");
            ViewBag.RoleId = new SelectList(RoleModelList, "Id", "RoleName");
            UserModel UserModelById = Services.UserService.GetById(id);
            return View(UserModelById);
        }
		public ActionResult ExcelUpload()
		{
			return View();
		}
		[HttpPost]
		public ActionResult ExcelUpload(UserModel user, HttpPostedFileBase ExcelFile)
		{
             var Email = new Dictionary<int, string>();
            Dictionary<string, string> process = new Dictionary<string, string>();
            Dictionary<int, UserModel> users = new Dictionary<int, UserModel>();
            Dictionary<int, UserModel> TempUsers = new Dictionary<int, UserModel>();
            Dictionary<int, UserModel> updateusers = new Dictionary<int, UserModel>();
            string filePath = string.Empty;
            if (ExcelFile != null)
            {
                string path = Server.MapPath("~/File/");
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                filePath = path + Path.GetFileName(ExcelFile.FileName);
                string extension = Path.GetExtension(ExcelFile.FileName);
                ExcelFile.SaveAs(filePath);
                string csvData = System.IO.File.ReadAllText(filePath);
                var csv = csvData.Split('\n').Length;
                var row = csvData.Split('\n');
                int i = 0;
                int j = 0;
                for (i = 1; i < csv; i++)
                {
                    try
                    {
                        if (!string.IsNullOrEmpty(row[i]) && !row[i].StartsWith(",,,,,"))
                        {
                            j++;
                            var rowSplit = row[i].Split(',');
                            var model = new UserModel();
                            process.Add(j + "#" + rowSplit[1], "");
                            var name = rowSplit[0];
                            string[] sizes = name.Split(' ');
                            int c = sizes.Count();
                            if (c == 3)
                            {
                                model.FirstName = sizes[0];
                                model.MiddleName = sizes[1];
                                model.LastName = sizes[2];
                            }
                            else if (c == 2)
                            {
                                model.FirstName = sizes[0];

                                model.LastName = sizes[1];
                            }
                            else if (c == 1)
                            {
                                model.FirstName = sizes[0];
                            }
                            model.Email = rowSplit[1];
                            model.Phone = rowSplit[2];
                            model.Password = rowSplit[3];
                            var roleName = rowSplit[4];
                            model.RoleID = Services.RoleService.GetByRoleName(roleName).Id;
                            var branchName = rowSplit[5];
                            model.BranchID = Services.BranchService.GetByName(branchName).Id;
                            model.IsVerified = rowSplit[6].Equals("1") ? true : false;
                            model.IsActive = true;
                            string chk = rowSplit[1];
                            TempUsers.Add(i, model);
                            Email.Add(i, model.Email);
                        }
                    }
                    catch (Exception ex)
                    {
                        //error loging stuff
                        if (ex.Message != null)
                        {
                            process[j + "#" + row[i].Split(',')[1]] = ex.Message;
                        }
                    }
                }

                var check = Services.UserService.CheckEmailDict(Email);
                if (TempUsers != null && TempUsers.Count > 0)
                {
                    foreach (var item in TempUsers)
                    {
                        if (check[item.Key] == false)
                        {
                            users.Add(item.Key, item.Value);
                            process[item.Key + "#" + item.Value.Email] = "Add";
                        }
                        else
                        {
                            updateusers.Add(item.Key, item.Value);
                            process[item.Key + "#" + item.Value.Email] = "Update";
                        }
                    }
                }
            }
            var addList = Services.UserService.CreateList(users);
            var updateList = Services.UserService.UpdateList(updateusers);
            var dictionaryFrom = new Dictionary<string, string>();
            dictionaryFrom = sc.getFilterData(addList, updateList, process);
            TempData["ProcessData"] = dictionaryFrom;
            TempData["Success"] = "Data Uploaded Successfully!";
			return RedirectToAction("Index", "User");
		}
		public ActionResult ExportList()
		{
			var data = Services.UserService.GetAll();
			ExcelPackage excel = new ExcelPackage();
			var workSheet = excel.Workbook.Worksheets.Add("Sheet1");
			workSheet.Cells[1, 2].LoadFromCollection(data, true);
			using (var memoryStream = new MemoryStream())
			{
				Response.ContentType = "application/vnd.ms-excel";
				Response.AddHeader("content-disposition", "attachment;  filename=UserExportData.xlsx");
				excel.SaveAs(memoryStream);
				memoryStream.WriteTo(Response.OutputStream);
				Response.Flush();
				//Response.End();
			}
			return View();
		}
		[HttpPost]
        public ActionResult Edit(UserModel user)
        {
           
                bool UserEdit = Services.UserService.Edit(user);
                TempData["Success"] = "Data Saved Successfully!";
                return RedirectToAction("Index", "User");
          
        }
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserModel UserModelById = Services.UserService.GetById(id);

            if (UserModelById == null)
            {
                return HttpNotFound();
            }
            return View(UserModelById);
        }
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(UserModel user)
        {
            if (user.Id > 0)
            {
                UserModel UserDelete = Services.UserService.Delete(user);
                TempData["Success"] = "Data Saved Successfully!";
                return RedirectToAction("Index", "User");
            }
            return View(user);
        }
        public ActionResult Approve(int? id)
        {
            UserModel UserModelById = Services.UserService.GetById(id);
            UserModelById.IsVerified = true;
            bool UserEdit = Services.UserService.Edit(UserModelById);
            return Json(new { id = id }, JsonRequestBehavior.AllowGet);
            //return View();
        }

        public ActionResult CheckUserPhoneNumber(UserModel user)
        {
            var iExist = Services.UserService.CheckUserPhone(user);
            return Json(!iExist, JsonRequestBehavior.AllowGet);
        }
        public ActionResult CheckUserEmail(UserModel user)
        {
            var iExist = Services.UserService.CheckUserEmail(user);
            return Json(!iExist, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public ActionResult ResetPassword(int? id)
        {
            UserModel UserModelById = Services.UserService.GetById(id);
            ViewBag.Id = id;
            return View();
        }
        [HttpPost]
        public ActionResult ResetPassword(UserResetPassword urp)
        {
            bool UserResetPassword = Services.UserService.ResetPassword(urp);
            TempData["Success"] = "Data Deleted Successfully!";
            return RedirectToAction("Index", "User");
        }
     
    }
}