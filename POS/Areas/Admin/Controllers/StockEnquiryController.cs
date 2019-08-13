﻿using Helper;
using POS.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace POS.Areas.Admin.Controllers
{
    [CustomAuth(PageSession.StockEnquiry)]
    public class StockEnquiryController : BaseController
    {
        // GET: Admin/StockEnquiry
        public ActionResult Index()
        {
          
            return View();
        }
        [HttpPost]
        public ActionResult GetData(Model.StockEnquiryModel searchData)
        {
            var StockDistributionModelList = Services.StockEnquiryService.GetSelectedData(searchData);
            return View(StockDistributionModelList);
        }

        [HttpPost]
        public ActionResult StockData(Model.StockEnquiryModel searchData)
        {
            var StockInventories = Services.StockEnquiryService.GetAll(searchData);
            return View(StockInventories);
        }
    }
}