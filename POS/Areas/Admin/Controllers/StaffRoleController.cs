using Helper;
using Model;
using POS.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Mvc;

namespace POS.Areas.Admin.Controllers
{
    [CustomAuth]
    public class StaffRoleController : BaseController
    {


        public ActionResult Index()
        {
            List<StaffRoleModel> StaffRoleModelList = Services.StaffRoleService.GetAll();
            return View(StaffRoleModelList);
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }


            StaffRoleModel StaffRoleModelById = Services.StaffRoleService.GetById(id);
            return View(StaffRoleModelById);
        }
    }
}
