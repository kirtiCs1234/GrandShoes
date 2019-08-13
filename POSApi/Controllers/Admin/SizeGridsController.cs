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
using System.Data.Entity.Validation;

namespace POSApi.Controllers
{
    [RoutePrefix("api/sizeGrid")]
    public class SizeGridsController : ApiController
    {
        private GrandShoesEntities db = new GrandShoesEntities();
        public SizeGridsController()
        {
            db.Configuration.LazyLoadingEnabled = false;
            db.Configuration.ProxyCreationEnabled = false;
        }
        // GET: api/SizeGrids
        [HttpGet]
        [AllowAnonymous]
        [Route("getDetails")]
        public List<SizeGrid> GetSizeGrids()
        {
            var list= db.SizeGrids.Where(x=>x.IsActive==true).OrderByDescending(x => x.Id).ToList();
            return list;
        }
        [HttpPost]
        [Route("checkGridNo")]
        public bool GetEmail(SizeGrid model)
        {
            if (model.Id > 0)
            {
                var code = db.SizeGrids.Where(x => x.Id == model.Id && x.IsActive == true).FirstOrDefault();
                if (code.GridNumber.Equals(model.GridNumber))
                {
                    return false;
                }

            }
            var data = db.SizeGrids.Any(x => x.GridNumber == model.GridNumber && x.IsActive == true);
            return data;
        }
        [HttpGet]
        [AllowAnonymous]
        [Route("getSizeGridPaging")]
        public ServiceResult<List<SizeGrid>> GetArea([FromUri]Paging paging)

        {
            ServiceResult<List<SizeGrid>> model = new ServiceResult<List<SizeGrid>>();
            var source = db.SizeGrids.Where(x => x.IsActive == true).OrderByDescending(x => x.Id).ToList();
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
        [HttpPost]
        [AllowAnonymous]
        [Route("getSearchData")]
        public ServiceResult<List<SizeGrid>> GetSearchData(SizeGridSearch sizeGridSearch)

        {
            ServiceResult<List<SizeGrid>> model = new ServiceResult<List<SizeGrid>>();
            var source = db.SizeGrids.Where(x => x.IsActive == true);
            var pageSize = 10;
            if (sizeGridSearch != null)
            {

                if (!string.IsNullOrEmpty(sizeGridSearch.GridNumber))

                    source = source.Where(m => m.GridNumber == sizeGridSearch.GridNumber);
            }
            int count = source.Count();
            var items = source.OrderByDescending(x => x.Id).Skip(((sizeGridSearch.Page ?? 1) - 1) * pageSize).Take(pageSize).ToList();
            model.data = items.Select(x => new SizeGrid
            {
                Id = x.Id,
                GridNumber=x.GridNumber,
                Z01=x.Z01,
                Z02=x.Z02,
                Z03=x.Z03,
                Z04=x.Z04,
                Z05=x.Z05,
                Z06=x.Z06,
                Z07=x.Z07,
                Z08=x.Z08,
                Z09=x.Z09,
                Z10=x.Z10,
                Z11=x.Z11,
                Z12=x.Z12,
                Z13=x.Z13,
                Z14=x.Z14,
                Z15=x.Z15,
                Z16=x.Z16,
                Z17=x.Z17,
                Z18=x.Z18,
                Z19=x.Z19,
                Z20=x.Z20,
                Z21=x.Z21,
                Z22=x.Z22,
                Z23=x.Z23,
                Z24=x.Z24,
                Z25=x.Z25,
                Z26=x.Z26,
                Z27=x.Z27,
                Z28=x.Z28,
                Z29=x.Z29,
                Z30=x.Z30,
                IsActive=x.IsActive
            }).ToList();
            model.TotalCount = count;
            return model; ;
        }
        // GET: api/SizeGrids/5
        [HttpGet]
        [AllowAnonymous]
        [Route("getDetail")]
        [ResponseType(typeof(SizeGrid))]
        public IHttpActionResult GetSizeGrid(int id)
        {
            SizeGrid sizeGrid = db.SizeGrids.Find(id);
            if (sizeGrid == null)
            {
                return NotFound();
            }
            return Ok(sizeGrid);
        }
		[HttpPost]
		[Route("createList")]
		public IHttpActionResult CreateList(Dictionary<int, SizeGrid> list)
		{
            Dictionary<string, string> result = new Dictionary<string, string>();
            foreach (var item in list)
			{
                item.Value.CreatedOn = System.DateTime.UtcNow;
                item.Value.UpdatedOn = System.DateTime.UtcNow;
                result.Add(item.Key + "#" + item.Value.GridNumber, "");
                db.SizeGrids.Add(item.Value);
                try
                {
                    result[item.Key + "#" + item.Value.GridNumber] = "Add";
                    db.SaveChanges();
                }
                catch (Exception ex)
                {
                    if (ex.Message != null)
                    {
                        result[item.Key + "#" + item.Value.GridNumber] = ex.Message;
                    }
                }
            }
			return Ok(result);
		}
        [HttpPost]
        [Route("updateList")]
        public IHttpActionResult UpdateList(Dictionary<int, SizeGrid> list)
        {
            Dictionary<string, string> result = new Dictionary<string, string>();
            foreach (var item in list)
            {
                item.Value.UpdatedOn = System.DateTime.UtcNow;
                result.Add(item.Key + "#" + item.Value.GridNumber, "");
                db.SizeGrids.Add(item.Value);
                var obj = db.SizeGrids.Where(x => x.GridNumber == item.Value.GridNumber).FirstOrDefault();
                obj.GridNumber = item.Value.GridNumber;
                obj.IsActive = item.Value.IsActive;
                obj.Z01 = item.Value.Z01;
                obj.Z02 = item.Value.Z02;
                obj.Z03 = item.Value.Z03;
                obj.Z04 = item.Value.Z04;
                obj.Z05 = item.Value.Z05;
                obj.Z06 = item.Value.Z06;
                obj.Z07 = item.Value.Z07;
                obj.Z08 = item.Value.Z08;
                obj.Z09 = item.Value.Z09;
                obj.Z10 = item.Value.Z10;
                obj.Z11 = item.Value.Z11;
                obj.Z12 = item.Value.Z12;
                obj.Z13 = item.Value.Z13;
                obj.Z14 = item.Value.Z14;
                obj.Z15 = item.Value.Z15;
                obj.Z16 = item.Value.Z16;
                obj.Z17 = item.Value.Z17;
                obj.Z18 = item.Value.Z18;
                obj.Z19 = item.Value.Z19;
                obj.Z20 = item.Value.Z20;
                obj.Z21 = item.Value.Z21;
                obj.Z22 = item.Value.Z22;
                obj.Z23 = item.Value.Z23;
                obj.Z24 = item.Value.Z24;
                obj.Z25 = item.Value.Z25;
                obj.Z26 = item.Value.Z26;
                obj.Z27 = item.Value.Z27;
                obj.Z28 = item.Value.Z28;
                obj.Z29 = item.Value.Z29;
                obj.Z30 = item.Value.Z30;
                try
                {
                    result[item.Key + "#" + item.Value.GridNumber] = "Update";
                    db.SaveChanges();
                }
                catch (Exception ex)
                {
                    if (ex.Message != null)
                    {
                        result[item.Key + "#" + item.Value.GridNumber] = ex.Message;
                    }
                }
            }
            return Ok(result);
        }
        [HttpGet]
        [AllowAnonymous]
        [Route("getGridNo")]
        [ResponseType(typeof(SizeGrid))]
        public IQueryable<string> GetGridSizeNo()
        {

            var list = from s in db.SizeGrids.Where(x => x.IsActive == true)
                       select s.GridNumber;
            return list;
        }
        [HttpPost]
        [AllowAnonymous]
        [Route("getGridNoId")]
        public SizeGrid GetCode(string sku)
        {
            SizeGrid model = new SizeGrid();
            bool data = db.SizeGrids.Any(x => x.GridNumber == sku && x.IsActive == true);
            if (data == true)
            {
                model = db.SizeGrids.Where(x => x.IsActive == true && x.GridNumber == sku).FirstOrDefault();
            }
            else
            {
                model.GridNumber = sku;
                model.IsActive = true;
                db.SizeGrids.Add(model);
                db.SaveChanges();
               var list = db.SizeGrids.Where(x => x.IsActive == true).ToList();
                model = list.LastOrDefault();
            }
            return model;
        }
        [HttpGet]
        [AllowAnonymous]
        [Route("getGridSize")]
        [ResponseType(typeof(SizeGrid))]
        public IHttpActionResult GetGridSizeNo(int id)
        {
            var sizeGrid = db.SizeGrids.Where(x => x.IsActive == true && x.Id == id).ToList();
            return Ok(sizeGrid);
        }
        // PUT: api/SizeGrids/5
        [HttpPost]
        [AllowAnonymous]
        [Route("edit")]
        [ResponseType(typeof(void))]
        public IHttpActionResult PutSizeGrid(int id, SizeGrid sizeGrid)
        {
            var data = db.SizeGrids.Where(x => x.IsActive == true && x.Id == id).FirstOrDefault();
            data.Z01 = sizeGrid.Z01;
            data.Z02 = sizeGrid.Z02;
            data.Z03 = sizeGrid.Z03;
            data.Z04 = sizeGrid.Z04;
            data.Z05 = sizeGrid.Z05;
            data.Z06 = sizeGrid.Z06;
            data.Z07 = sizeGrid.Z07;
            data.Z08 = sizeGrid.Z08;
            data.Z09 = sizeGrid.Z09;
            data.Z10 = sizeGrid.Z10;
            data.Z11 = sizeGrid.Z11;
            data.Z12 = sizeGrid.Z12;
            data.Z13 = sizeGrid.Z13;
            data.Z14 = sizeGrid.Z14;
            data.Z15 = sizeGrid.Z15;
            data.Z16 = sizeGrid.Z16;
            data.Z17 = sizeGrid.Z17;
            data.Z18 = sizeGrid.Z18;
            data.Z19 = sizeGrid.Z19;
            data.Z20 = sizeGrid.Z20;
            data.Z21 = sizeGrid.Z21;
            data.Z22 = sizeGrid.Z22;
            data.Z23 = sizeGrid.Z23;
            data.Z24 = sizeGrid.Z24;
            data.Z25 = sizeGrid.Z25;
            data.Z26 = sizeGrid.Z26;
            data.Z27 = sizeGrid.Z27;
            data.Z28 = sizeGrid.Z28;
            data.Z29 = sizeGrid.Z29;
            data.Z30 = sizeGrid.Z30;
            data.GridNumber = sizeGrid.GridNumber;
            data.IsActive = true;
            data.UpdatedOn = System.DateTime.UtcNow;
            db.SaveChanges();
            return Ok(true);
        }
        [HttpPost]
        [AllowAnonymous]
        [Route("create")]
        [ResponseType(typeof(SizeGrid))]
        public IHttpActionResult PostSizeGrid(SizeGrid sizeGrid)
        {
            SizeGrid model = new SizeGrid();
            model.GridNumber = sizeGrid.GridNumber;
            model.Z01 = sizeGrid.Z01??0;
            model.Z02 = sizeGrid.Z02??0;
            model.Z03 = sizeGrid.Z03??0;
            model.Z04 = sizeGrid.Z04??0;
            model.Z05 = sizeGrid.Z05??0;
            model.Z06 = sizeGrid.Z06??0;
            model.Z07 = sizeGrid.Z07??0;
            model.Z08 = sizeGrid.Z08??0;
            model.Z09 = sizeGrid.Z09??0;
            model.Z10 = sizeGrid.Z10??0;
            model.Z11 = sizeGrid.Z11??0;
            model.Z12 = sizeGrid.Z12??0;
            model.Z13 = sizeGrid.Z13??0;
            model.Z14 = sizeGrid.Z14??0;
            model.Z15 = sizeGrid.Z15??0;
            model.Z16 = sizeGrid.Z16??0;
            model.Z17 = sizeGrid.Z17??0;
            model.Z18 = sizeGrid.Z18??0;
            model.Z19 = sizeGrid.Z19??0;
            model.Z20 = sizeGrid.Z20??0;
            model.Z21 = sizeGrid.Z21??0;
            model.Z22 = sizeGrid.Z22??0;
            model.Z23 = sizeGrid.Z23??0;
            model.Z24 = sizeGrid.Z24??0;
            model.Z25 = sizeGrid.Z25??0;
            model.Z26 = sizeGrid.Z26??0;
            model.Z27 = sizeGrid.Z27??0;
            model.Z28 = sizeGrid.Z28??0;
            model.Z29 = sizeGrid.Z29??0;
            model.Z30 = sizeGrid.Z30??0;
            model.IsActive = true;
            model.CreatedOn = System.DateTime.UtcNow;
            model.UpdatedOn = System.DateTime.UtcNow;
            db.SizeGrids.Add(model);
            db.SaveChanges();
            model.IsActive = true;
            return Ok(true);
        }
        [HttpPost]
        [AllowAnonymous]
        [Route("checkGridNo")]
        public bool GetCodeID(string chk)
        {

            var data = db.SizeGrids.Any(x => x.GridNumber == chk && x.IsActive == true);
            return data;
        }
        // DELETE: api/SizeGrids/5
        [HttpPost]
        [AllowAnonymous]
        [Route("Delete")]
        [ResponseType(typeof(SizeGrid))]
        public IHttpActionResult DeleteSizeGrid(int id)
        {
            SizeGrid sizeGrid = db.SizeGrids.Find(id);
            if (sizeGrid == null)
            {
                return NotFound();
            }
            sizeGrid.IsActive =false;
            sizeGrid.UpdatedOn = System.DateTime.UtcNow;
            db.SaveChanges();
            return Ok(sizeGrid);
        }
        [HttpGet]
        [Route("SizeGridAutocomplete")]
        public IHttpActionResult SizeGridAutocomplete(string name)
        {
            var data = db.SizeGrids.Where(x => x.IsActive == true && x.GridNumber.ToString().Contains(name)).ToList().Select(m => new SizeGridModel
            {
                GridNumber = m.GridNumber,
                Id = m.Id,
                Z01 = m.Z01,
                Z02 = m.Z02,
                Z03 = m.Z03,
                Z04 = m.Z04,
                Z05 = m.Z05,
                Z06 = m.Z06,
                Z07 = m.Z07,
                Z08 = m.Z08,
                Z09 = m.Z09,
                Z10 = m.Z10,
                Z11 = m.Z11,
                Z12 = m.Z12,
                Z13 = m.Z13,
                Z14 = m.Z14,
                Z15 = m.Z15,
                Z16 = m.Z16,
                Z17 = m.Z17,
                Z18 = m.Z18,
                Z19 = m.Z19,
                Z20 = m.Z20,
                Z21 = m.Z21,
                Z22 = m.Z22,
                Z23 = m.Z23,
                Z24 = m.Z24,
                Z25 = m.Z25,
                Z26 = m.Z26,
                Z27 = m.Z27,
                Z28 = m.Z28,
                Z29 = m.Z29,
                Z30 = m.Z30,
            }).ToList();
            return Ok(data);
        }
        [HttpGet]
        [Route("SizeGridAutocompleteOffer")]
        public IHttpActionResult SizeGridAutocompleteOffer(string name)
        {
            SizeGridModel model = new SizeGridModel();
            var data = db.SizeGrids.Where(x => x.IsActive == true);
            foreach(var item in data)
            {
                model.Z01 = item.Z01;
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
        private bool SizeGridExists(int id)
        {
            return db.SizeGrids.Count(e => e.Id == id) > 0;
        }
    }
}