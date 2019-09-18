using Helper;
using Model;
using POS.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace POS.Areas.Admin.Controllers
{
    public class CartonDispatchController : BaseController
    {
        // GET: Admin/CartonDispatch
        public ActionResult Index()
        {
            var BranchList = Services.BranchService.GetAll();
            ViewBag.BranchID = new SelectList(BranchList, "Id", "Name");
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
        public JsonResult GetByIBTNumber(string id,int? BranchID)
        {
            var data = Services.IBTService.GetByIBTNumber(id, BranchID);
            if (data != null)
            {
                data.BranchName = data.Branch.Name;
            }
            else if (data == null)
            {
                
            }
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult Dispatch(List<int> CartonList)
        {
            bool status = Services.IBTService.DispatchData(CartonList);
            return Json(status, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult SearchData(int? summaryID, int? branchId)
        {
            var data = Services.IBTService.GetSearchData(summaryID, branchId);
            foreach (var item in data)
            {
                item.BranchName = item.Branch.Name;
            }
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        public ActionResult ViewPrint(int? id)
        {
            var data = Services.IBTService.GetByIdCarton(id);
            if (data != null)
            {
                data.PackDate = data.PackDate.Substring(0, 10);
                List<DictIBTModel> list = new List<DictIBTModel>();
                foreach (var item in data.CartonManagementDetails)
                {
                    DictIBTModel model = new DictIBTModel();
                    model.ItemSize = new Dictionary<string, string>();
                    model.QuantitySize = new Dictionary<string, string>();
                    model.ProductId = item.ProductID;
                    model.Product = item.Product;
                    var utility = Utilities.getKeyVaue(item);
                    model.ItemSize = Utilities.getFilterDictionary(utility, "ItemSize"); //utility.Where(x => x.Key.Contains("ItemSize")).ToList();
                    model.QuantitySize = Utilities.getFilterDictionary(utility, "Z"); //utility.Where(x => x.Key.Contains("QuantitySize")).ToList();
                    list.Add(model);
                }
            ViewData["list"] = list;
            }
            return View(data);
        }

    }
}