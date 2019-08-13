using POS.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Script.Serialization;
using System.Web.Security;

namespace POS
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }


        protected void Application_PostAuthenticateRequest(Object sender, EventArgs e)
        {
            var authCookie = Request.Cookies[FormsAuthentication.FormsCookieName];
            if (authCookie != null)
            {
                FormsAuthenticationTicket authTicket = FormsAuthentication.Decrypt(authCookie.Value);
                JavaScriptSerializer serializer = new JavaScriptSerializer();
                SerializationModel serializeModel = serializer.Deserialize<SerializationModel>(authTicket.UserData);

                MyPrincipal newUser = new MyPrincipal(authTicket.Name);
                newUser.Id = serializeModel.Id;
                newUser.FirstName = serializeModel.FirstName;
                newUser.LastName = serializeModel.LastName;
                newUser.Email = serializeModel.Email;
                newUser.RoleId = serializeModel.RoleId;
                newUser.BranchId = serializeModel.BranchId;
                if (serializeModel.RoleId == 1)
                {
                    newUser.IsAdmin = true;
                }
                else
                {
                    newUser.IsAdmin = false;
                }
                HttpContext.Current.User = newUser;
            }
        }
    }
}
