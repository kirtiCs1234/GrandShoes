﻿using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace POSApi.Controllers.Admin
{
	[RoutePrefix("api/pendingReceipt")]
    public class PendingItemReceiptsController : ApiController
    {
		GrandShoesEntities db = new GrandShoesEntities();
	    public PendingItemReceiptsController()
		{
			db.Configuration.LazyLoadingEnabled = false;
			db.Configuration.ProxyCreationEnabled = false;
		}
		[HttpPost]
		[Route("create")]
		public IHttpActionResult Create(PendingItemReceipt a)
		{
			PendingItemReceipt model = new PendingItemReceipt();
			model.ProductId = a.ProductId;
			model.PurchaseOrderId = a.PurchaseOrderId;
			model.ReceiptId = a.ReceiptId;
			model.TotalCost = a.TotalCost;
			model.TotalItem = a.TotalItem;
			model.Quantity01 = a.Quantity01; model.Quantity02 = a.Quantity02; model.Quantity03 = a.Quantity03; model.Quantity04 = a.Quantity04; model.Quantity05 = a.Quantity05; model.Quantity06 = a.Quantity06; model.Quantity07 = a.Quantity07; model.Quantity08 = a.Quantity08; model.Quantity09 = a.Quantity09; model.Quantity10 = a.Quantity10; model.Quantity11 = a.Quantity11; model.Quantity12 = a.Quantity12; model.Quantity13 = a.Quantity13; model.Quantity14 = a.Quantity14; model.Quantity15 = a.Quantity15; model.Quantity16 = a.Quantity16; model.Quantity17 = a.Quantity17; model.Quantity18 = a.Quantity18; model.Quantity19 = a.Quantity19; model.Quantity20 = a.Quantity20; model.Quantity21 = a.Quantity21; model.Quantity22 = a.Quantity22; model.Quantity23 = a.Quantity23; model.Quantity24 = a.Quantity24; model.Quantity25 = a.Quantity25; model.Quantity26 = a.Quantity26; model.Quantity27 = a.Quantity27; model.Quantity28 = a.Quantity28; model.Quantity29 = a.Quantity29; model.Quantity30 = a.Quantity30;
			  model.Cost01 = a.Cost01; model.Cost02 = a.Cost02; model.Cost03 = a.Cost03; model.Cost04 = a.Cost04; model.Cost05 = a.Cost05; model.Cost06 = a.Cost06; model.Cost07 = a.Cost07; model.Cost08 = a.Cost08; model.Cost09 = a.Cost09; model.Cost10 = a.Cost10; model.Cost11 = a.Cost11; model.Cost12 = a.Cost12; model.Cost13 = a.Cost13; model.Cost14 = a.Cost14; model.Cost15 = a.Cost15; model.Cost16 = a.Cost16; model.Cost17 = a.Cost17; model.Cost18 = a.Cost18; model.Cost19 = a.Cost19; model.Cost20 = a.Cost20; model.Cost21 = a.Cost21; model.Cost22 = a.Cost22; model.Cost21 = a.Cost21; model.Cost24 = a.Cost24; model.Cost25 = a.Cost25; model.Cost26 = a.Cost26; model.Cost27 = a.Cost27; model.Cost28 = a.Cost28; model.Cost29 = a.Cost29; model.Cost30 = a.Cost30;
			model.IsActive = true;
			var receiptOrder = db.ReceiveOrders.Where(x => x.IsActive == true && x.PurchaseOrderId == a.PurchaseOrderId && x.Id==a.ReceiptId).FirstOrDefault();
			receiptOrder.IsFinalize = true;
			db.PendingItemReceipts.Add(model);
			db.SaveChanges();
			return Ok(true);
		}
    }
}
