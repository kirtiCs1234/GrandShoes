using DAL;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace POSApi.Controllers
{
    [RoutePrefix("api/myaccount")]
    public class MyAccountController : ApiController
    {
        private GrandShoesEntities db = new GrandShoesEntities();
        public MyAccountController()
        {
            db.Configuration.LazyLoadingEnabled = false;
            db.Configuration.ProxyCreationEnabled = false;
        }
        [HttpPost]
        [AllowAnonymous]
        [Route("login")]
        public IHttpActionResult GetFreeGift(UserLoginModel model)
        {
            User list = db.Users.Where(x => x.Email == model.Email && x.Password == model.Password).FirstOrDefault();
            return Ok(list);
        }
    }
}
