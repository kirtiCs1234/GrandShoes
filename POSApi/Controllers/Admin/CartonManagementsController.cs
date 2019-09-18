using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using DAL;
using Helper.ExtensionMethod;
using Model;
using Model.ForStockTransfer;

namespace Helper.Controllers.Admin
{
	[RoutePrefix("api/cartonManagement")]
	public class CartonManagementsController : ApiController
	{
		private GrandShoesEntities db = new GrandShoesEntities();
		public CartonManagementsController()
		{
			db.Configuration.LazyLoadingEnabled = false;
			db.Configuration.ProxyCreationEnabled = false;
		}
		[HttpGet]
		[AllowAnonymous]
		[Route("getDetails")]
		// GET: api/CartonManagements
		public List<CartonManagement> GetCartonManagements(int? SummaryId)
		{
			var list = db.CartonManagements.Where(x => x.IsActive == true && x.DistributionSummaryID == SummaryId && x.TotalItems != null).Include(x => x.Branch);
			return list.ToList();
		}

		[HttpGet]
		[AllowAnonymous]
		[Route("GetAllDetailForStock")]
		// GET: api/CartonManagements
		public List<CartonMgmtStockTransfer> GetAllDetailForStock(int? SummaryId)
		{
			var list = db.CartonMgmtStockTransfers.Where(x => x.IsActive == true && x.DistributionSummaryID == SummaryId && x.TotalItems != null).Include(x => x.Branch);
			return list.ToList();
		}
		[HttpGet]
		[Route("getCartonById")]
		public IHttpActionResult GetCarton(int id)
		{
			var carton = db.CartonManagements.Where(x => x.IsActive == true && x.Id == id).Include(x => x.Branch)
			.Include(x => x.CartonManagementDetails)
			.Include(x => x.CartonManagementDetails.Select(m => m.Product))
			.Include(x => x.CartonManagementDetails.Select(m => m.Product.Color))
			.Include(x => x.StockDistributionSummary).FirstOrDefault();
			return Ok(carton);
		}
		[HttpGet]
		[AllowAnonymous]
		[Route("getDetail")]
		// GET: api/CartonManagements
		public List<CartonManagement> GetCartonManagement(int? SummaryId)
		{
			var list = db.CartonManagements.Where(x => x.IsActive == true && x.CartonNumber != null && x.DistributionSummaryID == SummaryId).Include(x => x.Branch);
			return list.ToList();
		}
		[HttpGet]
		[AllowAnonymous]
		[Route("getAreaPaging")]
		public ServiceResult<List<CartonManagement>> GetArea([FromUri]Paging paging)
		{
			ServiceResult<List<CartonManagement>> model = new ServiceResult<List<CartonManagement>>();
			var source = db.CartonManagements.Where(x => x.IsActive == true && x.TotalItems != null)
						.Include(x => x.StockDistributionSummary).Include(x => x.Branch).OrderByDescending(x => x.Id).ToList();
			int count = source.Count();
			// Parameter is passed from Query string if it is null then it default Value will be pageNumber:1  
			int CurrentPage = paging.pageNumber;
			// Parameter is passed from Query string if it is null then it default Value will be pageSize:20  
			int PageSize = paging.pageSize;
			// Display TotalCount to Records to User  
			int TotalCount = count;
			// Calculating Totalpage by Dividing (No of Records / Pagesize)  
			int TotalPages = (int)Math.Ceiling(count / (double)PageSize);
			// Returns List of Customer after applying Paging   
			var items = source.Skip((CurrentPage - 1) * PageSize).Take(PageSize).ToList();
			model.TotalCount = count;
			model.data = items;
			model.pageSize = PageSize;
			return model;
		}
		[HttpGet]
		[Route("GetIBTNumber")]
		public List<CartonManagement> GetIBTNumber()
		{
			var carton = db.CartonManagements.ToList();
			return carton;
		}

		[HttpGet]
		[Route("GetIBTNumberStockTransfer")]
		public List<CartonMgmtStockTransfer> GetIBTNumberStockTransfer()
		{
			var carton = db.CartonMgmtStockTransfers.ToList();
			return carton;
		}
        [HttpGet]
        [Route("getLastSummaryData")]
        public IHttpActionResult GetLastSummaryData()
        {
            var summary = db.StockDistributionSummaries.Where(x => x.IsActive == false).ToList();
            var summ = summary.LastOrDefault();
            var data = db.CartonManagements.Where(x => x.IsActive == true && x.DistributionSummaryID == summ.Id).Include(x=>x.Branch).ToList().RemoveReferences();
            return Ok(data);
        }
		[HttpGet]
		[Route("DeleteWaste")]
		public IHttpActionResult DeleteAll()
		{
			var list = db.CartonManagements.ToList();
			var list1 = list.Select(c => new CartonManagement()
			{
				IsActive = false,
				UpdatedOn = System.DateTime.UtcNow
			}).ToList();

			//list.RemoveAll(x => x.IsActive == null && x.TotalItems == null && x.CartonNumber == null);
			db.SaveChanges();
			return Ok(true);
		}

		[HttpGet]
		[Route("getCartonByBranch")]
		public CartonManagement Data2(string BranchName)
		{
			var data = db.CartonManagements.Where(x => x.IsActive == true && x.Branch.Name == BranchName).Include(x => x.Branch).FirstOrDefault();
			return data;
		}
		[HttpGet]
		[AllowAnonymous]
		[Route("GetByBranch")]
		public List<CartonManagementDetail> Data(string BranchName)
		{
			var list = db.CartonManagements.Where(x => x.IsActive == true && x.Branch.Name == BranchName).Include(x => x.Branch).Include(x => x.CartonManagementDetails.Select(z => z.Product.Color)).Include(x => x.StockDistributionSummary).ToList();
			//list = list.Where(x => x.Branch.Name == BranchName).ToList();

			List<CartonManagementDetail> cartonList = new List<CartonManagementDetail>();
			foreach (var a in list)
			{

				var list1 = a.CartonManagementDetails.Where(m => m.IsActive == true && m.CartonManagementID == a.Id).ToList();
				cartonList.AddRange(list1);
			}

			//var list2 = cartonList.GroupBy(a => a.ProductID,a=>a)
			//      .Select(a => new CartonManagementDetail { Z01 = a.Sum(x => x.Z01), Z02 = a.Sum(x => x.Z02), Z03 = a.Sum(x => x.Z03), Z04 = a.Sum(x => x.Z04),
			//       Z05 = a.Sum(x => x.Z05),
			//         Z06 = a.Sum(x => x.Z06),
			//          Z07 = a.Sum(x => x.Z07),
			//          Z08 = a.Sum(x => x.Z08),
			//          Z09 = a.Sum(x => x.Z09),
			//         Z10 = a.Sum(x => x.Z10),
			//         Z11 = a.Sum(x => x.Z11),
			//         Z12 = a.Sum(x => x.Z12),
			//          Z13 = a.Sum(x => x.Z13),
			//         Z14 = a.Sum(x => x.Z14),
			//          Z15 = a.Sum(x => x.Z15),
			//          Z16 = a.Sum(x => x.Z16),
			//          Z17 = a.Sum(x => x.Z17),
			//          Z18 = a.Sum(x => x.Z18),
			//          Z19 = a.Sum(x => x.Z19),
			//         Z20 = a.Sum(x => x.Z20),
			//         Z21 = a.Sum(x => x.Z21),
			//          Z22 = a.Sum(x => x.Z22),
			//          Z23 = a.Sum(x => x.Z23),
			//          Z24 = a.Sum(x => x.Z24),
			//         Z25 = a.Sum(x => x.Z25),
			//         Z26 = a.Sum(x => x.Z26),
			//         Z27 = a.Sum(x => x.Z27),
			//         Z28 = a.Sum(x => x.Z28),
			//         Z29 = a.Sum(x => x.Z29),
			//         Z30 = a.Sum(x => x.Z30),
			//        IsActive = true,
			//         ProductID = a.Select(x => x.ProductID).FirstOrDefault(),
			//        // Product = db.Products.Where(x => x.IsActive == true && x.Id == a.ProductID).FirstOrDefault(),
			//    }).ToList();
			//      //{
			//      //    ProductID = lk.ProductID,
			//      //    Product = db.Products.Where(x => x.Id == lk.ProductID).Include(x=>x.ProductStyle).FirstOrDefault(),

			//      //    Z01 = cartonList.Sum(x=>x.Z01),
			//      //    Z02 = cartonList.Sum(x => x.Z02),
			//      //    Z03 = cartonList.Sum(x => x.Z03),
			//      //    Z04 = cartonList.Sum(x => x.Z04),
			//      //    Z05 = cartonList.Sum(x => x.Z05),
			//      //    Z06 = cartonList.Sum(x => x.Z06),
			//      //    Z07 = cartonList.Sum(x => x.Z07),
			//      //    Z08 = cartonList.Sum(x => x.Z08),
			//      //    Z09 = cartonList.Sum(x => x.Z09),
			//      //    Z10 = cartonList.Sum(x => x.Z10),
			//      //    Z11 = cartonList.Sum(x => x.Z11),
			//      //    Z12 = cartonList.Sum(x => x.Z12),
			//      //    Z13 = cartonList.Sum(x => x.Z13),
			//      //    Z14 = cartonList.Sum(x => x.Z14),
			//      //    Z15 = cartonList.Sum(x => x.Z15),
			//      //    Z16 = cartonList.Sum(x => x.Z16),
			//      //    Z17 = cartonList.Sum(x => x.Z17),
			//      //    Z18 = cartonList.Sum(x => x.Z18),
			//      //    Z19 = cartonList.Sum(x => x.Z19),
			//      //    Z20 = cartonList.Sum(x => x.Z20),
			//      //    Z21 = cartonList.Sum(x => x.Z21),
			//      //    Z22 = cartonList.Sum(x => x.Z22),
			//      //    Z23 = cartonList.Sum(x => x.Z23),
			//      //    Z24 = cartonList.Sum(x => x.Z24),
			//      //    Z25 = cartonList.Sum(x => x.Z25),
			//      //    Z26 = cartonList.Sum(x => x.Z26),
			//      //    Z27 = cartonList.Sum(x => x.Z27),
			//      //    Z28 = cartonList.Sum(x => x.Z28),
			//      //    Z29 = cartonList.Sum(x => x.Z29),
			//      //    Z30 = cartonList.Sum(x => x.Z30),
			//      //    IsActive = true, 
			//         // CartonManagement = db.CartonManagements.Where(x => x.IsActive == true && x.Id == lk.CartonManagementID).FirstOrDefault().DistributionSummaryID;

			//      //})
			//      //.ToList();
			//list2 = list2.ToList();
			return cartonList;
		}


		[HttpPost]
		[Route("GenerateIBT")]
		public IHttpActionResult GenerateIBT(CartonManagementModel model)
		{
			CartonManagement carton = new CartonManagement();
			carton.IBTNumber = model.IBTNumber;
			// carton.IsActive = true;
			carton.BranchID = db.Branches.Where(x => x.IsActive == true && x.Name == model.BranchName).FirstOrDefault().Id;
			DateTime today = DateTime.Today;
			carton.PackDate = today;
			//carton.CartonNumber = 0;
			carton.DistributionSummaryID = model.DistributionSummaryID;
            carton.CreatedOn = System.DateTime.UtcNow;
            carton.UpdatedOn = System.DateTime.UtcNow;
			db.CartonManagements.Add(carton);
			db.SaveChanges();
			return Ok(true);
		}
        [HttpGet]
        [Route("getByIBTNumber")]
        public IHttpActionResult GetIBTNumber(string id,int BranchID)
        {
            var data = db.CartonManagements.Where(x => x.IBTNumber == id && x.CartonNumber!=null && x.BranchID==BranchID).Include(x=>x.Branch).FirstOrDefault();
            return Ok(data);
        }
		[HttpPost]
		[Route("GenerateIBTForStock")]
		public IHttpActionResult GenerateIBTForStock(CartonManagementForStockTransferModel model)
		{
			CartonMgmtStockTransfer carton = new CartonMgmtStockTransfer();
			carton.IBTNumber = model.IBTNumber;
			// carton.IsActive = true;
			carton.FromBranchID = db.Branches.Where(x => x.IsActive == true && x.Name == model.BranchName).FirstOrDefault().Id;
			DateTime today = DateTime.Today;
			carton.PackDate = today;
			//carton.CartonNumber = 0;
			carton.DistributionSummaryID = model.DistributionSummaryID;
			carton.IsDispatched = false;
			carton.CreatedOn = System.DateTime.UtcNow;
			carton.UpdatedOn = System.DateTime.UtcNow;
			db.CartonMgmtStockTransfers.Add(carton);
			db.SaveChanges();
			return Ok(true);
		}

		[HttpGet]
		[Route("deleteAll")]
		public IHttpActionResult DeleteAll1()
		{
			var list = db.CartonManagements.Where(x => x.IsActive == null && x.CartonNumber == null).ToList();
			foreach (var item in list)
			{
				item.IsActive = false;
				item.UpdatedOn = System.DateTime.UtcNow;
			}
			return Ok(true);
		}
		[HttpPost]
		[AllowAnonymous]
		[Route("getSearchData")]
		public ServiceResult<List<CartonManagement>> GetSearchData(CartonManagementModel search)
		{
			var pageSize = 10;
			ServiceResult<List<CartonManagement>> model = new ServiceResult<List<CartonManagement>>();
			var source = db.CartonManagements.Where(x => x.IsActive == true && x.TotalItems != null).Include(x => x.Branch).Include(x => x.StockDistributionSummary);
			if (search != null)
			{
				if (!string.IsNullOrEmpty(search.BranchName) && !search.BranchName.All(char.IsNumber))
					source = source.Where(m => m.Branch.Name.Contains(search.BranchName.ToLower()));
				//if (search.DistributionSummaryID > 0)
				//	source = source.Where(m => m.DistributionSummaryID == search.DistributionSummaryID);
				//  var items = source.OrderBy(m => m.Id).Skip((areaSearch.Page ?? 1 - 1) * pageSize).Take(pageSize).ToList();
			}
			int count = source.Count();
			var items = source.OrderByDescending(m => m.Id).Skip(((search.Page ?? 1) - 1) * pageSize)
						.Take(pageSize).ToList();
			model.data = items.Select(x => new CartonManagement
			{
				Id = x.Id,
				DistributionSummaryID = x.DistributionSummaryID,
				TotalItems = x.TotalItems,
				PackDate = x.PackDate,
				BranchID = x.BranchID,
				Branch = x.Branch,
                CartonManagementDetails=db.CartonManagementDetails.Where(k=>k.CartonManagementID==x.Id).ToList(),
				StockDistributionSummary = x.StockDistributionSummary,
				CartonNumber = x.CartonNumber,
				IBTNumber = x.IBTNumber,
				IsDispatched = x.IsDispatched,
			}).ToList();
			model.TotalCount = count;
			return model;
		}
		[HttpGet]
		[Route("GetData")]
		public List<CartonManagement> List(string BranchName, int? DistributionSummaryID)
		{
			var list = db.CartonManagements.Where(x => x.IsActive == true && x.DistributionSummaryID == DistributionSummaryID).Include(x => x.Branch).Include(x => x.StockDistributionSummary).ToList();
			list = list.Where(x => x.Branch.Name == BranchName).ToList();
			return list;
		}
		[HttpPost]
		[AllowAnonymous]
		[Route("getSearchDataForReport")]
		public ServiceResult<List<CartonManagement>> GetSearchDataReport(CartonManagementModel search)
		{
			var pageSize = 10;

			ServiceResult<List<CartonManagement>> model = new ServiceResult<List<CartonManagement>>();
			var source = db.CartonManagements.Where(x => x.IsActive == true && x.TotalItems != null).Include(x => x.Branch).Include(x => x.StockDistributionSummary);
			if (search != null)
			{
				if (search.BranchID > 0)
					source = source.Where(m => m.BranchID == search.BranchID);
				if (search.DistributionSummaryID > 0)
					source = source.Where(m => m.DistributionSummaryID == search.DistributionSummaryID);
				//  var items = source.OrderBy(m => m.Id).Skip((areaSearch.Page ?? 1 - 1) * pageSize).Take(pageSize).ToList();
			}
			int count = source.Count();
			var items = source.OrderByDescending(m => m.Id).Skip(((search.Page ?? 1) - 1) * pageSize)
						.Take(pageSize).ToList();
			model.data = items.Select(x => new CartonManagement
			{
				Id = x.Id,
				DistributionSummaryID = x.DistributionSummaryID,
				TotalItems = x.TotalItems,
				PackDate = x.PackDate,
				BranchID = x.BranchID,
				Branch = x.Branch,
				StockDistributionSummary = db.StockDistributionSummaries.Where(m => m.Id == x.DistributionSummaryID).FirstOrDefault(),

			}).ToList();
			model.TotalCount = count;
			return model;
		}
		[HttpGet]
		[Route("getById")]
		// GET: api/CartonManagements/5
		[ResponseType(typeof(CartonManagement))]
		public IHttpActionResult GetCartonManagement(int id)
		{
			CartonManagement cartonManagement = db.CartonManagements.Find(id);
			if (cartonManagement == null)
			{
				return NotFound();
			}

			return Ok(cartonManagement);
		}
		[HttpPost]
		[Route("edit")]
		// PUT: api/CartonManagements/5
		[ResponseType(typeof(void))]
		public IHttpActionResult PutCartonManagement(int id, CartonManagement cartonManagement)
		{
			var data = db.CartonManagements.Where(x => x.IsActive == true && x.Id == id).ToList().FirstOrDefault();
			data.IsDispatched = cartonManagement.IsDispatched;
            data.UpdatedOn = System.DateTime.UtcNow;
			db.SaveChanges();
			return Ok(true);
		}
		[HttpPost]
		[AllowAnonymous]
		[Route("create")]
		// POST: api/CartonManagements
		[ResponseType(typeof(CartonManagement))]
		public IHttpActionResult PostCartonManagement(SearchForCarton carton)
		{
			CartonManagement cartonManagement = new CartonManagement();
			cartonManagement.DistributionSummaryID = carton.StockDistributionSummaryId;
			cartonManagement.BranchID = carton.BranchId;
			cartonManagement.IsActive = true;
			cartonManagement.CreatedOn = System.DateTime.UtcNow;
			cartonManagement.UpdatedOn = System.DateTime.UtcNow;
			db.CartonManagements.Add(cartonManagement);
			db.SaveChanges();
			return Ok(true);
		}
		[HttpPost]
		[Route("completeCreate")]
		public IHttpActionResult Create(CartonManagement carton)
		{
			var model = db.CartonManagements.ToList().LastOrDefault();
			//var result = carton.CartonManagementDetails.Select(a => new { ProductSKU = a.ProductSKU, ProductStyle = a.StyleSKU }).ToList().FirstOrDefault();
			foreach (var a in carton.CartonManagementDetails)
			{
				a.CreatedOn = System.DateTime.UtcNow;
				a.UpdatedOn = System.DateTime.UtcNow;
				model.CartonManagementDetails.Add(a);
			}
			//model.CartonManagementDetails = carton.CartonManagementDetails.Select(x => new CartonManagementDetail
			//{
			//    CartonManagementID = model.Id,
			//    ProductID = db.Products.Where(m => m.ProductSKU == result.ProductSKU && m.ProductStyle.StyleSKU == result.ProductStyle).FirstOrDefault().Id,
			//    Z01=x.Z01,
			//    Z02 = x.Z02,
			//    Z03 = x.Z03,
			//    Z04 = x.Z04,
			//    Z05 = x.Z05,
			//    Z06 = x.Z06,
			//    Z07 = x.Z07,
			//    Z08 = x.Z08,
			//    Z09 = x.Z09,
			//    Z10 = x.Z10,
			//    Z11 = x.Z11,
			//    Z12 = x.Z12,
			//    Z13 = x.Z13,
			//    Z14 = x.Z14,
			//    Z15 = x.Z15,
			//    Z16 = x.Z16,
			//    Z17 = x.Z17,
			//    Z18 = x.Z18,
			//    Z19 = x.Z19,
			//    Z20 = x.Z20,
			//    Z21 = x.Z21,
			//    Z22 = x.Z22,
			//    Z23 = x.Z23,
			//    Z24 = x.Z24,
			//    Z25 = x.Z25,
			//    Z26 = x.Z26,
			//    Z27 = x.Z27,
			//    Z28 = x.Z28,
			//    Z29 = x.Z29,
			//    Z30 = x.Z30,
			//   IsActive=true,

			//}).ToList();
			model.DistributionSummaryID = carton.DistributionSummaryID;
			model.CartonNumber = carton.CartonNumber;
			model.TotalItems = carton.TotalItems;
			model.IsActive = true;
			db.SaveChanges();
			return Ok(true);
		}
        [HttpPost]
        [Route("dispatched")]
        public IHttpActionResult Dispatched(List<int> CartonList)
        {
            var pageName = Request.RequestUri.LocalPath.getRouteName();
          //  var kkk = pageName.Substring(3, 7);
            foreach (var item in CartonList)
            {
                var data = db.CartonManagements.Where(x => x.IsActive == true && x.Id == item).Include(x=>x.CartonManagementDetails).FirstOrDefault();
                data.IsDispatched = true;
                List<StockWarehouseTransaction> list = new List<StockWarehouseTransaction>();
                foreach(var element in data.CartonManagementDetails)
                {
                    StockWarehouseTransaction model = new StockWarehouseTransaction();
                    model.Quantity01 = element.Z01;
                    model.Quantity02 = element.Z02;
                    model.Quantity03 = element.Z03;
                    model.Quantity04 = element.Z04;
                    model.Quantity05 = element.Z05;
                    model.Quantity06 = element.Z06;
                    model.Quantity07 = element.Z07;
                    model.Quantity08 = element.Z08;
                    model.Quantity09 = element.Z09;
                    model.Quantity10 = element.Z10;
                    model.Quantity11 = element.Z11;
                    model.Quantity12 = element.Z12;
                    model.Quantity13 = element.Z13;
                    model.Quantity14 = element.Z14;
                    model.Quantity15 = element.Z15;
                    model.Quantity16 = element.Z16;
                    model.Quantity17 = element.Z17;
                    model.Quantity18 = element.Z18;
                    model.Quantity19 = element.Z19;
                    model.Quantity20 = element.Z20;
                    model.Quantity21 = element.Z21;
                    model.Quantity22 = element.Z22;
                    model.Quantity23 = element.Z23;
                    model.Quantity24 = element.Z24;
                    model.Quantity25 = element.Z25;
                    model.Quantity26 = element.Z26;
                    model.Quantity27 = element.Z27;
                    model.Quantity28 = element.Z28;
                    model.Quantity29 = element.Z29;
                    model.Quantity30 = element.Z30;
                    model.StockTransactionTypeId = 2;
                    model.PrimaryID = element.Id;
                    model.ProductID = element.ProductID;
                    model.TransactionReferenceID = db.TrasactionReferences.Where(x => x.Task == pageName).FirstOrDefault().Id;
                    list.Add(model);
                }
                db.StockWarehouseTransactions.AddRange(list);
                db.SaveChanges();
            }
            return Ok(true);
        }
		[HttpPost]
		[Route("CompleteCreateForStock")]
		public IHttpActionResult CreateForStockTransfer(CartonMgmtStockTransfer model)
		{
			var data = db.CartonMgmtStockTransfers.ToList().LastOrDefault();
			//var result = carton.CartonManagementDetails.Select(a => new { ProductSKU = a.ProductSKU, ProductStyle = a.StyleSKU }).ToList().FirstOrDefault();
			foreach (var a in model.CartonMgmtDetailsStockTransfers)
			{
				a.CreatedOn = System.DateTime.UtcNow;
				a.UpdatedOn = System.DateTime.UtcNow;
				data.CartonMgmtDetailsStockTransfers.Add(a);
			}
			data.DistributionSummaryID = model.DistributionSummaryID;
			data.CartonNumber = model.CartonNumber;
			data.TotalItems = model.TotalItems;
			data.ToBranchID = model.ToBranchID;
			data.IsActive = true;
			data.UpdatedOn = System.DateTime.UtcNow;
			db.SaveChanges();
			return Ok(true);
		}
		[HttpPost]
		[AllowAnonymous]
		[Route("AddCartonOrder")]
		[ResponseType(typeof(CartonManagement))]
		public IHttpActionResult PostCartonOrder(int? CartonManagementId)
		{
			var CartonItemById = db.CartonManagementDetails.Where(x => x.CartonManagementID == CartonManagementId).ToList();
			int? TotalItems = 0;

			foreach (var a in CartonItemById)
			{
				TotalItems += (a.Z01 ?? 0) + (a.Z02 ?? 0) + (a.Z03 ?? 0) + (a.Z04 ?? 0) + (a.Z05 ?? 0) + (a.Z06 ?? 0) + (a.Z07 ?? 0) + (a.Z08 ?? 0) + (a.Z09 ?? 0) + (a.Z10 ?? 0) + (a.Z11 ?? 0) + (a.Z12 ?? 0) + (a.Z13 ?? 0) + (a.Z14 ?? 0) + (a.Z15 ?? 0) + (a.Z16 ?? 0) + (a.Z17 ?? 0) + (a.Z18 ?? 0) + (a.Z19 ?? 0) + (a.Z20 ?? 0) +
					(a.Z21 ?? 0) + (a.Z22 ?? 0) + (a.Z23 ?? 0) + (a.Z24 ?? 0) + (a.Z25 ?? 0) + (a.Z26 ?? 0) + (a.Z27 ?? 0) + (a.Z28 ?? 0) + (a.Z29 ?? 0) + (a.Z30 ?? 0);
			}
			var CartonManagementById = db.CartonManagements.Where(x => x.Id == CartonManagementId).FirstOrDefault();
			CartonManagementById.TotalItems = TotalItems;

			CartonManagementById.IsActive = true;
			CartonManagementById.UpdatedOn = System.DateTime.UtcNow;
			db.SaveChanges();
			return Ok(true);
		}
		[HttpPost]
		[Route("delete")]
		// DELETE: api/CartonManagements/5
		[ResponseType(typeof(CartonManagement))]
		public IHttpActionResult DeleteCartonManagement(int id)
		{
			CartonManagement area = db.CartonManagements.Find(id);
			if (area == null)
			{
				return NotFound();
			}
			area.IsActive = false;
			area.UpdatedOn = System.DateTime.UtcNow;
			db.SaveChanges();
			return Ok(area);
		}
		[HttpPost]
		[AllowAnonymous]
		[Route("EditCartonManagement")]
		[ResponseType(typeof(CartonManagement))]
		public IHttpActionResult PostReceiveOrder1(int? Id)
		{
			var CartonManagementItemById = db.CartonManagementDetails.Where(x => x.CartonManagementID == Id).ToList();
			int? TotalQuantity = 0;

			foreach (var a in CartonManagementItemById)
			{
				TotalQuantity += (a.Z01 ?? 0) + (a.Z02 ?? 0) + (a.Z03 ?? 0) + (a.Z04 ?? 0) + (a.Z05 ?? 0) + (a.Z06 ?? 0) + (a.Z07 ?? 0) + (a.Z08 ?? 0) + (a.Z09 ?? 0) +
					(a.Z10 ?? 0) + (a.Z11 ?? 0) + (a.Z12 ?? 0) + (a.Z13 ?? 0) + (a.Z14 ?? 0) + (a.Z15 ?? 0) + (a.Z16 ?? 0) + (a.Z17 ?? 0) + (a.Z18 ?? 0) + (a.Z19 ?? 0) + (a.Z20 ?? 0) + (a.Z21 ?? 0) +
					(a.Z22 ?? 0) + (a.Z23 ?? 0) + (a.Z24 ?? 0) + (a.Z25 ?? 0) + (a.Z26 ?? 0) + (a.Z27 ?? 0) + (a.Z28 ?? 0) + (a.Z29 ?? 0) + (a.Z30 ?? 0);

			}
			var CartonManagementById = db.CartonManagements.Where(x => x.Id == Id).FirstOrDefault();
			CartonManagementById.TotalItems = TotalQuantity;

			CartonManagementById.IsActive = true;
			CartonManagementById.UpdatedOn = System.DateTime.UtcNow;
			db.SaveChanges();
			return Ok(true);
		}
        [HttpPost]
        [Route("searchData")]
        public IHttpActionResult SerachData(int? summaryID,int? branchId)
        {
            var data = db.CartonManagements.Where(x => x.IsActive == true && x.CartonNumber!=null).Include(x=>x.Branch);
            if (summaryID > 0)
            {
                data = data.Where(x => x.DistributionSummaryID == summaryID);
                    }
            if (branchId > 0)
            {
                data = data.Where(x => x.BranchID == branchId);
            }
            return Ok(data);
        }
		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				db.Dispose();
			}
			base.Dispose(disposing);
		}

		private bool CartonManagementExists(int id)
		{
			return db.CartonManagements.Count(e => e.Id == id) > 0;
		}
	}
}