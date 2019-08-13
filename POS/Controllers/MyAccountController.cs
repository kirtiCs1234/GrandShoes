
using Helper;
using Model;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using POS.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using System.Web.Security;

namespace POS.Controllers
{

    public class MyAccountController : BaseController
    {
        public ActionResult login()
        {
            if (SessionManagement.CurrentUser != null)
            {
                //if (SessionManagement.CurrentUser.RoleId == (int)Model.Roles.Admin)
                //{
                //    return Redirect("/admin/dashboard/index");
                //}
                //else if (SessionManagement.CurrentUser.RoleId == (int)Model.Roles.CompanyAdmin)
                //{
                //    return Redirect("/company/dashboard/index");
                //}
                //else
                //{
                //    return Redirect("/error/pagenotfound");

                //}
            }
            return View();
        }
        [HttpPost]
        public ActionResult Login(UserLoginModel model)
        {

            var response = new JObject();
            var body = JsonConvert.SerializeObject(model);

            UserModel user = Services.LoginService.Login(model);

            //UserModel user = ServerResponse.Invoke<UserModel>("api/myaccount/login", body, "POST");
            //if (user.ReturnCode == 0)
            //{
            if (user != null)
            {
                if (user.IsActive == true)
                {
                    if (user.RoleID == 2)
                    {
                        CreateAuthenticationTicket(user);

                        response.Add("ReturnCode", 0);
                        response.Add("RedirectTo", "/admin/User/index");
                    }
                }
                else
                {
                    response.Add("ReturnCode", -1);
                    response.Add("ReturnMessage", "Your account is inactive");
                }
            }
            
            //return Json(JsonConvert.SerializeObject(response));
            //return View(user);
            return RedirectToAction("Index", "Admin/Welcome");
        }
        // GET: MyAccount
        private void CreateAuthenticationTicket(UserModel model)
        {
            SerializationModel serializationModel = new SerializationModel();
            serializationModel.FirstName = model.FirstName;
            serializationModel.LastName = model.LastName;
            serializationModel.Email = model.Email;
            serializationModel.Id = model.Id;
            serializationModel.RoleId = model.RoleID ?? 0;

            JavaScriptSerializer serializer = new JavaScriptSerializer();
            string userData = serializer.Serialize(serializationModel);
            FormsAuthenticationTicket authTicket = new FormsAuthenticationTicket(1, model.Email, DateTime.Now, DateTime.Now.AddHours(8), false, userData);
            string encTicket = FormsAuthentication.Encrypt(authTicket);
            HttpCookie faCookie = new HttpCookie(FormsAuthentication.FormsCookieName, encTicket);
            Response.Cookies.Add(faCookie);
        }
        //public ActionResult Login()
        //{

        //    return View();
        //}
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Login(UserLoginModel model)
        //{

        //   var login = Services.UserService.Login(model);
        //    if (login != null)
        //    {
        //        Session["Email"] = login.Email.ToString();
        //        Session["Password"] = login.Password.ToString();
        //        return RedirectToAction("Index", "Admin/User");
        //    }
        //    else if (model.Email == " " && model.Password == " ")
        //    {
        //        Session["Email"] = "";
        //        Session["Password"] = "";
        //        return RedirectToAction("Login", "MyAccount");
        //    }
        //    else if(login==null)
        //    {

        //            ViewBag.message = "Email and Password is not correct.";


        //    }
        //    return View(model);
        //}

        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();
            Session.Abandon();
            Session.Clear();
            return RedirectToAction("Login");
        }
        public ActionResult CheckUserEmail(UserLoginModel user)
        {
            var iExist = Services.UserService.CheckUserEmail1(user);
            return Json(!iExist, JsonRequestBehavior.AllowGet);
        }
        public ActionResult ForgetPassword()
        {
            return View();
        }
        [HttpPost]
        public ActionResult ForgetPassword(string email)
        {
            var result = Services.UserService.GetUserByEmail(email);
            if (result != null)
            {

                EmailHandler.SendMail(result.Email, "", "", "Forget Password", "<a href='" + Request.Url.AbsoluteUri.ToLower().Replace("/MyAccount/ForgotPassword", "/MyAccount/ResetForgotPassword/") + result.Id + "'> Click here </a>&nbsp; to reset your password ", null, null);
                //TempData["Status"] = "Success";
                //TempData["Message"] = "Link sent to your account.Please login to reset your password.";
                //return RedirectToAction("thankyou");
                //return Json(true);
                return RedirectToAction("ResetForgetPassword/" + result.Id);
            }
            else
            {
                //TempData["Status"] = "UnSuccess";
                //TempData["Message"] = "Sorry, Unable to find your email.";
                //return RedirectToAction("thankyou");
                return Json(false);
            }
            //return RedirectToAction("Index","MyAccount");
        }
        public ActionResult ResetForgetPassword(int id)
        {
            return View();
        }
        [HttpPost]
        public ActionResult ResetForgetPassword(UserLoginModel user)
        {
            bool result = Services.UserService.ChangePassword(user);
            if (result == true)
            {
                //TempData["Success"] = "Your password has been successfully changed. Please login now.";
                return RedirectToAction("Login", "MyAccount");
            }
            else
            {
                ViewBag.Message = "Sorry, Unable to change your password.";
                return View();
            }
        }
        public ActionResult ResetPassword()
        {
            return View();
        }
        [HttpPost]
        public ActionResult ResetPassword(UserLoginModel model)
        {
            var data = Session["Email"].ToString();
            var Id = Services.UserService.GetUserByEmail(data).Id;
            model.Id = Id;
            bool user = Services.UserService.ResetPassword1(model);
            if (user == true)
            {
                Session.Abandon();
                return RedirectToAction("Index", "Area");

            }
            else
            {
                ViewBag.Message = "Invalid Old Password";
            }
            // ViewBag.returnmessage = returnMessage;
            return View();
        }
    }
}