using Helper;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace POS.Areas.Admin.Controllers
{
    [CustomAuth]
    public class WelcomeController : Controller
    {
        // GET: Admin/Welcome
        public ActionResult Index(string PageName)
        {
            if (!string.IsNullOrEmpty(PageName))
            {
                ViewBag.PageName = PageName;
            }
            int CurrencyId = 0;
            var session = SessionManagement.CurrentUser;
            if (session == null)
            {
                return Redirect("/myaccount/login");
            }
            var model = ServerResponse.Invoke<UserModel>("api/user/getDetail?id=" + session.Id, "", "GET");
            var BranchName = ServerResponse.Invoke<BranchModel>("api/branch/getDetail?id=" + model.BranchID, "", "GET");
            ViewBag.BranchName = BranchName.Name;
            return View(model);
        }
    }
}