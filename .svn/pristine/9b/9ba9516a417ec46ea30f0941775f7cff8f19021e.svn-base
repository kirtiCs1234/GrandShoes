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
    public class StaffStatusController : BaseController
    {
        public ActionResult Index()
        {
            List<StaffStatusModel> StaffStatusModelList =  Services.StaffStatusService.GetAll();
            return View(StaffStatusModelList);
        }
        public ActionResult Details(int? id)
        {
            if(id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StaffStatusModel StaffStatusModelById = Services.StaffStatusService.GetById(id);
            return View(StaffStatusModelById);
        }
    }
}
