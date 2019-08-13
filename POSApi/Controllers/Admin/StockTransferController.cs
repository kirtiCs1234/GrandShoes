using DAL;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Http;


namespace POSApi.Controllers.Admin
{
	[RoutePrefix("api/StockTransfer")]
	public class StockTransferController : ApiController
	{
		private GrandShoesEntities db = new GrandShoesEntities();
		public StockTransferController()
		{
			db.Configuration.LazyLoadingEnabled = false;
			db.Configuration.ProxyCreationEnabled = false;
		}

		// POST: api/Buyers
		[HttpPost]
		[AllowAnonymous]
		[Route("create")]
		public IHttpActionResult insertSuggetion(List<StockTransferDetail> records)
		{
            if (records.Count > 0)
            {
                var newData = new List<StockTransferDetail>();
                //-------------getting old transfer of particular product
                var productId = records.Select(v => v.ProductId).FirstOrDefault();
                var oldData = db.StockTransferDetails.Where(c => c.IsActive == true && c.ProductId == productId && c.IsScheduled == true && c.IsDeleted == false).ToList();
                foreach (var item in oldData)
                {
                    item.IsActive = false;
                    item.IsScheduled = false;
                    item.IsDeleted = true;
                }

                foreach (var item in records)
                {
                    item.IsActive = true;
                    item.IsScheduled = true;
                    item.IsPacked = false;
                    item.IsDispatched = false;
                    item.IsDeleted = false;
                    item.RecordEntryDate = System.DateTime.Now;
                    newData.Add(item);
                }
                db.StockTransferDetails.AddRange(newData);
                db.SaveChanges();
            }
            return Ok(true);
		}
		[HttpGet]
		[Route("GetScheduledTransfers")]
		public IHttpActionResult GetScheduledTransfers()
		{
			var Data = db.StockTransferDetails.Where(c => c.IsActive == true && c.IsDeleted == false && c.IsScheduled == true && c.IsDispatched == false && c.IsPacked == false).Include(c => c.Branch).Include(c => c.Branch1).ToList();
			return Ok(Data);
		}


		[HttpPost]
		[Route("UpdateStockTransfer")]
		public IHttpActionResult UpdateStockTransfer(List<StockTransferDetail> data)
		{
			foreach (var item in data)
			{
				var st = db.StockTransferDetails.Where(c => c.IsActive == true && c.IsDeleted == false && c.IsDispatched == false && c.IsScheduled == true && c.IsPacked == false && c.FromBranchId == item.FromBranchId && c.ToBranchId == item.ToBranchId && c.ProductId == item.ProductId).FirstOrDefault();
				st.Quantity01 -= item.Quantity01;
				st.Quantity02 -= item.Quantity02;
				st.Quantity03 -= item.Quantity03;
				st.Quantity04 -= item.Quantity04;
				st.Quantity05 -= item.Quantity05;
				st.Quantity06 -= item.Quantity06;
				st.Quantity07 -= item.Quantity07;
				st.Quantity08 -= item.Quantity08;
				st.Quantity09 -= item.Quantity09;
				st.Quantity10 -= item.Quantity10;
				st.Quantity11 -= item.Quantity11;
				st.Quantity12 -= item.Quantity12;
				st.Quantity13 -= item.Quantity13;
				st.Quantity14 -= item.Quantity14;
				st.Quantity15 -= item.Quantity15;
				st.Quantity16 -= item.Quantity16;
				st.Quantity17 -= item.Quantity17;
				st.Quantity18 -= item.Quantity18;
				st.Quantity19 -= item.Quantity19;
				st.Quantity20 -= item.Quantity20;
				st.Quantity21 -= item.Quantity21;
				st.Quantity22 -= item.Quantity22;
				st.Quantity23 -= item.Quantity23;
				st.Quantity24 -= item.Quantity24;
				st.Quantity25 -= item.Quantity25;
				st.Quantity26 -= item.Quantity26;
				st.Quantity27 -= item.Quantity27;
				st.Quantity28 -= item.Quantity28;
				st.Quantity29 -= item.Quantity29;
				st.Quantity30 -= item.Quantity30;
				db.SaveChanges();
			}
			return Ok(true);
		}
	}
}