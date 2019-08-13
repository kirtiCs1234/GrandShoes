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
    [CustomAuth(PageSession.BranchCartonDispach)]
    public class BranchCartonDispachController : BaseController
    {
        // GET: Admin/BranchCartonDispach
        public ActionResult Index()
        {
            var SummaryList = Services.StockDistributionSummaryService.GetAll();
            ViewBag.DistributionSummaryID = new SelectList(SummaryList, "Id", "Id");
            
            return View();
        }
        public ActionResult DispatchDetail(string BranchName,int? DistributionSummaryID)
        {
            var list = Services.IBTService.GetDataBranch(BranchName, DistributionSummaryID);
            if (list.Count != 0)
            {
                ViewBag.cartonCount = list.Count();
            }
            return View(list);
        }
        public JsonResult AutoCompleteBranch(string name)
        {
            var BranchLists = Services.IBTService.BranchAutocomplete(name);
            return Json(BranchLists, JsonRequestBehavior.AllowGet);
        }
        public JsonResult Dispatch(int? id)
        {
            var CartonId = Services.CartonManagementService.GetById(id);
			CartonId.IsDispatched = true;
            bool Edit = Services.CartonManagementService.Edit(CartonId);
            return Json(new { id = id }, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public ActionResult GetCartonById(int? id)
        {
            var CartonList = Services.IBTService.GetByCartonId(id);
           // var list = ReceiptOrderList.Where(x => x.TotalQuantity != 0 && x.TotalCost != 0).ToList();
            return View(CartonList);
        }
		public ActionResult ViewPrint(int? id)
		{
			var data = Services.IBTService.GetByIdCarton(id);
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
			return View(data);
		}

	}
}