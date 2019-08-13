using Helper;
using Model;
using Model.ForStockTransfer;
using Model.StockDistribution;
using POS.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace POS.Areas.Admin.Controllers
{
    [CustomAuth(PageSession.IBTCarton)]
    public class IBTCartonController : BaseController
    {
        // GET: Admin/IBTCarton
        public ActionResult Index()
        {
            return View();
        }
        
        public JsonResult AutoCompleteBranch(string name)
        {
            var BranchLists = Services.IBTService.BranchAutocomplete(name);
            return Json(BranchLists, JsonRequestBehavior.AllowGet);
        }
        public ActionResult CartonDetail()
        {
            //var list = Services.IBTService.GetCartonDetail().LastOrDefault();
           return View();
        }
        [HttpPost]
        public JsonResult ProductDetail1(CartonManagementModel carton)
        {
            List<CartonManagementDetailModel> model = new List<CartonManagementDetailModel>();
            model = carton.CartonManagementDetails;

            return Json(model, JsonRequestBehavior.AllowGet);
        }
        public ActionResult ProductDetail(string BranchName)

        {
            
            List<CartonManagementDetailModel> list = new List<CartonManagementDetailModel>();
            if (BranchName != null)
            {
                var carton = Services.IBTService.GetCartonByBranch(BranchName);
                if (carton != null)
                {
                    ViewBag.DistributionSummaryID = carton.DistributionSummaryID;
                }


                var cartonList = Services.IBTService.GetProductByBranch(BranchName);
                foreach (var a in cartonList)
                {
                    CartonManagementDetailModel model = new CartonManagementDetailModel();
                    
                        var ProductModel = Services.ProductService.GetById(a.ProductID);
                    model.ProductSKU = a.Product.ProductSKU;
                    model.StyleSKU = a.Product.StyleSKU;
                    model.ColorCode = a.Product.Color.Code;
                    model.CartonManagement = a.CartonManagement;
                    model.CartonManagement.CartonNumber = a.CartonManagement.CartonNumber;
                    model.PackingNo = a.CartonManagement.DistributionSummaryID.ToString();
                    model.Z01 = a.Z01; model.Z02 = a.Z02; model.Z03 = a.Z03; model.Z04 = a.Z04; model.Z05 = a.Z05; model.Z06 = a.Z06; model.Z07 = a.Z07;
                    model.Z08 = a.Z08; model.Z09 = a.Z09; model.Z10 = a.Z10; model.Z11 = a.Z11; model.Z12 = a.Z12; model.Z13 = a.Z13; model.Z14 = a.Z14;
                    model.Z15 = a.Z15; model.Z16 = a.Z16; model.Z17 = a.Z17; model.Z18 = a.Z18; model.Z19 = a.Z19; model.Z20 = a.Z20; model.Z21 = a.Z21;
                    model.Z22 = a.Z22; model.Z23 = a.Z23; model.Z24 = a.Z24; model.Z25 = a.Z25; model.Z26 = a.Z26; model.Z27 = a.Z27; model.Z28 = a.Z28;
                    model.Z29 = a.Z29; model.Z30 = a.Z30;
					model.IBTNumber = carton.IBTNumber;
                    list.Add(model);
                }
				bool delete = Services.CartonManagementService.DeleteNull();
            }
            return View(list);
        }
        public ActionResult ViewDistribution(int? DistributionSummaryID,string BranchName)
        {
            var StockData = Services.StockDistributionService.GetStockData(DistributionSummaryID, BranchName);
            
            return View(StockData);
        }
        [HttpPost]
        public JsonResult GetBranchAddress(string BranchName)
        {
            var BranchAddress = Services.IBTService.GetBranchAddress(BranchName);
            return Json(BranchAddress, JsonRequestBehavior.AllowGet);
        }
        public JsonResult StartPacking(string BranchName)
        {

            CartonManagementModel CM = new CartonManagementModel();
            CM.BranchName = BranchName;
			var list = Services.IBTService.GetIBTNumber();
            CM.IBTNumber=CommonFunction.IBtNo(list);
            var Summary = Services.IBTService.GetLast();
            var SummaryId = Summary.StockDistributionSummaryId;
            //ViewBag.SummaryId = SummaryId;
            CM.DistributionSummaryID = SummaryId;
            //Session["ReceiveOrder"] = RO.ReceiptNumber;
            var createCartonManagement = Services.IBTService.CreateCartonManagement(CM);
            var IBT = Services.IBTService.GetIBTNumber().LastOrDefault();
            var c = Services.CartonManagementService.GetAllDetail(SummaryId);
            if (c.Count!= 0)
            {
                var cd1 = c.Where(x => x.DistributionSummaryID == SummaryId);
                var cd = cd1.LastOrDefault().CartonNumber;
                IBT.CartonNumber = cd + 1;
            }
            else
            {
                IBT.CartonNumber = 1;
            }
            //bool createCartonDetail = Services.IBTService.CreateCartonDetail1(IBT);
            return Json(IBT, JsonRequestBehavior.AllowGet);
           // return RedirectToAction("Index", "IBTCarton");
        }
        [HttpPost]
        public ActionResult CreateA(CartonManagementModel model)
        {
            var create = Services.IBTService.CompleteCreate(model);
            return RedirectToAction("Index", "IBTCarton");
        }
        public JsonResult CheckBarcode(string Barcode)
        {
            var iExist = Services.IBTService.CheckBarcode(Barcode);
            return Json(!iExist, JsonRequestBehavior.AllowGet);
        }
        public bool DeleteCartonWaste()
        {
            bool status = Services.IBTService.DeleteWaste();
            return status;
        }
		//-------------------------- branch to branch transfer --------------------------------------

		public JsonResult StartPackingStockTransfer(string BranchName)
		{
			CartonManagementForStockTransferModel CM = new CartonManagementForStockTransferModel();
			CM.BranchName = BranchName;
			var list = Services.IBTService.GetIBTNumberStockTransfer();
			CM.IBTNumber = Helper.CommonFunction.IBtNoForStock(list);
			var Summary = Services.IBTService.GetLast();
			var SummaryId = Summary.StockDistributionSummaryId;
			//ViewBag.SummaryId = SummaryId;
			CM.DistributionSummaryID = SummaryId;
			//Session["ReceiveOrder"] = RO.ReceiptNumber;
			var createCartonManagement = Services.IBTService.CreateCartonManagementForStock(CM);
			var IBT = Services.IBTService.GetIBTNumberStockTransfer().LastOrDefault();
			var c = Services.CartonManagementService.GetAllDetailForStock(SummaryId);
			if (c.Count != 0)
			{
				var cd1 = c.Where(x => x.DistributionSummaryID == SummaryId);
				var cd = cd1.LastOrDefault().CartonNumber;
				IBT.CartonNumber = cd + 1;
			}
			else
			{
				IBT.CartonNumber = 1;
			}
			//bool createCartonDetail = Services.IBTService.CreateCartonDetail1(IBT);
			return Json(IBT, JsonRequestBehavior.AllowGet);
			// return RedirectToAction("Index", "IBTCarton");
		}


		[HttpPost]
		public JsonResult ProductDetail1ForStock(CartonManagementForStockTransferModel carton)
		{
			List<CartonManagementDetailForStockTransferModel> model = new List<CartonManagementDetailForStockTransferModel>();
			model = carton.CartonMgmtDetailsStockTransfers;
			return Json(model, JsonRequestBehavior.AllowGet);
		}



        public ActionResult ViewDistributionStockTransfer(int? DistributionSummaryID, string BranchName, int? FromBranchID, int? ToBranchID)
        {
            var StockData = Services.StockDistributionService.GetStockData(DistributionSummaryID, BranchName);
            var transfer = Services.StockTransferService.GetScheduledTransfers().Where(c => c.FromBranchId == FromBranchID && c.ToBranchId == ToBranchID).ToList();
            var NewStockData = StockData.Join(transfer, a => a.ProductId, b => b.ProductId, (a, b) => new StockDistributionModel
            {
                Quantity01 = b.Quantity01,
                Quantity02 = b.Quantity02,
                Quantity03 = b.Quantity03,
                Quantity04 = b.Quantity04,
                Quantity05 = b.Quantity05,
                Quantity06 = b.Quantity06,
                Quantity07 = b.Quantity07,
                Quantity08 = b.Quantity08,
                Quantity09 = b.Quantity09,
                Quantity10 = b.Quantity10,
                Quantity11 = b.Quantity11,
                Quantity12 = b.Quantity12,
                Quantity13 = b.Quantity13,
                Quantity14 = b.Quantity14,
                Quantity15 = b.Quantity15,
                Quantity16 = b.Quantity16,
                Quantity17 = b.Quantity17,
                Quantity18 = b.Quantity18,
                Quantity19 = b.Quantity19,
                Quantity20 = b.Quantity20,
                Quantity21 = b.Quantity21,
                Quantity22 = b.Quantity22,
                Quantity23 = b.Quantity23,
                Quantity24 = b.Quantity24,
                Quantity25 = b.Quantity25,
                Quantity26 = b.Quantity26,
                Quantity27 = b.Quantity27,
                Quantity28 = b.Quantity28,
                Quantity29 = b.Quantity29,
                Quantity30 = b.Quantity30,
                StockDistributionSummaryId = DistributionSummaryID,
                Product = a.Product,
                Branch = a.Branch,
                DistributionDate = a.DistributionDate,
                ProductId = a.ProductId,
                Id = a.Id
            }).ToList();

            return View("ViewDistribution", NewStockData);
		}


		public ActionResult ToBranchTransfer()
		{
			var Data = Services.StockTransferService.GetScheduledTransfers();
			var lists = new List<OptionHelper>();
			foreach (var item in Data)
			{
				OptionHelper op = new OptionHelper();
				op.ID = item.Id;
				op.Value = "From (" + item.Branch.Name + ") To (" + item.Branch1.Name + ")";
				if (lists.Count == 0)
					lists.Add(op);
				else
				{
					if (!lists.Any(c => c.Value == op.Value))
						lists.Add(op);
				}
			}
			ViewBag.AllTransfers = new SelectList(lists, "ID", "Value");
			ViewBag.TransfersData = Data;
			return View();
		}

		[HttpPost]
		public ActionResult CreateAForStockTransfer(CartonManagementForStockTransferModel model)
		{
			var create = Services.IBTService.CompleteCreateForStock(model);
			var list = new List<StockTransferDetail>();
			foreach (var item in model.CartonMgmtDetailsStockTransfers)
			{
				StockTransferDetail st = new StockTransferDetail();
				st.ProductId = item.ProductID ?? 0;
				st.FromBranchId = model.FromBranchID;
				st.ToBranchId = model.ToBranchID;
				st.Quantity01 = item.Z01;
				st.Quantity02 = item.Z02;
				st.Quantity03 = item.Z03;
				st.Quantity04 = item.Z04;
				st.Quantity05 = item.Z05;
				st.Quantity06 = item.Z06;
				st.Quantity07 = item.Z07;
				st.Quantity08 = item.Z08;
				st.Quantity09 = item.Z09;
				st.Quantity10 = item.Z10;
				st.Quantity11 = item.Z11;
				st.Quantity12 = item.Z12;
				st.Quantity13 = item.Z13;
				st.Quantity14 = item.Z14;
				st.Quantity15 = item.Z15;
				st.Quantity16 = item.Z16;
				st.Quantity17 = item.Z17;
				st.Quantity18 = item.Z18;
				st.Quantity19 = item.Z19;
				st.Quantity20 = item.Z20;
				st.Quantity21 = item.Z21;
				st.Quantity22 = item.Z22;
				st.Quantity23 = item.Z23;
				st.Quantity24 = item.Z24;
				st.Quantity25 = item.Z25;
				st.Quantity26 = item.Z26;
				st.Quantity27 = item.Z27;
				st.Quantity28 = item.Z28;
				st.Quantity29 = item.Z29;
				st.Quantity30 = item.Z30;
				list.Add(st);
			}
			var status = Services.StockTransferService.UpdateStockTransfer(list);
			return RedirectToAction("Index", "IBTCarton");
		}

	}

	class OptionHelper
	{
		public int ID { get; set; }
		public string Value { get; set; }
	}
}