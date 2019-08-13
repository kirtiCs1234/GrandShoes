using Helper;
using LinqToExcel.Extensions;
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
    [CustomAuth(PageSession.Role)]
    public class RoleController : BaseController
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
            var StyleModelList = Services.RoleService.GetPaging(page, out TotalCount);

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
        [OutputCache(Duration = 60)]
        public ActionResult _Index1(RoleModel search, int? page)
        {
            int TotalCount = 0;
            var pageSize = 10;
            var pageNumber = page ?? 1;
            int CurrentPage = pageNumber;
            var endPage = CurrentPage + 4;
            int PagesToShow = 10;
            ViewBag.PageSize = pageSize;
            var StyleModelList = Services.RoleService.GetSearchData(search, page, out TotalCount);
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
        public ActionResult Create(RoleModel model)
        {
            bool status = Services.RoleService.Create(model);
            TempData["Success"] = "Data Saved Successfully!";
            return RedirectToAction("Index", "Role");
        }
        public ActionResult Details(int id)
        {
            var RoleById = Services.RoleService.GetById(id);
            return View(RoleById);
        }
        public ActionResult Edit(int id)
        {
            var RoleById = Services.RoleService.GetById(id);
            return View(RoleById);
        }
        [HttpPost]
        public ActionResult Edit(RoleModel model)
        {
            bool status = Services.RoleService.Edit(model);
            TempData["Success"] = "Data Saved Successfully!";
            return RedirectToAction("Index", "Role");
        }
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RoleModel RoleModelById = Services.RoleService.GetById(id);

            if (RoleModelById == null)
            {
                return HttpNotFound();
            }
            return View(RoleModelById);
        }
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(RoleModel model)
        {
            if (model.Id > 0)
            {
                RoleModel status = Services.RoleService.Delete(model);
                TempData["Success"] = "Data Deleted Successfully!";
                return RedirectToAction("Index", "Role");
            }
            return View(model);
        }

        public ActionResult _GrantPermission(int RolePage)
        {
            var IsAdminPage = false;
            var Page = Services.PageNameService.GetAll(IsAdminPage);
            if (RolePage == 1)
            {
                IsAdminPage = true;
                ViewBag.IsAdminPage = true;
            }
            else { ViewBag.IsAdminPage = false; }
            int BranchId = 0;
            ViewBag.PageName = Services.PageNameService.GetAll(IsAdminPage);
            ViewBag.BranchId = 0;
            ViewBag.getGrantPermission = Services.RoleService.GetGrantPermission();
            var model = Services.RoleService.GetAll();

            return View(model);
        }

        public ActionResult GrantPermission()
        {
            int BranchId = 0;
            // ViewBag.PageName = Services.PageNameService.GetAll();
            ViewBag.BranchId = 0;
            ViewBag.getGrantPermission = Services.RoleService.GetGrantPermission();
            var model = Services.RoleService.GetAll();

            return View(model);
        }
        [HttpPost]
        public ActionResult GrantPermission(PageListModel model)
        {
            //3x3 me first number pagename id and second number is roleId
            var s = new Dictionary<int, string>();
            var role = Services.RoleService.GetAll();
            //var role = db.Result<List<RoleModel>>("api/role/getAllForPagePermission?companyId=" + CompanyId, "", db.get);

            s = Processing(model, role);

            var list = new List<PagePermissionModel>();

            foreach (var first in role)
            {
                foreach (var second in s)
                {
                    if (first.Id == second.Key)
                    {

                        var ls = second.Value.Split('#');
                        foreach (var i in ls)
                        {
                            var newmodel = new PagePermissionModel();
                            if (i != "")
                            {
                                newmodel.RoleId = first.Id;
                                newmodel.PageId = Int32.Parse(i);
                                newmodel.IsActive = true;
                                newmodel.IsAdminPage = model.IsAdminPage;
                                newmodel.PageAction = newmodel.RoleId.ToString() + "x" + newmodel.PageId.ToString();
                                list.Add(newmodel);
                            }

                        }

                    }
                }
            }
            Services.RoleService.SetGrantPermission(list);
            // var model1 = db.Result<List<CompanyPageModel>>("api/role/setGrantPermission?companyId=" + CompanyId, list, db.post);
            //   TempData["Success"] = msg.Success;
            return RedirectToAction("Index", "Role");
        }

        private Dictionary<int, string> Processing(PageListModel model, List<RoleModel> role)
        {
            var dict = new Dictionary<int, string>();
            if (role != null)
            {
                foreach (var item in role)
                {
                    dict.Add(item.Id, "");
                }
            }
            var modelList = Utilities.getKeyVaue(model).ToList();
            var strlist = modelList.Where(x => !string.IsNullOrEmpty(x.Value) && x.Value.Contains("[System.String]")).ToList();
            foreach (var item in strlist)
            {
                var result = model.GetType().GetProperty(item.Key).GetValue(model) as List<string>;
                if (result != null)
                {
                    for (var i = 0; i < result.Count; i++)
                    {
                        var splt = result[i].Split('x');
                        dict[Int32.Parse(splt[1])] += splt[0] + "#";
                    }
                }
            }
            return dict;
        }
    }
}