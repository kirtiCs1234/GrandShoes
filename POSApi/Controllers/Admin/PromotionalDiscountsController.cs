using DAL;
using Helper.ExtensionMethod;
using Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace POSApi.Controllers.Admin
{
    [RoutePrefix("api/promotionalDiscount")]
    public class PromotionalDiscountsController : ApiController
    {
        GrandShoesEntities db = new GrandShoesEntities();
        public PromotionalDiscountsController()
        {
            db.Configuration.LazyLoadingEnabled = false;
            db.Configuration.ProxyCreationEnabled = false;
        }
        [HttpPost]
        [Route("create")]
        public IHttpActionResult CreateList(List<PromotionalDiscountModel> list)
        {
            foreach(var item in list)
            {
                PromotionalDiscount model = new PromotionalDiscount();
                model.DiscountBranches = item.BranchList.Select(x => new DiscountBranch
                {
                    BranchID = Convert.ToInt16(x),
                    IsActive = true
                }).ToList();
              
                db.PromotionalDiscounts.Add(model);
                db.SaveChanges();
            }
            return Ok(true);
        }
        [HttpGet]
        [Route("getProductList")]
        public IHttpActionResult GetProductList(int? id)
        {
            var list = db.PromotionalDiscounts.Where(x => x.IsActive == true && x.DiscountSummaryID == id).Include(x=>x.Product).Include(x=>x.DiscountSummary).ToList();
            return Ok(list.RemoveReferences());
        }
        [HttpPost]
        [Route("allow")]
        public IHttpActionResult Allow(PromotionalDiscountModel discount)
        {
            List<DiscountBranch> list = new List<DiscountBranch>();
            if (discount.BranchList != null && discount.ProductList != null)
            {
                foreach (var item in discount.ProductList)
                {

                    foreach (var itemj in discount.BranchList)
                    {
                        DiscountBranch model = new DiscountBranch();
                        model.DiscountID = Convert.ToInt32(item);
                        model.BranchID = Convert.ToInt32(itemj);
                        model.IsActive = true;
                       list.Add(model);
                    }

                }
                try
                {
                    db.DiscountBranches.AddRange(list);
                    db.SaveChanges();

                }
                catch (Exception ex) { }

            }
         
            return Ok(true);
        }
    }
}
