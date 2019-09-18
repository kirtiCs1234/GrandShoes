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

namespace Helper.Controllers.Admin
{
    [RoutePrefix("api/stockDistributions")]
    public class StockDistributionsController : ApiController
    {
        private GrandShoesEntities db = new GrandShoesEntities();
        public StockDistributionsController()
        {
            db.Configuration.LazyLoadingEnabled = false;
            db.Configuration.ProxyCreationEnabled = false;
        }
        // GET: api/StockDistributions
        [HttpGet]
        [AllowAnonymous]
        [Route("getDetails")]
        public List<StockDistribution> GetStockDistributions()
        {
            var list= db.StockDistributions.Where(x=>x.IsActive==true).Include(x => x.Branch).ToList().RemoveReferences();
            
            return list;
        }
        //[HttpPost]
        //[Route("BranchAutocomplete")]
        //public IHttpActionResult BranchAutocomplete(string name)
        //{
        //    var list = db.StockDistributions.Where(x => x.IsActive == true).ToList();
        //    var data = list.Where(x => x.Branch.Name.StartsWith(name)).ToList().Select(m => new StockDistributionModel
        //    {
        //        BranchId = m.BranchId,
        //        Branch = db.Branches.Where(x => x.IsActive == true && x.Id == m.BranchId).FirstOrDefault(),
        //        Id = m.Id,
        //        BranchName=m.Branch.Name
        //    }).ToList();
        //    return Ok(data);
        //}
        [HttpGet]
        [Route("GetStockData")]
        public List<StockDistribution> GetData(int? DistributionSummaryID,string BranchName)
        {
          //  List<StockDistribution> updateList = new List<StockDistribution>();
            var list = db.StockDistributions.Where(x => x.IsActive == true && x.StockDistributionSummaryId == DistributionSummaryID).Include(x => x.Branch).Include(x => x.StockDistributionSummary);
            list = list.Where(x => x.Branch.Name == BranchName).Include(x=>x.Product);
            var stockList = list.ToList();
            var cartonList = db.CartonManagementDetails.Where(x => x.IsActive == true && x.CartonManagement.DistributionSummaryID == DistributionSummaryID).Include(x => x.CartonManagement).ToList();
            foreach(var item in stockList)
            {
                foreach(var item2 in cartonList)
                {
                    if (item2.ProductID == item.ProductID && item2.CartonManagement.BranchID==item.BranchId)
                    {
                        item.Quantity01 = item.Quantity01 - item2.Z01;
                        item.Quantity02 = item.Quantity02 - item2.Z02;
                        item.Quantity03 = item.Quantity03 - item2.Z03;
                        item.Quantity04 = item.Quantity04 - item2.Z04;
                        item.Quantity05 = item.Quantity05 - item2.Z05;
                        item.Quantity06 = item.Quantity06 - item2.Z06;
                        item.Quantity07 = item.Quantity07 - item2.Z07;
                        item.Quantity08 = item.Quantity08 - item2.Z08;
                        item.Quantity09 = item.Quantity09 - item2.Z09;
                        item.Quantity10 = item.Quantity10 - item2.Z10;
                        item.Quantity11 = item.Quantity11 - item2.Z11;
                        item.Quantity12 = item.Quantity12 - item2.Z12;
                        item.Quantity13 = item.Quantity13 - item2.Z13;
                        item.Quantity14 = item.Quantity14 - item2.Z14;
                        item.Quantity15 = item.Quantity15 - item2.Z15;
                        item.Quantity16 = item.Quantity16 - item2.Z16;
                        item.Quantity17 = item.Quantity17 - item2.Z17;
                        item.Quantity18 = item.Quantity18 - item2.Z18;
                        item.Quantity19 = item.Quantity19 - item2.Z19;
                        item.Quantity20 = item.Quantity20 - item2.Z20;
                        item.Quantity21 = item.Quantity21 - item2.Z21;
                        item.Quantity22 = item.Quantity22 - item2.Z22;
                        item.Quantity23 = item.Quantity23 - item2.Z23;
                        item.Quantity24 = item.Quantity24 - item2.Z24;
                        item.Quantity25 = item.Quantity25 - item2.Z25;
                        item.Quantity26= item.Quantity26- item2.Z26;
                        item.Quantity27 = item.Quantity27 - item2.Z27;
                        item.Quantity28 = item.Quantity28 - item2.Z28;
                        item.Quantity29 = item.Quantity29 - item2.Z29;
                        item.Quantity30 = item.Quantity30 - item2.Z30;
                    }
                }
            }
            return stockList.RemoveReferences();
        }
        [HttpGet]
        [AllowAnonymous]
        [Route("getSummary")]
        public IHttpActionResult GetSummary(int id)
        {
            var Summary = db.StockDistributions.Where(x=>x.IsActive==true).ToList().Select(x => new StockDistribution {
                Id = x.Id,
                BranchId = x.BranchId,
                Branch = db.Branches.Where(m => m.IsActive == true && m.Id == x.BranchId).FirstOrDefault(),
                //ProductId = x.ProductId,
               // Product = db.Products.Where(m => m.IsActive == true && m.Id == x.ProductId).FirstOrDefault(),
                StockDistributionStatusId = x.StockDistributionStatusId,
                StockDistributionStatu = db.StockDistributionStatus.Where(m => m.IsActive == true && m.Id == x.StockDistributionStatusId).FirstOrDefault(),
                
                Quantity01 = x.Quantity01,
                Quantity02 = x.Quantity02,
                Quantity03 = x.Quantity03,
                Quantity04 = x.Quantity04,
                Quantity05 = x.Quantity05,
                Quantity06 = x.Quantity06,
                Quantity07 = x.Quantity07,
                Quantity08 = x.Quantity08,
                Quantity09 = x.Quantity09,
                Quantity10 = x.Quantity10,
                Quantity11 = x.Quantity11,
                Quantity12 = x.Quantity12,
                Quantity13 = x.Quantity13,
                Quantity14 = x.Quantity14,
                Quantity15 = x.Quantity15,
                Quantity16 = x.Quantity16,
                Quantity17 = x.Quantity17,
                Quantity18 = x.Quantity18,
                Quantity19 = x.Quantity19,
                Quantity20 = x.Quantity20,
                Quantity21 = x.Quantity21,
                Quantity22 = x.Quantity22,
                Quantity23 = x.Quantity23,
                Quantity24 = x.Quantity24,
                Quantity25 = x.Quantity25,
                Quantity26 = x.Quantity26,
                Quantity27 = x.Quantity27,
                Quantity28 = x.Quantity28,
                Quantity29 = x.Quantity29,
                Quantity30 = x.Quantity30,
                IsActive=true,
                StockDistributionSummaryId = x.StockDistributionSummaryId,
                StockDistributionSummary = db.StockDistributionSummaries.Where(m => m.IsActive == true && m.Id == x.StockDistributionSummaryId).FirstOrDefault(),
            }).ToList().RemoveReferences();
            return Ok(Summary);
        }
        [HttpGet]
        [AllowAnonymous]
        [Route("getByBranchId")]
        [ResponseType(typeof(StockDistribution))]
        public IHttpActionResult GetStockDistributions(int? BranchId)
        {
            StockDistribution stockDistribution = db.StockDistributions.Where(x=>x.BranchId == BranchId).Include(x=>x.Branch).FirstOrDefault().RemoveReferences();
            if (stockDistribution == null)
            {
                return NotFound();
            }

            return Ok(stockDistribution);
        }
        //[HttpGet]
        //[Route("getBySummaryId")]
        //public IHttpActionResult GetLast(int id)
        //{
        //    var list = db.StockDistributions.Where(x => x.IsActive == true && x.StockDistributionSummaryId==id).FirstOrDefault();
        //    return Ok(list);
        //}
        [HttpGet]
        [Route("getLast")]
        public IHttpActionResult GetLast()
        {
            var list = db.StockDistributions.Where(x => x.IsActive == true).LastOrDefault();
            return Ok(list);
        }
        [HttpGet]
        [Route("getByProductId")]
        public IHttpActionResult GetByProduct(int? ProductId)
        {
            var data = db.StockDistributions.Where(x => x.IsActive == true && x.ProductID == ProductId).Include(x => x.Product).Include(x => x.Branch).Include(x => x.StockDistributionStatu).Include(x => x.StockDistributionSummary).ToList();
            return Ok(data);
        }
        [HttpPost]
        [Route("editData")]
        public IHttpActionResult EditData(int Id,StockDistributionModel model)
        {
            var stock = db.StockDistributions.Where(x => x.IsActive == true && x.Id == Id).Include(x=>x.Branch).FirstOrDefault();
            //var ProductList = db.Products.Where(x => x.IsActive == true && x.ProductSKU == model.ProductSKU ).Include(x=>x.ProductStyle);
            //stock.ProductId = ProductList.Where(x => x.ProductStyle.StyleSKU == model.StyleSKU).FirstOrDefault().Id;
            //stock.BranchId = db.Branches.Where(x => x.IsActive == true && x.Name == model.BranchName).FirstOrDefault().Id;
            //stock.InvoiceId = stock.InvoiceId;
            //stock.ProductId = stock.ProductId;
            stock.BranchId = stock.BranchId;
            stock.DistributionDate = stock.DistributionDate;
            stock.IsActive = true;
            stock.LogId = stock.LogId;
            stock.Quantity01 = model.Quantity01;
            stock.Quantity02 = model.Quantity02;
            stock.Quantity03 = model.Quantity03;
            stock.Quantity04 = model.Quantity04;
            stock.Quantity05 = model.Quantity05;
            stock.Quantity06 = model.Quantity06;
            stock.Quantity07 = model.Quantity07;
            stock.Quantity08 = model.Quantity08;
            stock.Quantity09 = model.Quantity09;
            stock.Quantity10 = model.Quantity10;
            stock.Quantity11 = model.Quantity11;
            stock.Quantity12 = model.Quantity12;
            stock.Quantity13 = model.Quantity13;
            stock.Quantity14 = model.Quantity14;
            stock.Quantity15 = model.Quantity15;
            stock.Quantity16 = model.Quantity16;
            stock.Quantity17 = model.Quantity17;
            stock.Quantity18 = model.Quantity18;
            stock.Quantity19 = model.Quantity19;
            stock.Quantity20 = model.Quantity20;
            stock.Quantity21 = model.Quantity21;
            stock.Quantity22 = model.Quantity22;
            stock.Quantity23 = model.Quantity23;
            stock.Quantity24 = model.Quantity24;
            stock.Quantity25 = model.Quantity25;
            stock.Quantity26 = model.Quantity26;
            stock.Quantity27 = model.Quantity27;
            stock.Quantity28 = model.Quantity28;
            stock.Quantity29 = model.Quantity29;
            stock.Quantity30 = model.Quantity30;
            db.SaveChanges();
            return Ok(true);
           
        }
        [HttpGet]
        [Route("getBySummaryId")]
        public IHttpActionResult GetBySummaryId(int id)
        {
            var list = db.StockDistributions.Where(x => x.IsActive == true && x.StockDistributionSummaryId == id).Include(x=>x.Product).Include(x=>x.StockDistributionSummary).Include(x=>x.Product.Color).Include(x=>x.Product.Supplier).Include(x=>x.Branch).Include(x=>x.StockDistributionStatu).ToList();
            return Ok(list.RemoveReferences());
        }
        [HttpGet]
        [AllowAnonymous]
        [Route("getProducts")]
        [ResponseType(typeof(StockDistribution))]
        public List<StockDistribution> GetProducts(int? StockDistributionSummaryId, int? BranchId)
        {
            var ProductList = db.StockDistributions.Where(x => x.IsActive==true);

            if (StockDistributionSummaryId>0)
            {
                ProductList = ProductList.Where(x => x.StockDistributionSummaryId == StockDistributionSummaryId).Include(x => x.StockDistributionSummary);

            }
            if (BranchId > 0)
            {
                 ProductList = ProductList.Where(x =>  x.BranchId == BranchId).Include(x => x.Branch);

            }
            if(StockDistributionSummaryId > 0|| BranchId > 0)
            {
               ProductList = ProductList.Where(x => x.StockDistributionSummaryId == StockDistributionSummaryId || x.BranchId == BranchId).Include(x => x.Branch).Include(x => x.StockDistributionSummary);

            }
            return ProductList.ToList().RemoveReferences();

        }
        [HttpGet]
        [AllowAnonymous]
        [Route("getById")]
        [ResponseType(typeof(StockDistribution))]
        public IHttpActionResult GetStockDistribution(int? id)
        {
            StockDistribution stockDistribution = db.StockDistributions.Where(x => x.Id == id).Include(x => x.Branch).FirstOrDefault().RemoveReferences();
            if (stockDistribution == null)
            {
                return NotFound();
            }

            return Ok(stockDistribution);
        }
        [HttpPost]
        [AllowAnonymous]
        [Route("getSelectedData")]
        public IHttpActionResult GetSearchData(Model.StockEnquiryModel searchData)
        {
            var list = db.StockDistributions.Where(x => x.IsActive == true).Include(x => x.Branch).Include(x=>x.Product).Include(x=>x.Product.Color);
			if (searchData != null)
			{
				if (!string.IsNullOrEmpty(searchData.BranchName))
				{
					list = list.Where(x => x.Branch.Name.Contains(searchData.BranchName));
				}
				if (!string.IsNullOrEmpty(searchData.ProductSKU))
				{
					list = list.Where(x => x.Product.ProductSKU == searchData.ProductSKU);
				}
				if (!string.IsNullOrEmpty(searchData.StyleSKU))
				{
					list = list.Where(x => x.Product.StyleSKU == searchData.StyleSKU);
				}
				if (!string.IsNullOrEmpty(searchData.ColorCode))
				{
					list = list.Where(x => x.Product.Color.Code.Contains(searchData.ColorCode));
				}
			}
           
			return Ok(list.ToList().RemoveReferences());
        }
        [HttpPost]
        [AllowAnonymous]
        [Route("getValue")]
        public List<StockDistribution> GetSearchValue(Model.SearchData searchData)
        {
            var list = db.StockDistributions.Where(x => x.IsActive == true).Include(x=>x.Branch);
            if (searchData.StockDistributionSummaryId > 0)
            {
                list = list.Where(x => x.StockDistributionSummaryId == searchData.StockDistributionSummaryId);

            }
            if (searchData.BranchId > 0)
            {
                list = list.Where(x => x.BranchId == searchData.BranchId);
                
            }
			var list1 = list.ToList().RemoveReferences();
			return list1;
        }
        
        // GET: api/StockDistributions/5
        [ResponseType(typeof(StockDistribution))]
        public IHttpActionResult GetStockDistribution(int id)
        {
            StockDistribution stockDistribution = db.StockDistributions.Find(id);
            if (stockDistribution == null)
            {
                return NotFound();
            }

            return Ok(stockDistribution);
        }
        [HttpGet]
        [Route("getLastSummaryData")]
        public IHttpActionResult GetLastSummaryData()
        {
            var lastSummary = db.StockDistributionSummaries.Where(x=>x.IsActive==false).ToList().LastOrDefault().Id;
            var data = db.StockDistributions.Where(x => x.IsActive == true && x.StockDistributionSummaryId == lastSummary)
                .Include(x=>x.Product).Include(x=>x.Product.Color).Include(x=>x.Product.Supplier).Include(x=>x.Branch).Include(x=>x.StockDistributionStatu).Include(x=>x.StockDistributionSummary).ToList();
            return Ok(data.RemoveReferences());
        }
        // PUT: api/StockDistributions/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutStockDistribution(int id, StockDistribution stockDistribution)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != stockDistribution.Id)
            {
                return BadRequest();
            }

            db.Entry(stockDistribution).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StockDistributionExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/StockDistributions
        [ResponseType(typeof(StockDistribution))]
        public IHttpActionResult PostStockDistribution(StockDistribution stockDistribution)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.StockDistributions.Add(stockDistribution);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = stockDistribution.Id }, stockDistribution);
        }

        // DELETE: api/StockDistributions/5
        [ResponseType(typeof(StockDistribution))]
        public IHttpActionResult DeleteStockDistribution(int id)
        {
            StockDistribution stockDistribution = db.StockDistributions.Find(id);
            if (stockDistribution == null)
            {
                return NotFound();
            }

            db.StockDistributions.Remove(stockDistribution);
            db.SaveChanges();

            return Ok(stockDistribution);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool StockDistributionExists(int id)
        {
            return db.StockDistributions.Count(e => e.Id == id) > 0;
        }
    }
}