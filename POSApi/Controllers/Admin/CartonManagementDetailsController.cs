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
    [RoutePrefix("api/cartonManagementDetail")]
    public class CartonManagementDetailsController : ApiController
    {
        private GrandShoesEntities db = new GrandShoesEntities();
        public CartonManagementDetailsController()
        {
            db.Configuration.LazyLoadingEnabled = false;
            db.Configuration.ProxyCreationEnabled = false;
        }
        [HttpGet]
        [AllowAnonymous]
        [Route("getDetails")]
        // GET: api/CartonManagementDetails
        public List<CartonManagementDetail> GetCartonManagementDetails()
        {
            var list=db.CartonManagementDetails.Where(x=>x.IsActive==true).OrderByDescending(x => x.Id).OrderByDescending(x=>x.CartonManagementID);
            return list.ToList();
        }
        [HttpGet]
        [AllowAnonymous]
        [Route("getDetail")]
        // GET: api/CartonManagementDetails/5
        [ResponseType(typeof(CartonManagementDetail))]
        public IHttpActionResult GetCartonManagementDetail(int id)
        {
            CartonManagementDetail cartonManagementDetail = db.CartonManagementDetails.Find(id);
            if (cartonManagementDetail == null)
            {
                return NotFound();
            }

            return Ok(cartonManagementDetail);
        }
        [HttpGet]
        [Route("GetAllDetail")]
        public List<CartonManagementDetail> GetAllDetail()
        {
            var list = db.CartonManagementDetails.Where(x => x.IsActive == true).OrderByDescending(x => x.Id).Include(x=>x.CartonManagement).Include(x=>x.CartonManagement.StockDistributionSummary).Include(x=>x.CartonManagement.Branch).ToList();
            return list;
        }
        [HttpGet]
        [AllowAnonymous]
        [Route("getProducts")]
        [ResponseType(typeof(CartonManagementDetail))]
        public List<CartonManagementDetail> GetProducts(int? StockDistributionSummaryId)
        {
            var ProductList = db.CartonManagementDetails.Where(x => x.IsActive == true).Include(x=>x.CartonManagement);

            if (StockDistributionSummaryId > 0)
            {
                ProductList = ProductList.Where(x => x.CartonManagement.DistributionSummaryID == StockDistributionSummaryId);

            }
           
            return ProductList.ToList();

        }
        [HttpGet]
        [Route("getByCartonId")]
        public List<CartonManagementDetail> GetDetails(int? Id)
        {
            var list = db.CartonManagementDetails.Where(x => x.IsActive == true && x.CartonManagementID == Id).Include(x=>x.Product).Include(x=>x.Product.Color).Include(x=>x.Product.Season).ToList();
            return list;
        }
        [HttpPost]
        [Route("GenerateDetailId")]
        public IHttpActionResult GenerateId(CartonManagementModel model)
        {
            CartonManagementDetail detail = new CartonManagementDetail();
            detail.CartonManagementID = model.Id;
            detail.IsActive = true;
            detail.Z01 = detail.Z01 ?? 0;
            detail.Z02 = detail.Z02 ?? 0;
            detail.Z03 = detail.Z03 ?? 0;
            detail.Z04 = detail.Z04 ?? 0;
            detail.Z05 = detail.Z05 ?? 0;
            detail.Z06 = detail.Z06 ?? 0;
            detail.Z07 = detail.Z07 ?? 0;
            detail.Z08 = detail.Z08 ?? 0;
            detail.Z09 = detail.Z09 ?? 0;
            detail.Z10 = detail.Z10 ?? 0;
            detail.Z11 = detail.Z11 ?? 0;
            detail.Z12 = detail.Z12 ?? 0;
            detail.Z13 = detail.Z13 ?? 0;
            detail.Z14 = detail.Z14 ?? 0;
            detail.Z15 = detail.Z15 ?? 0;
            detail.Z16 = detail.Z16 ?? 0;
            detail.Z17 = detail.Z17 ?? 0;
            detail.Z18 = detail.Z18 ?? 0;
            detail.Z19 = detail.Z19 ?? 0;
            detail.Z20 = detail.Z20 ?? 0;
            detail.Z21 = detail.Z21 ?? 0;
            detail.Z22 = detail.Z22 ?? 0;
            detail.Z23 = detail.Z23 ?? 0;
            detail.Z24 = detail.Z24 ?? 0;
            detail.Z25 = detail.Z25 ?? 0;
            detail.Z26 = detail.Z26 ?? 0;
            detail.Z27 = detail.Z27 ?? 0;
            detail.Z28 = detail.Z28 ?? 0;
            detail.Z29 = detail.Z29 ?? 0;
            detail.Z30 = detail.Z30 ?? 0;
            detail.CreatedOn = System.DateTime.UtcNow;
            detail.UpdatedOn = System.DateTime.UtcNow;
            db.CartonManagementDetails.Add(detail);
            db.SaveChanges();
            return Ok(true);
        }
        
        [HttpPost]
        [AllowAnonymous]
        [Route("getSearchData")]
        public ServiceResult<List<CartonManagementDetail>> GetSearchData(Model.CartonManagementDetailModel order)
        {
            ServiceResult<List<CartonManagementDetail>> model = new ServiceResult<List<CartonManagementDetail>>();
            var source = db.CartonManagementDetails
                        .Include(x => x.CartonManagement).AsNoTracking()
                        .Include(x => x.CartonManagement.Branch).AsNoTracking()
                        .Include(x => x.CartonManagement.StockDistributionSummary).AsNoTracking()
                        .Where(x => x.IsActive == true);
            if (order != null && order.BranchId > 0)
            {
                source = source.Where(m => m.CartonManagement.BranchID == order.BranchId
                         && (m.CartonManagement.DistributionSummaryID == order.StockDistributionSummaryId));
            }
            var result = source.ToList();
            int count = result.Count();
            model.TotalCount = count;
            model.data = result;
            return model;
        }

        //Pagging
        [HttpGet]
        [AllowAnonymous]
        [Route("getCartonPaging")]
        public ServiceResult<List<CartonManagementDetail>> GetStaffMemberPaging([FromUri]Paging paging)
        {
            ServiceResult<List<CartonManagementDetail>> model = new ServiceResult<List<CartonManagementDetail>>();
            var source = db.CartonManagementDetails
                        .Include(x => x.CartonManagement).AsNoTracking()
                        .Include(x => x.CartonManagement.Branch).AsNoTracking()
                        .Include(x => x.CartonManagement.StockDistributionSummary).AsNoTracking()
                        .Where(x => x.IsActive == true)
                        .ToList();
            int count = source.Count();
            int CurrentPage = paging.pageNumber;
            int PageSize = paging.pageSize;
            int TotalCount = count;
            int TotalPages = (int)Math.Ceiling(count / (double)PageSize);
            var items = source.Skip((CurrentPage - 1) * PageSize).Take(PageSize).ToList();
            var previousPage = CurrentPage > 1 ? "Yes" : "No";
            var nextPage = CurrentPage < TotalPages ? "Yes" : "No";
            model.TotalCount = count;
            model.data = items;
            return model;
        }
        [HttpPost]
        [AllowAnonymous]
        [Route("create")]
        // POST: api/CartonManagementDetails
        [ResponseType(typeof(CartonManagementDetail))]
        public IHttpActionResult PostCartonManagementDetail(CartonManagementDetail cartonManagementDetail)
        {
            CartonManagementDetail CartonDetail = new CartonManagementDetail();
            CartonDetail.CartonManagementID = cartonManagementDetail.CartonManagementID;
            //CartonDetail.ProductID = cartonManagementDetail.ProductID;
          //  CartonDetail.ProductIds = cartonManagementDetail.ProductIds;
            CartonDetail.Z01 = cartonManagementDetail.Z01;
            CartonDetail.Z02 = cartonManagementDetail.Z02;
            CartonDetail.Z03 = cartonManagementDetail.Z03;
            CartonDetail.Z04 = cartonManagementDetail.Z04;
            CartonDetail.Z05 = cartonManagementDetail.Z05;
            CartonDetail.Z06 = cartonManagementDetail.Z06;
            CartonDetail.Z07 = cartonManagementDetail.Z07;
            CartonDetail.Z08 = cartonManagementDetail.Z08;
            CartonDetail.Z09 = cartonManagementDetail.Z09;
            CartonDetail.Z10 = cartonManagementDetail.Z10;
            CartonDetail.Z11 = cartonManagementDetail.Z11;
            CartonDetail.Z12 = cartonManagementDetail.Z12;
            CartonDetail.Z13 = cartonManagementDetail.Z13;
            CartonDetail.Z14 = cartonManagementDetail.Z14;
            CartonDetail.Z15 = cartonManagementDetail.Z15;
            CartonDetail.Z16 = cartonManagementDetail.Z16;
            CartonDetail.Z17 = cartonManagementDetail.Z17;
            CartonDetail.Z18 = cartonManagementDetail.Z18;
            CartonDetail.Z19 = cartonManagementDetail.Z19;
            CartonDetail.Z20 = cartonManagementDetail.Z20;
            CartonDetail.Z21 = cartonManagementDetail.Z21;
            CartonDetail.Z22 = cartonManagementDetail.Z22;
            CartonDetail.Z23 = cartonManagementDetail.Z23;
            CartonDetail.Z24 = cartonManagementDetail.Z24;
            CartonDetail.Z25 = cartonManagementDetail.Z25;
            CartonDetail.Z26 = cartonManagementDetail.Z26;
            CartonDetail.Z27 = cartonManagementDetail.Z27;
            CartonDetail.Z28 = cartonManagementDetail.Z28;
            CartonDetail.Z29 = cartonManagementDetail.Z29;
            CartonDetail.Z30 = cartonManagementDetail.Z30;
            CartonDetail.IsActive = true;
            CartonDetail.CreatedOn = System.DateTime.UtcNow;
            CartonDetail.UpdatedOn = System.DateTime.UtcNow;
            db.CartonManagementDetails.Add(CartonDetail);
            db.SaveChanges();
            return Ok(true);
        }
        
        [HttpPost]
        [AllowAnonymous]
        [Route("getValue")]
        public List<CartonManagementDetail> GetSearchValue(Model.SearchData searchData)
        {
            var list = db.CartonManagementDetails.Where(x => x.IsActive == true).Include(x => x.CartonManagement.Branch).Include(x=>x.CartonManagement).Include(x => x.CartonManagement.StockDistributionSummary).ToList();
            if (searchData.StockDistributionSummaryId > 0)
            {
                var cartonManagement = list.Select(x => x.CartonManagement).ToList();
                //foreach (var item in cartonManagement)
                //{
                //    if (item != null)
                //    {
                //        list = list.Where(x => x.CartonManagement.DistributionSummaryID == searchData.StockDistributionSummaryId).ToList();
                //    }
                //}
            }
          
            if (searchData.BranchId > 0)
            {
                list = list.Where(x => x.CartonManagement.BranchID == searchData.BranchId).ToList();

            }
            return list;
        }
        
        [HttpPost]
        [AllowAnonymous]
        [Route("delete")]
        // DELETE: api/CartonManagementDetails/5
        [ResponseType(typeof(CartonManagementDetail))]
        public IHttpActionResult DeleteCartonManagementDetail(int id)
        {
            CartonManagementDetail cartonManagementDetail = db.CartonManagementDetails.Find(id);
            if (cartonManagementDetail == null)
            {
                return NotFound();
            }
            db.CartonManagementDetails.Remove(cartonManagementDetail);
            db.SaveChanges();
            return Ok(cartonManagementDetail);
        }
        [HttpPost]
        [AllowAnonymous]
        [Route("edit")]
        [ResponseType(typeof(void))]
        public IHttpActionResult PutReceiptOrderItem(int id, CartonManagementDetail carton)
        {
            //ReceiptOrderItem model = new ReceiptOrderItem();
            var model = db.CartonManagementDetails.Where(x => x.Id == id).FirstOrDefault();
            model.Z01 = carton.Z01;
            model.Z02 = carton.Z02;
            model.Z03 = carton.Z03;
            model.Z04 = carton.Z04;
            model.Z05 = carton.Z05;
            model.Z06 = carton.Z06;
            model.Z07 = carton.Z07;
            model.Z08 = carton.Z08;
            model.Z09 = carton.Z09;
            model.Z10 = carton.Z10;
            model.Z11 = carton.Z11;
            model.Z12 = carton.Z12;
            model.Z13 = carton.Z13;
            model.Z14 = carton.Z14;
            model.Z15 = carton.Z15;
            model.Z16 = carton.Z16;
            model.Z17 = carton.Z17;
            model.Z18 = carton.Z18;
            model.Z19 = carton.Z19;
            model.Z20 = carton.Z20;
            model.Z21 = carton.Z21;
            model.Z22 = carton.Z22;
            model.Z23 = carton.Z23;
            model.Z24 = carton.Z24;
            model.Z25 = carton.Z25;
            model.Z26 = carton.Z26;
            model.Z27 = carton.Z27;
            model.Z28 = carton.Z28;
            model.Z29 = carton.Z29;
            model.Z30 = carton.Z30;
            model.IsActive = true;
            var Total = (model.Z01 ?? 0) + (model.Z02 ?? 0) + (model.Z03 ?? 0) + (model.Z04 ?? 0) + (model.Z05 ?? 0) + (model.Z06?? 0) + (model.Z07 ?? 0)
                + (model.Z08 ?? 0) + (model.Z09 ?? 0) + (model.Z10?? 0) + (model.Z11?? 0) + (model.Z12 ?? 0) + (model.Z13 ?? 0) + (model.Z14 ?? 0)
                + (model.Z15?? 0) + (model.Z16 ?? 0) + (model.Z17 ?? 0) + (model.Z18 ?? 0) + (model.Z19 ?? 0) + (model.Z20 ?? 0) + (model.Z21 ?? 0)
                + (model.Z22 ?? 0) + (model.Z23 ?? 0) + (model.Z23 ?? 0) + (model.Z24 ?? 0) + (model.Z25 ?? 0) + (model.Z26 ?? 0) + (model.Z27 ?? 0)
                + (model.Z28 ?? 0) + (model.Z29 ?? 0) + (model.Z30 ?? 0);
            //model.ProductID = carton.ProductID;
            model.CartonManagementID = model.CartonManagementID;
            model.UpdatedOn = System.DateTime.UtcNow;
            db.SaveChanges();
            return Ok(true);
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
        private bool CartonManagementDetailExists(int id)
        {
            return db.CartonManagementDetails.Count(e => e.Id == id) > 0;
        }
    }
}