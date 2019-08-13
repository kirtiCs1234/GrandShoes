using Model;
using POS.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace POS.Areas.Admin.Controllers
{
    public class PromotionalDiscountController : BaseController
    {
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult GetProductList(DiscountSummaryModel discount)
        {
            var list = Services.DiscountService.Create(discount);
            return View(list);
        }
    }
}