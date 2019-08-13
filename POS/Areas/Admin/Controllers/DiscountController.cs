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
    [CustomAuth(PageSession.Discount)]
    public class DiscountController : BaseController
    {
        public ActionResult Index()
        {
            int id = Convert.ToInt32(Request.QueryString["id"]);
            if (id != 0)
            {
                ViewBag.Id = id;
                var data = Services.ProductService.GetAll();
                var dataById = data.Where(x => x.Id == id).FirstOrDefault();
                ViewBag.ProductSKU = dataById.ProductSKU;

                // ViewBag.StyleSKU = dataById.ProductStyle.StyleSKU;
            }
            PromotionalDiscountModel model = new PromotionalDiscountModel();
            var BranchList = Services.BranchService.GetAll();
            //  var discount = Services.DiscountService.GetAll();
            //  model.DiscountList = discount;
            model.BranchList1 = BranchList;
            return View(model);

        }
        public ActionResult Create()
        {
            int id = Convert.ToInt32(Request.QueryString["id"]);
            if (id != 0)
            {
                var data = Services.ProductService.GetAll();
                var dataById = data.Where(x => x.Id == id).FirstOrDefault();
                ViewBag.ProductSKU = dataById.ProductSKU;
                // ViewBag.StyleSKU = dataById.ProductStyle.StyleSKU;
            }
            return View();
        }
        [HttpPost]
        public JsonResult Create(DiscountSummaryModel discount)
        {
            var PromotionlDiscountList = Services.DiscountService.Create(discount);
            return Json(PromotionlDiscountList, JsonRequestBehavior.AllowGet);
        }
        public ActionResult AllowDiscount(PromotionalDiscountModel promotion)
        {
            return View();
        }
        public JsonResult AutoCompleteProductSKUList(string name)
        {
            var ProductSKUList = Services.ProductService.ProductAutocomplete(name);
            return Json(ProductSKUList, JsonRequestBehavior.AllowGet);
        }
        public JsonResult AutoCompleteProductStyleSKUList(string name)
        {
            var StyleSKUList = Services.ProductStyleService.ProductStyleAutocomplete(name);
            return Json(StyleSKUList, JsonRequestBehavior.AllowGet);
        }
        public ActionResult Detail(int? id)
        {
            var DiscountSummaryList = Services.DiscountService.GetProductList(id);
            foreach(var item in DiscountSummaryList)
            {
                if (item.DiscountSummary.FromDate!= null && item.DiscountSummary.ToDate!= null)
                {
                    var DateOpen = item.DiscountSummary.FromDate.Substring(0, item.DiscountSummary.FromDate.Length - 9);
                    item.DiscountSummary.FromDate = DateOpen;
                    var DateClosed = item.DiscountSummary.ToDate.Substring(0, item.DiscountSummary.ToDate.Length - 9);
                    item.DiscountSummary.ToDate = DateClosed;
                }
            }
            TempData["Success"] = "Data Saved successfully!";
            return View(DiscountSummaryList);
        }
        [HttpPost]
        public ActionResult Allow(PromotionalDiscountModel model)
        {
            bool status = Services.DiscountService.AllowDiscount(model);
            return RedirectToAction("Index");
        }
        public ActionResult History()
        {
            var DiscountSummaryList = Services.DiscountService.GetDataSummary();
            TempData["Success"] = "Data Saved successfully!";
            return View(DiscountSummaryList);
        }
        public ActionResult GetReceiptById(int? id)
        {
            var list = Services.DiscountService.GetProtionalDiscount(id);
            foreach(var item in list)
            {
                List<string> termsList = new List<string>();
               foreach (var b in item.DiscountBranches)
               {
                    termsList.Add(b.Branch.Name);
               }
               item.Branches = termsList.Aggregate((s1, s2) => s1 + "," + s2);
            }
            return View(list);
        }
        public ActionResult Delete(int? id)
        {
            var data = Services.DiscountService.GetByIdSummary(id);
            return View(data);
        }
        [HttpPost,ActionName("Delete")]
        public ActionResult DeleteConfirmed(DiscountSummaryModel model)
        {
            bool status = Services.DiscountService.Delete(model);
            return RedirectToAction("Index", "Discount");
        }
    }
}