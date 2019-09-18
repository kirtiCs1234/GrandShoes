using POS.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace POS.Areas.Admin
{
    public class CartonDispatchController : BaseController
    {
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult GetData()
        {
            var summaryList = Services.StockDistributionSummaryService.GetAllSummary();
            var branchList = Services.BranchService.GetAll();
            ViewBag.BranchId = new SelectList(branchList, "Id", "Name");
            ViewBag.DistributionSummaryID = new SelectList(summaryList, "Id", "Id");
            var data = Services.IBTService.GetLastSummaryData();
            return View(data);
        }
        public ActionResult CartonDetail()
        {
            return View();
        }
        public JsonResult GetByIBTNumber(string id)
        {
            var data = Services.IBTService.GetByIBTNumber(id);
            if (data != null)
            {
                data.BranchName = data.Branch.Name;
            }
            return Json(data,JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult Dispatch(List<int> CartonList)
        {
            bool status = Services.IBTService.DispatchData(CartonList);
            return Json(status,JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult SearchData(int? summaryID, int? branchId)
        {
            var data = Services.IBTService.GetSearchData(summaryID, branchId);
            foreach(var item in data)
            {
                item.BranchName = item.Branch.Name;
            }
            return Json(data,JsonRequestBehavior.AllowGet);
        }
    }
}