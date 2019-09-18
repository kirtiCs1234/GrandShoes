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
    [CustomAuth(PageSession.MarkDownBranch)]
    public class MarkDownBranchController : BaseController
    {
		// GET: Admin/MarkDownBranch
        public List<BranchModel> Branches=new List<BranchModel>();
		public ActionResult Index()
		{
			int id = Convert.ToInt32(Request.QueryString["id"]);
			MarkDownAddModel model = new MarkDownAddModel();
			var BranchList = Services.BranchService.GetAll();
            Branches = BranchList;
			var markDown = Services.MarkDownService.GetAll();
			if (id != 0)
			{
				var data = Services.ProductService.GetAll();
				var dataById = data.Where(x => x.Id == id).FirstOrDefault();
				ViewBag.ProductSKU = dataById.ProductSKU;
				ViewBag.StyleSKU = dataById.StyleSKU;
				var amt1 = dataById.ActualSellingPrice.ToString();
				ViewBag.OriginalSellingPrice =amt1.Substring(0,amt1.Length-2);
				ViewBag.Id = id;
				var markDOwn = Services.MarkDownService.GetByProduct(dataById.ProductSKU, dataById.StyleSKU);
				if (markDOwn.Count > 0)
				{
					model.Count = markDOwn.Count().ToString();
					model.LastMarkDownDate = markDown.LastOrDefault().EffectiveDate.Substring(0, 10);
					var amt = markDown.FirstOrDefault().NewCashPrice.ToString();
                    if (amt != "")
                    {
                        ViewBag.NewCashPrice = amt.Substring(0, amt.Length - 2);
                    }
                    else
                    {
                        ViewBag.NewCashPrice = 0;
                    }
					
				}

				//ViewBag.StyleSKU = dataById.ProductStyle.StyleSKU;
				var amt2 = dataById.ActualSellingPrice;
				ViewBag.OriginalSellingPrice =amt2.Substring(0,amt2.Length-2);
				var productSku = Services.ProductService.GetById(id).ProductSKU;
				//var markDownPrev = markDown.Where(x => x.ProductSKU == productSku).Count();
				//  TempData["Number"] = markDownPrev;
				//  var EffectiveDateLast = markDown.Where(x => x.ProductSKU == productSku).LastOrDefault();
				// if (EffectiveDateLast == null)
				// {
				//    TempData["Date"] = "00/00/0000";
				// }
				//  else
				//    {
				//       TempData["Date"] = EffectiveDateLast.EffectiveDate;
				//  }
			}


			model.MarkDownList = markDown;
			model.BranchList1 = BranchList;
			return View(model);
		}
		public ActionResult Create()
        {
			MarkDownAddModel model = new MarkDownAddModel();
			int id = Convert.ToInt32(Request.QueryString["id"]);
            if (id != 0)
            {
                var data = Services.ProductService.GetAll();
                var dataById = data.Where(x => x.Id == id).FirstOrDefault();
                ViewBag.ProductSKU = dataById.ProductSKU;
                ViewBag.StyleSKU = dataById.StyleSKU;
                ViewBag.OriginalSellingPrice = dataById.ActualSellingPrice;
				var markDown = Services.MarkDownService.GetByProduct(dataById.ProductSKU, dataById.StyleSKU);
				if (markDown.Count > 0)
				{
					ViewBag.Count = markDown.Count().ToString();
					ViewBag.LastMarkDown = markDown.LastOrDefault().EffectiveDate.Substring(0,10);

					ViewBag.NewCashPrice = markDown.LastOrDefault().NewSellingPrice;

					ViewBag.TodayDate= DateTime.Now.ToString("yyyy-M-dd");
				}else if(markDown.Count==0)
				{
					ViewBag.Count = 0;
					ViewBag.LastMarkDown = "none";
					ViewBag.NewCashPrice =0;
					ViewBag.TodayDate = DateTime.Now.ToString("yyyy-M-dd");
				}
			}
            return View(model);
        }
        [HttpPost]
        public JsonResult GetProductValues(string ProductSKU,string StyleSKU)

        {
            var ProductValues = new ProductModel();
            ProductValues = Services.ProductService.GetValues(ProductSKU, StyleSKU);
            var markDown = Services.MarkDownService.GetAll();
            var markDownPrev = markDown.Where(x => x.ProductSKU == ProductSKU && x.StyleSKU == StyleSKU).Count();

            if (ProductValues != null)
            {
                if (markDownPrev != 0)
                {
                    ProductValues.CountMarkDown = markDownPrev;
                    var markDownDataList = markDown.Where(x => x.ProductSKU == ProductSKU && x.StyleSKU==StyleSKU);
					var markDownData = markDownDataList.FirstOrDefault();
					//if (ProductValues.NewCashPrice.ToString() == "0.00")
					//{
						ProductValues.NewCashPrice = Math.Round(Convert.ToDecimal(markDownData.NewSellingPrice),2);

					//}
					//else
					//{
					//	ProductValues.NewCashPrice = markDownData.NewCashPrice;

					//}
					ProductValues.PreDate = markDownData.EffectiveDate.ToString();
                    return Json(ProductValues, JsonRequestBehavior.AllowGet);
                }
                else
                {
					// var ProductValues1 = new ProductModel();
					ProductValues.OriginalSellingPrice= Math.Round(Convert.ToDecimal(ProductValues.ActualSellingPrice),2);
                    ProductValues.CountMarkDown = 0;
					ProductValues.NewCashPrice = Math.Round(Convert.ToDecimal(ProductValues.ActualSellingPrice),2);
                    return Json(ProductValues, JsonRequestBehavior.AllowGet);
                }
                
            }
            else{
                var ProductValues1 = new ProductModel();
               // ProductValues1.ActualSellingPrice = 0;
                
                return Json(ProductValues1, JsonRequestBehavior.AllowGet);
            }

            
        }
        [HttpPost]
        public ActionResult Create(MarkDownAddModel model)
        {
            var errors = ModelState.Values.SelectMany(v => v.Errors);
                bool createMarkDown = Services.MarkDownService.Create(model);
               
                bool productUpdate = Services.ProductService.EditByMark(model.MarkDown);
                TempData["Success"] = "Data Saved successfully!";
                return RedirectToAction("Index", "MarkDownBranch");
        }
        [HttpGet]
        public ActionResult Detail(int? id)
        {
            // int id = Convert.ToInt32(Request.QueryString["id"]);
            var markDown = Services.MarkDownService.GetByDate();
            if (id != null)
            {
                var ProductById = Services.ProductService.GetByIdMarkDown(id).ProductSKU;
                markDown = markDown.Where(x => x.ProductSKU == ProductById).ToList();
            }
            TempData["Success"] = "Data Saved successfully!";
			foreach (var item in markDown)
			{
				if (item.EffectiveDate != null)
				{
					item.EffectiveDate = item.EffectiveDate.Substring(0, 10);

				}
			}
			return View(markDown);
        }
        public ActionResult History()
        {
            var markDown = Services.MarkDownService.GetAll();
			foreach(var item in markDown)
			{
				if (item.EffectiveDate != null)
				{
					item.EffectiveDate = item.EffectiveDate.Substring(0, 10);

				}
			}
            return View(markDown);
        }
        [HttpGet]
        public JsonResult AutoCompleteProductStyleSKUList(string name,string id)
        {
            var StyleSKUList = Services.ProductService.ProductStyleAutocomplete(name,id);
            return Json(StyleSKUList, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public JsonResult AutoCompleteProductSKUList(string name)
        {
            var ProductSKUList = Services.ProductService.ProductAutocomplete(name);
            return Json(ProductSKUList, JsonRequestBehavior.AllowGet);
        }
    }
}