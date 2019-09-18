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
using Model;
using Helper.ExtensionMethod;

namespace POSApi.Controllers.Admin
{
	[RoutePrefix("api/stockInventory")]
	public class StockInventoriesController : ApiController
	{
		private GrandShoesEntities db = new GrandShoesEntities();
		public StockInventoriesController()
		{
			db.Configuration.LazyLoadingEnabled = false;
			db.Configuration.ProxyCreationEnabled = false;
		}
		[HttpGet]
		[AllowAnonymous]
		[Route("getDetails")]
		public StockDistributionViewModel GetProductList(int? ProductId)
		{
			StockDistributionViewModel StocksProducts = new StockDistributionViewModel();
			var Productlist = db.Products.Where(x => x.Id == ProductId).ToList().Select(m => new ProductModel
			{
				Id = m.Id,
				ProductSKU = m.ProductSKU,
			}).ToList();
			StocksProducts.ProductsList = Productlist;
			return StocksProducts;
		}
		[HttpGet]
		[Route("getProductList")]
		public IHttpActionResult ProductList()
		{
			//var ProductList = db.StockInventories.GroupBy(a => a.ProductId).Select(g => g.FirstOrDefault()).Include(x => x.Product).ToList();
			//var data=ProductList.Where(x=>x.IsActive==true).Distinct().ToList().Select(m=>new  StockInventoryModel
			//    {
			//   ProductSKU=m.Product.ProductSKU,
			//   Id=m.Id,
			//   ProductId=m.ProductId,
			//});
			return Ok();
		}
		[HttpGet]
		[Route("getProducts")]
		public IHttpActionResult Products()
		{
			var list = db.StockInventories.Where(x => x.IsActive == true).Include(x => x.Product).ToList().RemoveReferences();
			var data = list.Select(m => new StockInventoryModel
			{
				ProductSKU = m.Product.ProductSKU + "+" + m.Product.StyleSKU,
				Id = m.Id,
				ProductId = m.ProductID,
			});
			var list1 = data.Distinct();
			return Ok(data);
		}

		[HttpPost]
		[AllowAnonymous]
		[Route("getDetailList")]
		public IHttpActionResult GetSearch(Model.StockEnquiryModel searchdata)
		{

			var list = db.StockInventories.Where(x => x.IsActive == true).Include(x => x.Product).Include(x => x.Product.Color);
			if (searchdata != null)
			{
				if (!string.IsNullOrEmpty(searchdata.ProductSKU))
				{
					list = list.Where(x => x.Product.ProductSKU == searchdata.ProductSKU);
				}
				if (!string.IsNullOrEmpty(searchdata.StyleSKU))
				{
					list = list.Where(x => x.Product.StyleSKU == searchdata.StyleSKU);
				}
				if (!string.IsNullOrEmpty(searchdata.ColorCode))
				{
					list = list.Where(x => x.Product.Color.Code.Contains(searchdata.ColorCode));
				}
			}
			return Ok(list.ToList().RemoveReferences());
		}
		[HttpGet]
		[AllowAnonymous]
		[Route("getProduct")]
		[ResponseType(typeof(StockInventory))]
		public StockDistributionViewModel StockInventoryForProduct(int ProductId, int DisributionSummaryId)
		{
			StockDistributionViewModel stockDistribution = new StockDistributionViewModel();
			var stockList = db.StockInventories.Where(x => x.IsActive == true && x.ProductID == ProductId).FirstOrDefault();
			var stockDistributionList = db.StockDistributions.Where(x => x.IsActive == true).Include(x => x.Branch).ToList();//x.DisributionSummaryId ==DisributionSummaryId
			var stockDistributionList1 = db.StockDistributions.Where(x => x.IsActive == true && x.ProductID == ProductId && x.StockDistributionSummaryId == DisributionSummaryId).Include(x => x.Branch).ToList();
			var BranchList = db.Branches.Where(x => x.IsActive == true).ToList();

			if (stockList != null)
			{

				stockDistribution.ProductInventory = new StockInventoryModel();
				// stockDistribution.ProductInventory.Id = stock.Id;
				stockDistribution.ProductInventory.ProductId = ProductId;
				stockDistribution.ProductInventory.Quantity01 = Convert.ToInt32(stockList.Quantity01);
				stockDistribution.ProductInventory.Quantity02 = Convert.ToInt32(stockList.Quantity02);
				stockDistribution.ProductInventory.Quantity03 = Convert.ToInt32(stockList.Quantity03);
				stockDistribution.ProductInventory.Quantity04 = Convert.ToInt32(stockList.Quantity04);
				stockDistribution.ProductInventory.Quantity05 = Convert.ToInt32(stockList.Quantity05);
				stockDistribution.ProductInventory.Quantity06 = Convert.ToInt32(stockList.Quantity06);
				stockDistribution.ProductInventory.Quantity07 = Convert.ToInt32(stockList.Quantity07);
				stockDistribution.ProductInventory.Quantity08 = Convert.ToInt32(stockList.Quantity08);
				stockDistribution.ProductInventory.Quantity09 = Convert.ToInt32(stockList.Quantity09);
				stockDistribution.ProductInventory.Quantity10 = Convert.ToInt32(stockList.Quantity10);
				stockDistribution.ProductInventory.Quantity11 = Convert.ToInt32(stockList.Quantity11);
				stockDistribution.ProductInventory.Quantity12 = Convert.ToInt32(stockList.Quantity12);
				stockDistribution.ProductInventory.Quantity13 = Convert.ToInt32(stockList.Quantity13);
				stockDistribution.ProductInventory.Quantity14 = Convert.ToInt32(stockList.Quantity14);
				stockDistribution.ProductInventory.Quantity15 = Convert.ToInt32(stockList.Quantity15);
				stockDistribution.ProductInventory.Quantity16 = Convert.ToInt32(stockList.Quantity16);
				stockDistribution.ProductInventory.Quantity17 = Convert.ToInt32(stockList.Quantity17);
				stockDistribution.ProductInventory.Quantity18 = Convert.ToInt32(stockList.Quantity18);
				stockDistribution.ProductInventory.Quantity19 = Convert.ToInt32(stockList.Quantity19);
				stockDistribution.ProductInventory.Quantity20 = Convert.ToInt32(stockList.Quantity20);
				stockDistribution.ProductInventory.Quantity21 = Convert.ToInt32(stockList.Quantity21);
				stockDistribution.ProductInventory.Quantity22 = Convert.ToInt32(stockList.Quantity22);
				stockDistribution.ProductInventory.Quantity23 = Convert.ToInt32(stockList.Quantity23);
				stockDistribution.ProductInventory.Quantity24 = Convert.ToInt32(stockList.Quantity24);
				stockDistribution.ProductInventory.Quantity25 = Convert.ToInt32(stockList.Quantity25);
				stockDistribution.ProductInventory.Quantity26 = Convert.ToInt32(stockList.Quantity26);
				stockDistribution.ProductInventory.Quantity27 = Convert.ToInt32(stockList.Quantity27);
				stockDistribution.ProductInventory.Quantity28 = Convert.ToInt32(stockList.Quantity28);
				stockDistribution.ProductInventory.Quantity29 = Convert.ToInt32(stockList.Quantity29);
				stockDistribution.ProductInventory.Quantity30 = Convert.ToInt32(stockList.Quantity30);
			}
			if (stockDistributionList1.Count > 0)
			{
				int? aQuantity01 = 0;
				int? aQuantity02 = 0;
				int? aQuantity03 = 0;
				int? aQuantity04 = 0;
				int? aQuantity05 = 0;
				int? aQuantity06 = 0;
				int? aQuantity07 = 0;
				int? aQuantity08 = 0;
				int? aQuantity09 = 0;
				int? aQuantity10 = 0;
				int? aQuantity11 = 0;
				int? aQuantity12 = 0;
				int? aQuantity13 = 0;
				int? aQuantity14 = 0;
				int? aQuantity15 = 0;
				int? aQuantity16 = 0;
				int? aQuantity17 = 0;
				int? aQuantity18 = 0;
				int? aQuantity19 = 0;
				int? aQuantity20 = 0;
				int? aQuantity21 = 0;
				int? aQuantity22 = 0;
				int? aQuantity23 = 0;
				int? aQuantity24 = 0;
				int? aQuantity25 = 0;
				int? aQuantity26 = 0;
				int? aQuantity27 = 0;
				int? aQuantity28 = 0;
				int? aQuantity29 = 0;
				int? aQuantity30 = 0;
				foreach (var item in stockDistributionList1)
				{

					// distributionModel.StockDistributionSummaryId = (int)item.StockDistributionSummaryId;
					aQuantity01 += Convert.ToInt32(item.Quantity01);
					aQuantity02 += Convert.ToInt32(item.Quantity02);
					aQuantity03 += Convert.ToInt32(item.Quantity03);
					aQuantity04 += Convert.ToInt32(item.Quantity04);
					aQuantity05 += Convert.ToInt32(item.Quantity05);
					aQuantity06 += Convert.ToInt32(item.Quantity06);
					aQuantity07 += Convert.ToInt32(item.Quantity07);
					aQuantity08 += Convert.ToInt32(item.Quantity08);
					aQuantity09 += Convert.ToInt32(item.Quantity09);
					aQuantity10 += Convert.ToInt32(item.Quantity10);
					aQuantity11 += Convert.ToInt32(item.Quantity11);
					aQuantity12 += Convert.ToInt32(item.Quantity12);
					aQuantity13 += Convert.ToInt32(item.Quantity13);
					aQuantity14 += Convert.ToInt32(item.Quantity14);
					aQuantity15 += Convert.ToInt32(item.Quantity15);
					aQuantity16 += Convert.ToInt32(item.Quantity16);
					aQuantity17 += Convert.ToInt32(item.Quantity17);
					aQuantity18 += Convert.ToInt32(item.Quantity18);
					aQuantity19 += Convert.ToInt32(item.Quantity19);
					aQuantity20 += Convert.ToInt32(item.Quantity20);
					aQuantity21 += Convert.ToInt32(item.Quantity21);
					aQuantity22 += Convert.ToInt32(item.Quantity22);
					aQuantity23 += Convert.ToInt32(item.Quantity23);
					aQuantity24 += Convert.ToInt32(item.Quantity24);
					aQuantity25 += Convert.ToInt32(item.Quantity25);
					aQuantity26 += Convert.ToInt32(item.Quantity26);
					aQuantity27 += Convert.ToInt32(item.Quantity27);
					aQuantity28 += Convert.ToInt32(item.Quantity28);
					aQuantity29 += Convert.ToInt32(item.Quantity29);
					aQuantity30 += Convert.ToInt32(item.Quantity30);

					//stockDistribution.BranchDistribution.Add(distributionModel);
				}
				//stockDistribution.ProductInventory.Quantity01 -= aQuantity01??0;
				//stockDistribution.ProductInventory.Quantity02 -= aQuantity02??0;
				//stockDistribution.ProductInventory.Quantity03 -= aQuantity03??0;
				//stockDistribution.ProductInventory.Quantity04 -= aQuantity04??0;
				//stockDistribution.ProductInventory.Quantity05 -= aQuantity05??0;
				//stockDistribution.ProductInventory.Quantity06 -= aQuantity06??0;
				//stockDistribution.ProductInventory.Quantity07 -= aQuantity07??0;
				//stockDistribution.ProductInventory.Quantity08 -= aQuantity08??0;
				//stockDistribution.ProductInventory.Quantity09 -= aQuantity09??0;
				//stockDistribution.ProductInventory.Quantity10 -= aQuantity10??0;
				//stockDistribution.ProductInventory.Quantity11 -= aQuantity11??0;
				//stockDistribution.ProductInventory.Quantity12 -= aQuantity12??0;
				//stockDistribution.ProductInventory.Quantity13 -= aQuantity13??0;
				//stockDistribution.ProductInventory.Quantity14 -= aQuantity14??0;
				//stockDistribution.ProductInventory.Quantity15 -= aQuantity15??0;
				//stockDistribution.ProductInventory.Quantity16 -= aQuantity16??0;
				//stockDistribution.ProductInventory.Quantity17 -= aQuantity17??0;
				//stockDistribution.ProductInventory.Quantity18 -= aQuantity18??0;
				//stockDistribution.ProductInventory.Quantity19 -= aQuantity19??0;
				//stockDistribution.ProductInventory.Quantity20 -= aQuantity20??0;
				//stockDistribution.ProductInventory.Quantity21 -= aQuantity21??0;
				//stockDistribution.ProductInventory.Quantity22 -= aQuantity22??0;
				//stockDistribution.ProductInventory.Quantity23 -= aQuantity23??0;
				//stockDistribution.ProductInventory.Quantity24 -= aQuantity24??0;
				//stockDistribution.ProductInventory.Quantity25 -= aQuantity25??0;
				//stockDistribution.ProductInventory.Quantity26 -= aQuantity26??0;
				//stockDistribution.ProductInventory.Quantity27 -= aQuantity27??0;
				//stockDistribution.ProductInventory.Quantity28 -= aQuantity28??0;
				//stockDistribution.ProductInventory.Quantity29 -= aQuantity29??0;
				//stockDistribution.ProductInventory.Quantity30 -= aQuantity30??0;

			}
			if (stockDistributionList1 != null)
			{
				foreach (var item in stockDistributionList1)
				{
					StockDistributionModel distributionModel = new StockDistributionModel();
					distributionModel.Name = item.Branch.Name;
					distributionModel.Id = Convert.ToInt32(item.Id);
					distributionModel.BranchId = Convert.ToInt32(item.BranchId);
					distributionModel.ProductId = Convert.ToInt32(item.ProductID);
					distributionModel.StockDistributionSummaryId = (int)item.StockDistributionSummaryId;
					distributionModel.Quantity01 = Convert.ToInt32(item.Quantity01);
					distributionModel.Quantity02 = Convert.ToInt32(item.Quantity02);
					distributionModel.Quantity03 = Convert.ToInt32(item.Quantity03);
					distributionModel.Quantity04 = Convert.ToInt32(item.Quantity04);
					distributionModel.Quantity05 = Convert.ToInt32(item.Quantity05);
					distributionModel.Quantity06 = Convert.ToInt32(item.Quantity06);
					distributionModel.Quantity07 = Convert.ToInt32(item.Quantity07);
					distributionModel.Quantity08 = Convert.ToInt32(item.Quantity08);
					distributionModel.Quantity09 = Convert.ToInt32(item.Quantity09);
					distributionModel.Quantity10 = Convert.ToInt32(item.Quantity10);
					distributionModel.Quantity11 = Convert.ToInt32(item.Quantity11);
					distributionModel.Quantity12 = Convert.ToInt32(item.Quantity12);
					distributionModel.Quantity13 = Convert.ToInt32(item.Quantity13);
					distributionModel.Quantity14 = Convert.ToInt32(item.Quantity14);
					distributionModel.Quantity15 = Convert.ToInt32(item.Quantity15);
					distributionModel.Quantity16 = Convert.ToInt32(item.Quantity16);
					distributionModel.Quantity17 = Convert.ToInt32(item.Quantity17);
					distributionModel.Quantity18 = Convert.ToInt32(item.Quantity18);
					distributionModel.Quantity19 = Convert.ToInt32(item.Quantity19);
					distributionModel.Quantity20 = Convert.ToInt32(item.Quantity20);
					distributionModel.Quantity21 = Convert.ToInt32(item.Quantity21);
					distributionModel.Quantity22 = Convert.ToInt32(item.Quantity22);
					distributionModel.Quantity23 = Convert.ToInt32(item.Quantity23);
					distributionModel.Quantity24 = Convert.ToInt32(item.Quantity24);
					distributionModel.Quantity25 = Convert.ToInt32(item.Quantity25);
					distributionModel.Quantity26 = Convert.ToInt32(item.Quantity26);
					distributionModel.Quantity27 = Convert.ToInt32(item.Quantity27);
					distributionModel.Quantity28 = Convert.ToInt32(item.Quantity28);
					distributionModel.Quantity29 = Convert.ToInt32(item.Quantity29);
					distributionModel.Quantity30 = Convert.ToInt32(item.Quantity30);
					stockDistribution.BranchDistribution.Add(distributionModel);
				}
			}
			foreach (var branch in BranchList)
			{
				if (!stockDistribution.BranchDistribution.Any(x => x.BranchId == branch.Id))
				{
					StockDistributionModel distributionModel = new StockDistributionModel();


					distributionModel.ProductId = ProductId;

					distributionModel.Quantity01 = 0;
					distributionModel.Quantity01 = 0;
					distributionModel.Quantity02 = 0;
					distributionModel.Quantity03 = 0;
					distributionModel.Quantity04 = 0;
					distributionModel.Quantity05 = 0;
					distributionModel.Quantity06 = 0;
					distributionModel.Quantity07 = 0;
					distributionModel.Quantity08 = 0;
					distributionModel.Quantity09 = 0;
					distributionModel.Quantity10 = 0;
					distributionModel.Quantity11 = 0;
					distributionModel.Quantity12 = 0;
					distributionModel.Quantity13 = 0;
					distributionModel.Quantity14 = 0;
					distributionModel.Quantity15 = 0;
					distributionModel.Quantity16 = 0;
					distributionModel.Quantity17 = 0;
					distributionModel.Quantity18 = 0;
					distributionModel.Quantity19 = 0;
					distributionModel.Quantity20 = 0;
					distributionModel.Quantity21 = 0;
					distributionModel.Quantity22 = 0;
					distributionModel.Quantity23 = 0;
					distributionModel.Quantity24 = 0;
					distributionModel.Quantity25 = 0;
					distributionModel.Quantity26 = 0;
					distributionModel.Quantity27 = 0;
					distributionModel.Quantity28 = 0;
					distributionModel.Quantity29 = 0;
					distributionModel.Quantity30 = 0;
					distributionModel.Name = branch.Name;
					distributionModel.BranchId = branch.Id;
					stockDistribution.BranchDistribution.Add(distributionModel);
				}
			}
			stockDistribution.ProductId = ProductId;
			return stockDistribution;
		}
        [HttpGet]
        [Route("getByProduct")]
        public IHttpActionResult GetByProduct(int ProductId)
        {
            var data = db.StockInventories.Where(x => x.IsActive == true && x.ProductID == ProductId).Include(x => x.Product).FirstOrDefault();
            return Ok(data);
        }
		[HttpPost]
		[AllowAnonymous]
		[Route("SaveStockData")]
		[ResponseType(typeof(void))]
		public IHttpActionResult PutStockData(StockDistributionViewModel model)
		{
			var stockDistribution1 = new StockDistribution();
			foreach (var item in model.BranchDistribution)
			{

				if (item.Id != 0)
				{
					var stockDistribution = db.StockDistributions.Where(s => s.Id == item.Id).FirstOrDefault();
					stockDistribution.ProductID = item.ProductId;
					stockDistribution.BranchId = item.BranchId;
					stockDistribution.Quantity01 = Convert.ToInt32(item.Quantity01);
					stockDistribution.Quantity02 = Convert.ToInt32(item.Quantity02);
					stockDistribution.Quantity03 = Convert.ToInt32(item.Quantity03);
					stockDistribution.Quantity04 = Convert.ToInt32(item.Quantity04);
					stockDistribution.Quantity05 = Convert.ToInt32(item.Quantity05);
					stockDistribution.Quantity06 = Convert.ToInt32(item.Quantity06);
					stockDistribution.Quantity07 = Convert.ToInt32(item.Quantity07);
					stockDistribution.Quantity08 = Convert.ToInt32(item.Quantity08);
					stockDistribution.Quantity09 = Convert.ToInt32(item.Quantity09);
					stockDistribution.Quantity10 = Convert.ToInt32(item.Quantity10);
					stockDistribution.Quantity11 = Convert.ToInt32(item.Quantity11);
					stockDistribution.Quantity12 = Convert.ToInt32(item.Quantity12);
					stockDistribution.Quantity13 = Convert.ToInt32(item.Quantity13);
					stockDistribution.Quantity14 = Convert.ToInt32(item.Quantity14);
					stockDistribution.Quantity15 = Convert.ToInt32(item.Quantity15);
					stockDistribution.Quantity16 = Convert.ToInt32(item.Quantity16);
					stockDistribution.Quantity17 = Convert.ToInt32(item.Quantity17);
					stockDistribution.Quantity18 = Convert.ToInt32(item.Quantity18);
					stockDistribution.Quantity19 = Convert.ToInt32(item.Quantity19);
					stockDistribution.Quantity20 = Convert.ToInt32(item.Quantity20);
					stockDistribution.Quantity21 = Convert.ToInt32(item.Quantity21);
					stockDistribution.Quantity22 = Convert.ToInt32(item.Quantity22);
					stockDistribution.Quantity23 = Convert.ToInt32(item.Quantity23);
					stockDistribution.Quantity24 = Convert.ToInt32(item.Quantity24);
					stockDistribution.Quantity25 = Convert.ToInt32(item.Quantity25);
					stockDistribution.Quantity26 = Convert.ToInt32(item.Quantity26);
					stockDistribution.Quantity27 = Convert.ToInt32(item.Quantity27);
					stockDistribution.Quantity28 = Convert.ToInt32(item.Quantity28);
					stockDistribution.Quantity29 = Convert.ToInt32(item.Quantity29);
					stockDistribution.Quantity30 = Convert.ToInt32(item.Quantity30);
					stockDistribution.StockDistributionSummaryId = Convert.ToInt32(item.StockDistributionSummaryId);
					stockDistribution.IsActive = true;

					stockDistribution1.StockDistributionStatusId = 1;

					db.SaveChanges();

				}

				else if (item.Id == 0)
				{
					if (item.Quantity01 != 0 || item.Quantity02 != 0 || item.Quantity03 != 0 || item.Quantity04 != 0 || item.Quantity05 != 0 || item.Quantity06 != 0 || item.Quantity07 != 0 || item.Quantity08 != 0 || item.Quantity09 != 0 || item.Quantity10 != 0 || item.Quantity11 != 0 || item.Quantity12 != 0 || item.Quantity13 != 0 || item.Quantity14 != 0 || item.Quantity15 != 0 || item.Quantity16 != 0 || item.Quantity17 != 0 || item.Quantity18 != 0 || item.Quantity19 != 0 || item.Quantity20 != 0
					 || item.Quantity21 != 0 || item.Quantity22 != 0 || item.Quantity23 != 0 || item.Quantity24 != 0 || item.Quantity25 != 0 || item.Quantity26 != 0 || item.Quantity27 != 0 || item.Quantity29 != 0 || item.Quantity29 != 0 || item.Quantity30 != 0)

					{

						stockDistribution1.ProductID = item.ProductId;
						stockDistribution1.BranchId = item.BranchId;
						// stockDistribution.Name = item.Name;
						stockDistribution1.Quantity01 = Convert.ToInt32(item.Quantity01);
						stockDistribution1.Quantity02 = Convert.ToInt32(item.Quantity02);
						stockDistribution1.Quantity03 = Convert.ToInt32(item.Quantity03);
						stockDistribution1.Quantity04 = Convert.ToInt32(item.Quantity04);
						stockDistribution1.Quantity05 = Convert.ToInt32(item.Quantity05);
						stockDistribution1.Quantity06 = Convert.ToInt32(item.Quantity06);
						stockDistribution1.Quantity07 = Convert.ToInt32(item.Quantity07);
						stockDistribution1.Quantity08 = Convert.ToInt32(item.Quantity08);
						stockDistribution1.Quantity09 = Convert.ToInt32(item.Quantity09);
						stockDistribution1.Quantity10 = Convert.ToInt32(item.Quantity10);
						stockDistribution1.Quantity11 = Convert.ToInt32(item.Quantity11);
						stockDistribution1.Quantity12 = Convert.ToInt32(item.Quantity12);
						stockDistribution1.Quantity13 = Convert.ToInt32(item.Quantity13);
						stockDistribution1.Quantity14 = Convert.ToInt32(item.Quantity14);
						stockDistribution1.Quantity15 = Convert.ToInt32(item.Quantity15);
						stockDistribution1.Quantity16 = Convert.ToInt32(item.Quantity16);
						stockDistribution1.Quantity17 = Convert.ToInt32(item.Quantity17);
						stockDistribution1.Quantity18 = Convert.ToInt32(item.Quantity18);
						stockDistribution1.Quantity19 = Convert.ToInt32(item.Quantity19);
						stockDistribution1.Quantity20 = Convert.ToInt32(item.Quantity20);
						stockDistribution1.Quantity21 = Convert.ToInt32(item.Quantity21);
						stockDistribution1.Quantity22 = Convert.ToInt32(item.Quantity22);
						stockDistribution1.Quantity23 = Convert.ToInt32(item.Quantity23);
						stockDistribution1.Quantity24 = Convert.ToInt32(item.Quantity24);
						stockDistribution1.Quantity25 = Convert.ToInt32(item.Quantity25);
						stockDistribution1.Quantity26 = Convert.ToInt32(item.Quantity26);
						stockDistribution1.Quantity27 = Convert.ToInt32(item.Quantity27);
						stockDistribution1.Quantity28 = Convert.ToInt32(item.Quantity28);
						stockDistribution1.Quantity29 = Convert.ToInt32(item.Quantity29);
						stockDistribution1.Quantity30 = Convert.ToInt32(item.Quantity30);
						stockDistribution1.StockDistributionSummaryId = Convert.ToInt32(item.StockDistributionSummaryId);
						stockDistribution1.IsActive = true;

						stockDistribution1.StockDistributionStatusId = 1;
						DateTime dNow1 = DateTime.Now;
						stockDistribution1.DistributionDate = dNow1;
						db.StockDistributions.Add(stockDistribution1);
						db.SaveChanges();
					}
				}


			}


			// var StockInventory = model.ProductInventory;

			//return Ok(true);
			var stockInventory1 = db.StockInventories.Where(x => x.IsActive == true && x.ProductID == model.ProductInventory.ProductId).FirstOrDefault();
			// stockInventory1.Id = model.ProductInventory.Id;
			// stockInventory1.ProductID = (int)model.ProductInventory.ProductId;
			stockInventory1.Quantity01 = model.ProductInventory.Quantity01;
			stockInventory1.Quantity02 = model.ProductInventory.Quantity02;
			stockInventory1.Quantity03 = model.ProductInventory.Quantity03;
			stockInventory1.Quantity04 = model.ProductInventory.Quantity04;
			stockInventory1.Quantity05 = model.ProductInventory.Quantity05;
			stockInventory1.Quantity06 = model.ProductInventory.Quantity06;
			stockInventory1.Quantity07 = model.ProductInventory.Quantity07;
			stockInventory1.Quantity08 = model.ProductInventory.Quantity08;
			stockInventory1.Quantity09 = model.ProductInventory.Quantity09;
			stockInventory1.Quantity10 = model.ProductInventory.Quantity10;
			stockInventory1.Quantity11 = model.ProductInventory.Quantity11;
			stockInventory1.Quantity12 = model.ProductInventory.Quantity12;
			stockInventory1.Quantity13 = model.ProductInventory.Quantity13;
			stockInventory1.Quantity14 = model.ProductInventory.Quantity14;
			stockInventory1.Quantity15 = model.ProductInventory.Quantity15;
			stockInventory1.Quantity16 = model.ProductInventory.Quantity16;
			stockInventory1.Quantity17 = model.ProductInventory.Quantity17;
			stockInventory1.Quantity18 = model.ProductInventory.Quantity18;
			stockInventory1.Quantity19 = model.ProductInventory.Quantity19;
			stockInventory1.Quantity20 = model.ProductInventory.Quantity20;
			stockInventory1.Quantity21 = model.ProductInventory.Quantity21;
			stockInventory1.Quantity22 = model.ProductInventory.Quantity22;
			stockInventory1.Quantity23 = model.ProductInventory.Quantity23;
			stockInventory1.Quantity24 = model.ProductInventory.Quantity24;
			stockInventory1.Quantity25 = model.ProductInventory.Quantity25;
			stockInventory1.Quantity26 = model.ProductInventory.Quantity26;
			stockInventory1.Quantity27 = model.ProductInventory.Quantity27;
			stockInventory1.Quantity28 = model.ProductInventory.Quantity28;
			stockInventory1.Quantity29 = model.ProductInventory.Quantity29;
			stockInventory1.Quantity30 = model.ProductInventory.Quantity30;

			stockInventory1.IsActive = true;
			db.SaveChanges();
			//db.Entry(stockInventory1).State = EntityState.Modified;
			var stockDistributionSummary = new StockDistributionSummary();
			stockDistributionSummary.Id = model.StockDistributionSummaryModel.Id;
			DateTime dNow = DateTime.Now;
			stockDistributionSummary.DateClose = dNow;
			stockDistributionSummary.IsActive = true;
			db.SaveChanges();
			return Ok(true);
		}
		[HttpPost]
		[Route("create")]
		public IHttpActionResult AddStockInventory(StockInventory stock)
		{
			DAL.StockInventory model = new DAL.StockInventory();
			model.BracketNumber = 89;
			//model.ColumnNumber = 55;
			//model.ProductId = stock.ProductId;
			//model.ProductStyleId = stock.ProductStyleId;
			model.Quantity01 = stock.Quantity01;
			model.Quantity02 = stock.Quantity02;
			model.Quantity03 = stock.Quantity03;
			model.Quantity04 = stock.Quantity04;
			model.Quantity05 = stock.Quantity05;
			model.Quantity06 = stock.Quantity06;
			model.Quantity07 = stock.Quantity07;
			model.Quantity08 = stock.Quantity08;
			model.Quantity09 = stock.Quantity09;
			model.Quantity10 = stock.Quantity10;
			model.Quantity11 = stock.Quantity11;
			model.Quantity12 = stock.Quantity12;
			model.Quantity13 = stock.Quantity13;
			model.Quantity14 = stock.Quantity14;
			model.Quantity15 = stock.Quantity15;
			model.Quantity16 = stock.Quantity16;
			model.Quantity17 = stock.Quantity17;
			model.Quantity18 = stock.Quantity18;
			model.Quantity19 = stock.Quantity19;
			model.Quantity20 = stock.Quantity20;
			model.Quantity21 = stock.Quantity21;
			model.Quantity22 = stock.Quantity22;
			model.Quantity23 = stock.Quantity23;
			model.Quantity24 = stock.Quantity24;
			model.Quantity25 = stock.Quantity25;
			model.Quantity26 = stock.Quantity26;
			model.Quantity27 = stock.Quantity27;
			model.Quantity28 = stock.Quantity28;
			model.Quantity29 = stock.Quantity29;
			model.Quantity30 = stock.Quantity30;
			model.IsActive = true;

			db.StockInventories.Add(stock);
			db.SaveChanges();
			return Ok(true);
		}
		[HttpPost]
		[AllowAnonymous]
		[Route("checkQuantities")]
		public bool checkbranchcode(StockDistributionModel stock)
		{
			if (stock.Id > 0)
			{
				var QuantityCheckStockDistribution = db.StockDistributions.Where(x => x.Id == stock.Id && x.IsActive == true).FirstOrDefault();
				var QuantityCheckStockInventory = db.StockInventories.Where(x => x.IsActive == true).FirstOrDefault();

				if ((QuantityCheckStockDistribution.Quantity01) > (QuantityCheckStockInventory.Quantity01))
				{
					return false;
				}
			}
			var data = db.StockDistributions.Any(x => x.Quantity01 == stock.Quantity01 && x.IsActive == true);
			return data;
		}
	}
}