using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Helper
{
    //public class SessionManagement
    //{
    //    public static string CurrentUser
    //    {
    //        get
    //        {
    //            return (string)HttpContext.Current.Session["Email"];
    //        }
    //    }
    //}

    //public class CustomAuth : AuthorizeAttribute
    //{
    //    protected override bool AuthorizeCore(HttpContextBase httpContext)

    //    {
    //        if (SessionManagement.CurrentUser == null)
    //            return false;
    //        else
    //            return true;
    //    }
    //    protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
    //    {
    //        if (SessionManagement.CurrentUser == null)
    //            filterContext.Result = new RedirectResult("~/myaccount/login");
    //        if (SessionManagement.CurrentUser != null)
    //            filterContext.Result = new RedirectResult("~/myaccount/login");
    //    }
    //}

}
