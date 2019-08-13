using Helper;
using Model;
using POS.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace POS.Areas
{
    public class SessionManager
    {
        public MyPrincipal getCompanyId()
        {
            return SessionManagement.CurrentUser;
        }
        public bool CheckSession()
        {
            if (getCompanyId() != null)
                return true;
            else
                return false;
        }
        public MyPrincipal getSession()
        {
            if (CheckSession())
            {
                return SessionManagement.CurrentUser;
            }
            else
            {
                return null;
            }
        }
    }
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, Inherited = true, AllowMultiple = true)]
    public class CustomAuth : AuthorizeAttribute
    {
        ServiceClass db = new ServiceClass();
        SessionManager sm = new SessionManager();
        private int PageId = 0;
        private int ActionId = 0;
        private int BranchId = 0;
        private int RoleId = 0;
        private int UserId = 0;

        private PagePermissionModel CompanyPage;
        private Model.ActionPageModel ActionPage;

        bool flag = false;
        public CustomAuth()
        {
            flag = true;
        }
        public CustomAuth(int PageId)
        {
            flag = false;
            var ses = sm.CheckSession();
            var session = sm.getSession();

            if (sm.CheckSession())
            {
                this.PageId = PageId;
                this.ActionId = ActionId;
                BranchId = session.BranchId ?? 0;
                RoleId = session.RoleId;
                UserId = session.Id;
            }
        }

        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {

            if (filterContext.HttpContext.Request.IsAjaxRequest())
            {
                filterContext.Result = new HttpStatusCodeResult(400, "Unauthorized request");
            }
            else if (flag == true)
            {
                if (SessionManagement.CurrentUser == null)
                    filterContext.Result = new RedirectResult("~/myaccount/login");
                if (SessionManagement.CurrentUser != null)
                    filterContext.Result = new RedirectResult("~/myaccount/login");
            }
            //else if (IsSubscribed() == false)
            //{
            //    filterContext.Result = new HttpStatusCodeResult(400, "Unauthorized request");

            //}
            //else if (OnTrail())
            //{
            //    filterContext.Result = new HttpStatusCodeResult(400, "Unauthorized request");

            //}
            else
            {
                var Page = ServerResponse.Invoke<PageNameModel>("api/page/getOne?PageId=" + PageId, "", "GET");
                //var ActionName = 

                var returnAction = new RouteValueDictionary//("/Welcome/Index?PageName=" + Page.Page);
                                {

                                    { "action", "Index" },
                                    { "controller", "Welcome" },
                                    { "PageName",Page.Page },

                                };

                var GoToLogin = new RouteValueDictionary
                {
                    { "action", "login" },
                                    { "controller", "myaccount" },
                    {"area","" }
                };
                if (sm.CheckSession() == true)
                {
                    filterContext.Result = new RedirectToRouteResult(returnAction);
                }
                else
                    filterContext.Result = new RedirectToRouteResult(GoToLogin);
                //base.HandleUnauthorizedRequest(filterContext);
            }
        }
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            if (flag == true)
            {
                if (SessionManagement.CurrentUser == null)
                    return false;
                else
                    return true;
            }
            else
            {
                var ses = sm.CheckSession();
                var session = sm.getSession();

                if (sm.CheckSession())
                {
                    this.PageId = PageId;
                    this.ActionId = ActionId;
                    BranchId = session.BranchId ?? 0;
                    RoleId = session.RoleId;
                    UserId = session.Id;
                }

                if (sm.CheckSession())
                {
                    if (IsAdmin())
                    {
                        return false;
                    }
                    CompanyPage = getPage();
                    // ActionPage = getAction();
                    if (CompanyPage != null)// && ActionPage != null)
                    {
                        return true;
                        //if (ActionId == 0)
                        //{
                        //    return false;
                        //}
                        //else
                        //{
                        //    if (ActionPage != null)
                        //    {
                        //        return true;
                        //    }
                        //    else
                        //    {
                        //        return false;
                        //    }
                        //}
                    }

                }
                return false;
            }
        }

        private PagePermissionModel getPage()
        {
            var result = ServerResponse.Invoke<PagePermissionModel>("api/permission/getPagePermission?BranchId=" + BranchId + "&RoleId=" + RoleId + "&PageId=" + PageId, "", "GET");
            return result;
        }
        //private Model.ActionPageModel getAction()
        //{
        //    if (CompanyPage == null)
        //    {
        //        return null;
        //    }
        //    var CompanyPageId = CompanyPage.Id;
        //    var result = ServerResponse.Invoke<Model.ActionPageModel>("api/permission/getAction?CompanyId=" + CompanyId + "&RoleId=" + RoleId + "&PageId=" + /*Company*/PageId + "&ActionId=" + ActionId, "","GET");
        //    return result;
        //}
        private bool IsAdmin()
        {
            var IsAdmin = false;
            var session = sm.getSession();
            var role = ServerResponse.Invoke<RoleModel>("api/role/getRole?id=" + session.RoleId, "", "GET");
            if (role != null)
            {
                if (role.Id == 1)
                {
                    IsAdmin = true;
                }
            }
            return (bool)IsAdmin;
        }
        //private bool IsSubscribed()
        //{
        //    var package = ServerResponse.Invoke<bool>("api/packagesubscription/isSubscribed?CompanyId=" + CompanyId, "", "GET");
        //    if (package)
        //    {
        //        return true;
        //    }
        //    return false;
        //}
        //private bool OnTrail()
        //{
        //    var package = ServerResponse.Invoke<bool>("api/packagesubscription/isOnTrail?CompanyId=" + CompanyId, "", "GET");
        //    if (package)
        //    {
        //        return true;
        //    }
        //    return false;
        //}
    }
    public static class PageSession
    {
        public const int Area = 1;
        public const int BranchCartonDispach = 2;
        public const int Branch = 3;
        public const int BranchStockReport = 4;
        public const int Buyer = 5;
        public const int Color = 6;
        public const int Discount = 7;
        public const int IBTCarton = 8;
        public const int IBTCartonReport = 9;
        public const int MarkDownBranch = 10;
        public const int Offers = 11;
        public const int ProductCategory = 12;
        public const int Product = 13;
        public const int ProductSource = 14;
        public const int ProductStyle = 15;
        public const int PurchaseOrder = 16;
        public const int PurchaseOrderReport = 17;
        public const int Receipt = 18;
        public const int Season = 19;
        public const int SizeGrid = 20;
        public const int SMIBranchDefault = 21;
        public const int StaffMember = 22;
        public const int StockAudit = 23;
        public const int StockTaKe = 24;
        public const int StoreDelieveryReport = 25;
        public const int Supplier = 26;
        public const int TreeView = 27;
        public const int User = 28;
        public const int CustomerPage = 29;
        public const int Template = 30;
        public const int StockEnquiry = 31;
        public const int StockDistribution = 32;
        public const int Role = 33;
        public const int Report = 34;
        public const int Log = 35;
        public const int CartonManagement = 36;
        public const int CartonManagementReport = 37;
        public const int ProductCat1 = 38;
        public const int ProductCat2 = 39;
        public const int ProductCat3 = 40;
        public const int ProductCat4 = 41;
        public const int WinnerReport = 42;
        public const int Suggestion = 43;
    }
}