using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using POS.Areas.Admin.Suggestion;
using Newtonsoft.Json;
using POS.Controllers;
using Model.StockDistribution;
using Helper;

namespace POS.Areas.Admin.Controllers
{
    [CustomAuth(PageSession.Suggestion)]
    public class SuggestionController : BaseController
	{
		public SuggestionController()
		{

		}

		// GET: Admin/Suggestion
		public ActionResult Index()
		{
			var Data = Services.ProductService.GetAllProduct().OrderBy(c=>c.Id).ToList();
			var lists = new List<OptionHelper>();
			foreach (var item in Data)
			{
				OptionHelper op = new OptionHelper();
				op.ID = item.Id;
				op.Value = "(" + item.ProductSKU + ") + (" + item.StyleSKU + ")";
				lists.Add(op);
			}
			ViewBag.productList = new SelectList(lists, "ID", "Value");

			return View();
		}

		public JsonResult GetSuggestion(DateTime date, int productId)
		{
			var Data = SuggestionsForMinDays.GetSuggestion(date.Date, productId, "testString");
			return Json(Data);
		}

		public ActionResult GetProductSizes(int productId, int branchID)
		{
			var Data = Services.StockBranchInventoryService.GetAll().Where(c => c.BranchId == branchID && c.ProductId == productId).FirstOrDefault();
			Dictionary<string, string> dictData = new Dictionary<string, string>();
			dictData.Add("Quantity01", Data.Quantity01.ToString());
			dictData.Add("Quantity02", Data.Quantity02.ToString());
			dictData.Add("Quantity03", Data.Quantity03.ToString());
			dictData.Add("Quantity04", Data.Quantity04.ToString());
			dictData.Add("Quantity05", Data.Quantity05.ToString());
			dictData.Add("Quantity06", Data.Quantity06.ToString());
			dictData.Add("Quantity07", Data.Quantity07.ToString());
			dictData.Add("Quantity08", Data.Quantity08.ToString());
			dictData.Add("Quantity09", Data.Quantity09.ToString());
			dictData.Add("Quantity10", Data.Quantity10.ToString());
			dictData.Add("Quantity11", Data.Quantity11.ToString());
			dictData.Add("Quantity12", Data.Quantity12.ToString());
			dictData.Add("Quantity13", Data.Quantity13.ToString());
			dictData.Add("Quantity14", Data.Quantity14.ToString());
			dictData.Add("Quantity15", Data.Quantity15.ToString());
			dictData.Add("Quantity16", Data.Quantity16.ToString());
			dictData.Add("Quantity17", Data.Quantity17.ToString());
			dictData.Add("Quantity18", Data.Quantity18.ToString());
			dictData.Add("Quantity19", Data.Quantity19.ToString());
			dictData.Add("Quantity20", Data.Quantity20.ToString());
			dictData.Add("Quantity21", Data.Quantity21.ToString());
			dictData.Add("Quantity22", Data.Quantity22.ToString());
			dictData.Add("Quantity23", Data.Quantity23.ToString());
			dictData.Add("Quantity24", Data.Quantity24.ToString());
			dictData.Add("Quantity25", Data.Quantity25.ToString());
			dictData.Add("Quantity26", Data.Quantity26.ToString());
			dictData.Add("Quantity27", Data.Quantity27.ToString());
			dictData.Add("Quantity28", Data.Quantity28.ToString());
			dictData.Add("Quantity29", Data.Quantity29.ToString());
			dictData.Add("Quantity30", Data.Quantity30.ToString());
			return Json(dictData, JsonRequestBehavior.AllowGet);
		}


		[HttpPost]
		public ActionResult insertSuggestion(List<StockTransferDetail> data)
		{
			var status = Services.StockTransferService.InsertSuggestions(data);
			if (status)
				return Json(true);
			else
				return Json(false);
		}
	}
}