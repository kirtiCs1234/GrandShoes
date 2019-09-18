using Helper;
using Model;
using POS.Controllers;
using POS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Mvc;

namespace POS.Areas.Admin.Controllers
{
    [CustomAuth(PageSession.SMIBranchDefault)]
    public class SMIBranchDefaultController : BaseController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public ActionResult Index()
        {
            List<BranchModel> AllBranch = Services.BranchService.GetAll();
            return View(AllBranch);
        }
        public ActionResult GetAllSMIBranch(int id)
        {
            BranchModel model = new BranchModel();
            var Model = Services.SMIBranchDefaultService.GetBranchByID(id);
            model.BranchDestination = Model;
            return PartialView("GetAllSMIBranch", model);
        }
        [System.Web.Http.HttpPost]
        public ActionResult SMIBranchDefault(SMIBranchDefaultModel smiListModel)
        {         
            var Model = Services.SMIBranchDefaultService.SMIBranch(smiListModel);
            return RedirectToAction("index");
        }
    }
}
