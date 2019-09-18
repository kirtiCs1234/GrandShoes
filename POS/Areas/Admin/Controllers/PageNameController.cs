using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace POS.Areas.Admin.Controllers
{
    [RoutePrefix("api/page")]
    public class PageNameController : ApiController
    {
        private GrandShoesEntities db = new GrandShoesEntities();
        public PageNameController()
        {
            db.Configuration.LazyLoadingEnabled = false;
            db.Configuration.ProxyCreationEnabled = false;
        }
        [HttpGet]
        [AllowAnonymous]
        [Route("getAll")]
        public IHttpActionResult GetFreeGift()
        {
            var list = db.PageNames.Where(x => x.IsActive == true).ToList();
            return Ok(list);
        }
    }
}
