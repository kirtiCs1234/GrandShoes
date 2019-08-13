using Org.BouncyCastle.Asn1.Ocsp;
using POS.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Script.Serialization;
using System.Web.Security;

namespace POS
{
    public static class CookieHelper
    {
        public static MyPrincipal getUser()
        {
            var authCookie = HttpContext.Current.Request[FormsAuthentication.FormsCookieName];
            if (authCookie != null)
            {
                FormsAuthenticationTicket authTicket = FormsAuthentication.Decrypt(authCookie);
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
                return newUser;
            }
            else
            {
                return null;
            }

        }
    }
}