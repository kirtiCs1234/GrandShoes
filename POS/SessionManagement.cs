using POS.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace POS
{
    public static class SessionManagement
    {
        public static MyPrincipal CurrentUser
        {
            get
            {
                if (HttpContext.Current.Request.IsAuthenticated)
                    return (MyPrincipal)HttpContext.Current.User;
                else
                    return null;
            }
        }
    }
    public class CustomAuth : AuthorizeAttribute
    {
        protected override bool AuthorizeCore(HttpContextBase httpContext)

        {
            if (SessionManagement.CurrentUser == null)
                return false;
            else
                return true;
        }
        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            if (SessionManagement.CurrentUser == null)
                filterContext.Result = new RedirectResult("~/myaccount/login");
            if (SessionManagement.CurrentUser != null)
                filterContext.Result = new RedirectResult("~/myaccount/login");
        }
    }
}