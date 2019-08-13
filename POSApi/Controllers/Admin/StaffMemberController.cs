using DAL;
using Helper;
using Model;
using Helper.ExtensionMethod;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Http.Description;

namespace POSApi.Controllers
{
    [RoutePrefix("api/staffMember")]
    public class StaffMemberController : ApiController
    {
        private GrandShoesEntities db = new GrandShoesEntities();
        public StaffMemberController()
        {
            db.Configuration.LazyLoadingEnabled = false;
            db.Configuration.ProxyCreationEnabled = false;
        }
        // GET: api/StaffMembers
        [HttpGet]
        [AllowAnonymous]
        [Route("getDetails")]
        public List<StaffMember> GetStaffMembers()
        {
            var list = db.StaffMembers.Include(c=>c.User).OrderByDescending(x => x.Id).Include(c => c.User.Branch).Include(x=>x.User.Role).Include(c => c.StaffStatu).Where(x => x.IsActive == true).ToList().RemoveReferences();
            return list;
        }
        // GET: api/StaffMembers/5
        [HttpGet]
        [AllowAnonymous]
        [Route("getDetail")]
        [ResponseType(typeof(StaffMember))]
        public IHttpActionResult GetStaffMember(int id)
        {
            StaffMember staffMember = db.StaffMembers.Find(id);
            if(staffMember==null)
            {
                return NotFound();
            }
            return Ok(staffMember);
        }
        //LogCheckDetails
        [HttpGet]
        [AllowAnonymous]
        [Route("LogCheckDetails")]
        [ResponseType(typeof(StaffMember))]
        public IHttpActionResult GetStaffLog(int id)
        {
            StaffMember staffMember1 = new StaffMember();
            var staffMemberLog = db.StaffMembers.Where(x => x.Id == id && x.IsActive == true).FirstOrDefault();
            staffMember1.LogId = CommonFunction.LogMethods(staffMember1, "View", (int)staffMemberLog.UserId);
            StaffMember staffMember = db.StaffMembers.Find(id);
            if (staffMember == null)
            {
                return NotFound();
            }
            return Ok(staffMember);
        }
        //Pagging
        [HttpGet]
        [AllowAnonymous]
        [Route("getStaffMemberPaging")]
        public ServiceResult<List<StaffMember>> GetStaffMemberPaging([FromUri]Paging paging)
        {
            ServiceResult<List<StaffMember>> model = new ServiceResult<List<StaffMember>>();
            var source = db.StaffMembers
                .Include(x => x.StaffStatu)
                .Include(x => x.User)
                .Include(x=>x.User.Branch)
                .Include(x=>x.User.Role)
                .Where(x => x.IsActive == true)
                .OrderByDescending(x=>x.Id).ToList().RemoveReferences();          
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
      
        //Searching
        [HttpPost]
        [AllowAnonymous]
        [Route("getSearchData")]
        public ServiceResult<List<StaffMember>> GetSearchData(StaffMemberSearch staffSearch)
        {
            var pageSize = 10;
            var CurrentPage = 1;
            ServiceResult<List<StaffMember>> model = new ServiceResult<List<StaffMember>>();
            var source = db.StaffMembers.Where(x => x.IsActive == true).Include(x=>x.User)
                        .Include(x => x.User.Branch)
                        .Include(x =>x.StaffStatu)
                        .Include(x =>x.User);
            if (staffSearch != null)
            {
                if (staffSearch.IsActive != false)
                {
                    source = db.StaffMembers.Where(x => x.IsActive == true).OrderByDescending(x => x.Id);
                }
                if (!string.IsNullOrEmpty(staffSearch.UserName))
                    source = source.Where(m => m.User.FirstName.ToLower().Equals(staffSearch.UserName.ToLower()));
                if (staffSearch.StaffStatusId > 0)
                    source = source.Where(x => x.StaffStatusId == staffSearch.StaffStatusId);
            }
            int count = source.Count();
            var items = source.OrderByDescending(m => m.Id).Skip(((staffSearch.Page ?? 1) - 1) * pageSize).Take(pageSize).ToList();
            model.data = items.Select(x => new StaffMember
            {
                Id = x.Id,
                UserId = x.UserId,
                User = db.Users.Where(m => m.Id == x.UserId).Include(m=>m.Branch).Include(m=>m.Role).FirstOrDefault(),
                StaffStatusId = x.StaffStatusId,
                StaffStatu = db.StaffStatus.Where(m => m.Id == x.StaffStatusId).FirstOrDefault(),
                IsActive = x.IsActive,
                JoiningDate=x.JoiningDate,
                IsFingurPrintAccess=x.IsFingurPrintAccess,
                ProfilePic=x.ProfilePic
            }).ToList().RemoveReferences();
            model.TotalCount = count;
            return model; ;
        }
        [HttpPost]
        [AllowAnonymous]
        [Route("edit")]
        [ResponseType(typeof(void))]
        public IHttpActionResult PutStaffMember(int id,StaffMember staffMember)
        {
			var model = db.StaffMembers.Where(x => x.IsActive == true && x.Id == id).FirstOrDefault();
            if(!ModelState.IsValid)
            {
                return BadRequest();
            }
            if(id != staffMember.Id)
            {
                return BadRequest();
            }
           // staffMember.LogId = CommonFunction.LogMethods(staffMember, "Update", (int)staffMember.UserId);
			model.IsActive = true;
			model.IsFingurPrintAccess = staffMember.IsFingurPrintAccess;
			model.JoiningDate = staffMember.JoiningDate;
			model.ProfilePic = staffMember.ProfilePic;
			model.StaffStatusId = staffMember.StaffStatusId;
			model.UserId = staffMember.UserId;
            model.UpdatedOn = System.DateTime.UtcNow;
			db.SaveChanges();
            return Ok(true);
        }
        [HttpPost]
        [AllowAnonymous]
        [Route("create")]
        [ResponseType(typeof(StaffMember))]
        public IHttpActionResult AddStaffMember(StaffMember staffMember )
        {
			var model = new StaffMember();
			model.IsActive = true;
			model.IsFingurPrintAccess = staffMember.IsFingurPrintAccess;
			model.JoiningDate = staffMember.JoiningDate;
			model.ProfilePic = staffMember.ProfilePic;
			model.StaffStatusId = staffMember.StaffStatusId;
			model.UserId = staffMember.UserId;
            model.CreatedOn = System.DateTime.UtcNow;
            model.UpdatedOn = System.DateTime.UtcNow;
			db.StaffMembers.Add(model);
			db.SaveChanges();  
            return Ok(true);
        }
        enum ActionLog
        {
            Insert,
            Update,
            Delete,
            View,
            Print,
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("Delete")]
        [ResponseType(typeof(StaffMember))]
        public IHttpActionResult DeleteMember(int id)
        {
            StaffMember staffMember = db.StaffMembers.Find(id);
            if(staffMember == null)
            {
                return NotFound();
            }
           // staffMember.LogId = CommonFunction.LogMethods(staffMember, "Delete", (int)staffMember.UserId);
            staffMember.IsActive = false;
            staffMember.UpdatedOn = System.DateTime.UtcNow;
          //  db.StaffMembers.Remove(staffMember);
            db.SaveChanges();
            return Ok(true);
        }
        [HttpPost]
        [Route("disable")]
        public IHttpActionResult Disable(StaffMember model)
        {
            var staff = db.StaffMembers.Where(x => x.IsActive == true && x.Id == model.Id).FirstOrDefault();
            staff.IsFingurPrintAccess = false;
            staff.UpdatedOn = System.DateTime.UtcNow;
            db.SaveChanges();
            return Ok(true);
        }
        [HttpPost]
        [Route("enable")]
        public IHttpActionResult Enable(StaffMember model)
        {
            var staff = db.StaffMembers.Where(x => x.IsActive == true && x.Id == model.Id).FirstOrDefault();
            staff.IsFingurPrintAccess = true;
            staff.UpdatedOn = System.DateTime.UtcNow;
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
        //FunctionCall StateMemberExists
        private bool StateMemberExists(int id)
        {
            return db.StaffMembers.Count(e => e.Id == id) > 0;
        }
    }
}
