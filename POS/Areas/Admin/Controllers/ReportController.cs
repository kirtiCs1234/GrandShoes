using Helper;
using Model;
using Model.Report;
using POS.Controllers;
using System.Collections.Generic;
using System.Web.Mvc;

namespace POS.Areas.Admin.Controllers
{
    [CustomAuth(PageSession.Report)]
    public class ReportController : BaseController
    {
        // GET: Admin/Report
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult BranchStockStatusReport()
        {
            var allBranch = Services.ReportService.GetAllBranch();// ServerResponse.Invoke<Dictionary<int,string>>("api/report/branch/getAll", "", "GET");
            var model = Services.ReportService.GetAllbranchStockStatus();// ServerResponse.Invoke<List<BranchStockStatusReport>>("api/report/branchStockStatus/getAll", "", "GET");
            ViewData["AllBranch"] = allBranch;
            return View(model);
        }
        [HttpPost]
        public ActionResult _BranchSTockStatusReport(InputBranchStockStatusReportModel inputmodel)
        {
            var body = Newtonsoft.Json.JsonConvert.SerializeObject(inputmodel);
            var allBranch = Services.ReportService.GetAllBranch();// ServerResponse.Invoke<Dictionary<int, string>>("api/report/branch/getAll", "", "GET");
            var model = Services.ReportService.GetAllbranchStockStatus(); 
            var model2 = ServerResponse.Invoke<List<BranchStockStatusReport>>("api/report/branchStockStatus/filter", body, "POST");
            ViewData["AllBranch"] = allBranch;
            return PartialView(model2);
        }
        public ActionResult StaffCommition()
        {//GetAllStaff()
            var model = Services.ReportService.GetAllStaff();// ServerResponse.Invoke<List<StaffCommitionListModel>>("api/report/staff/getAll", "", "GET");
            return View(model);
        }
        public ActionResult _StaffCommition(int id, int? BranchId, string SaleType)
        {
            var model = Services.ReportService.GetAllStaff();// ServerResponse.Invoke<List<StaffCommitionListModel>>("api/report/staff/getAll", "", "GET");
            return PartialView(model);
        }

        public ActionResult GNRReport()//Reciept Report
        {//GetAllStaff()
            var model = ServerResponse.Invoke<List<GNRReciept>>("api/report/gnr/getAll", "", "GET");
            return View(model);
        }
        public ActionResult _GNRReport(int id, int? BranchId, string SaleType)
        {
            var model = ServerResponse.Invoke<List<GNRReciept>>("api/report/gnr/getAll", "", "GET");
            return PartialView(model);
        }

        public ActionResult DailySalesReport()
        {
            var BranchId = Services.BranchService.GetAll();
            ViewBag.BranchId = new SelectList(BranchId, "Id", "Name");
            var model = ServerResponse.Invoke<List<DailySellReport>>("api/report/dailySellbyBranch/getAll?BranchId="+0+"&date="+System.DateTime.Now+"&TimeMethod=month", "", "GET");
            return View(model);
        }
        [HttpPost]
        public ActionResult _DailySalesReport(int? BranchId, string date, string TimeMethod)
        {
            var model = ServerResponse.Invoke<List<DailySellReport>>("api/report/dailySellbyBranch/getAll?BranchId=" + BranchId + "&date=" + date + "&TimeMethod="+TimeMethod, "", "GET");
            return PartialView(model);
        }
        public ActionResult DailySalesReportSummary()
        {
            var BranchId = Services.BranchService.GetAll();
            ViewBag.BranchId = new SelectList(BranchId, "Id", "Name");
            var model = ServerResponse.Invoke<List<DailySellSummaryModel>>("api/report/dailySellSummary/getAll?date=" + System.DateTime.Now + "&TimeMethod=day", "", "GET");
            return View(model);
        }
        [HttpPost]
        public ActionResult _DailySalesReportSummary(string date, string TimeMethod)
        {
            var model = ServerResponse.Invoke<List<DailySellSummaryModel>>("api/report/dailySellSummary/getAll?date=" + date + "&TimeMethod=" + TimeMethod, "", "GET");
            return PartialView(model);
        }
        public ActionResult OutstandingPurchaseOrderReport()
        {
            var model = ServerResponse.Invoke<List<OutstandingPurchaseOrderReport>>("api/report/outstandingPurchaseOrderReport", "", "GET");
            return View(model);
        }
        [HttpPost]
        public ActionResult _OutstandingPurchaseOrderReport(string date, string TimeMethod)
        {
            var model = ServerResponse.Invoke<List<DailySellSummaryModel>>("api/report/dailySellSummary/getAll?date=" + date + "&TimeMethod=" + TimeMethod, "", "GET");
            return PartialView(model);
        }


        public ActionResult TransactionEnquiryReport()
        {
            var obj = new HelpReportModel();
            var body = Newtonsoft.Json.JsonConvert.SerializeObject(obj);
            var BranchId = Services.BranchService.GetAll();
            ViewBag.BranchId = new SelectList(BranchId, "Id", "Name");
            var model = ServerResponse.Invoke<List<TransactionEnquiryModel>>("api/report/transactionEnquiry", body ,"POST");
            return View(model);
        }
        [HttpPost]
        public ActionResult _TransactionEnquiryReport(HelpReportModel help)
        {
            var body = Newtonsoft.Json.JsonConvert.SerializeObject(help);
            var model = ServerResponse.Invoke<List<TransactionEnquiryModel>>("api/report/transactionEnquiry", body, "POST");
            return PartialView(model);
        }
    }
}