using DAL;
using Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Http.Description;

namespace POSApi.Controllers
{
    [RoutePrefix("api/role")]
    //[EnableCors(origins: "*", headers: "*", methods: "*")]
    public class RolesController : ApiController
    {
        private GrandShoesEntities db = new GrandShoesEntities();
        public RolesController()
        {
            db.Configuration.ProxyCreationEnabled = false;
            db.Configuration.LazyLoadingEnabled = false;
        }
        [HttpGet]
        [AllowAnonymous]
        [Route("getDetails")]
        public IHttpActionResult GetRoles()
        {
            var roles = db.Roles.ToList();
            return Ok(roles);
        }
        // GET: api/Role/5
        [HttpGet]
        [AllowAnonymous]
        [Route("getDetail")]
        [ResponseType(typeof(Role))]
        public IHttpActionResult GetRole(int id)
        {
            Role role = db.Roles.Find(id);
            if (role == null)
            {
                return NotFound();
            }
            return Ok(role);
        }
        [HttpGet]
        [AllowAnonymous]
        [Route("getAreaPaging")]
        public ServiceResult<List<Role>> GetArea([FromUri]Paging paging)
        {
            ServiceResult<List<Role>> model = new ServiceResult<List<Role>>();
            var source = db.Roles.Where(x => x.IsActive == true)
                        .OrderByDescending(x => x.Id).ToList();
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
		[Route("getByName")]
		public IHttpActionResult GetByName(string name)
		{
			var data = db.Roles.Where(x => x.IsActive == true && x.RoleName.Contains(name)).FirstOrDefault();
			return Ok(data);
		}
        [HttpPost]
        [AllowAnonymous]
        [Route("getSearchData")]
        public ServiceResult<List<Role>> GetSearchData(RoleModel areaSearch)
        {
            var pageSize = 10;
            ServiceResult<List<Role>> model = new ServiceResult<List<Role>>();
            var source = db.Roles.Where(x => x.IsActive == true);
            if (areaSearch.RoleName != null)
            {
                if (!string.IsNullOrEmpty(areaSearch.RoleName))
                    source = source.Where(m => m.RoleName.Contains(areaSearch.RoleName.ToLower()));
                //  var items = source.OrderBy(m => m.Id).Skip((areaSearch.Page ?? 1 - 1) * pageSize).Take(pageSize).ToList();
            }
            int count = source.Count();
            var items = source.OrderByDescending(m => m.Id).Skip(((areaSearch.Page ?? 1) - 1) * pageSize)
                        .Take(pageSize).ToList();
            model.data = items.Select(x => new Role
            {
                Id = x.Id,
                RoleName = x.RoleName,
                IsActive=x.IsActive,
            }).ToList();
            model.TotalCount = count;
            return model;
        }
        // PUT: api/Role/5
        [HttpPost]
        [AllowAnonymous]
        [Route("edit")]
        [ResponseType(typeof(void))]
        public IHttpActionResult PutRole(int id, Role role)
        {
            var roleById = db.Roles.Where(x => x.IsActive == true && x.Id == id).FirstOrDefault();
            roleById.LogId = role.LogId;
            roleById.RoleName = role.RoleName;
            roleById.IsActive = role.IsActive;
            roleById.UpdatedOn = System.DateTime.UtcNow;
            db.SaveChanges();
            return Ok(true);
        }

        // POST: api/Role
        [HttpPost]
        [AllowAnonymous]
        [Route("create")]
        [ResponseType(typeof(Role))]
        public IHttpActionResult PostRole(Role role)
        {
            Role model = new Role();
            model.RoleName = role.RoleName;
            model.IsActive = role.IsActive;
            model.CreatedOn = System.DateTime.UtcNow;
            model.UpdatedOn = System.DateTime.UtcNow;
            db.Roles.Add(model);
            db.SaveChanges();
            return Ok(true);
        }

        // DELETE: api/Role/5
        [HttpPost]
        [AllowAnonymous]
        [Route("Delete")]
        [ResponseType(typeof(Role))]
        public IHttpActionResult DeleteRole(int id)
        {
            Role role = db.Roles.Find(id);
            if (role == null)
            {
                return NotFound();
            }
            role.UpdatedOn = System.DateTime.UtcNow;
            role.IsActive = false;
            //db.Roles.Remove(role);
            db.SaveChanges();
            return Ok(role);
        }
        [HttpGet]
        [AllowAnonymous]
        [Route("getGrantPermission")]
        public IHttpActionResult getPermission()
        {
            var permission = db.PagePermissions.Where(x => x.IsActive == true).ToList();
            return Ok(permission);
        }
        //setGrantPermission
        [HttpPost]
        [AllowAnonymous]
        [Route("setGrantPermission")]
        public IHttpActionResult setPermission(List<PagePermission> page)
        {
            var IsAdminPage = page[0].IsAdminPage;
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            //var permission = db.CompanyPages.Where(x => x.CompanyId == CompanyId).ToList();
            var permission = db.PagePermissions.Where(x => x.IsActive == true && x.IsAdminPage == IsAdminPage).ToList();
            //db.PagePermissions.Tolist();

            //permission = db.CompanyPages.Where(x)
            foreach (var item in permission)
            {
                item.IsActive = false;
                try
                {
                    db.SaveChanges();
                }
                catch (Exception e) { }
            }
            foreach (var item in page)
            {
                var result = permission.Where(x => x.PageAction == item.PageAction).FirstOrDefault();
                if (result != null)
                {
                    result.IsActive = true;
                    try
                    {
                        db.SaveChanges();
                    }
                    catch (Exception e) { }
                }
                else
                {
                    db.PagePermissions.Add(item);
                    try
                    {
                        db.SaveChanges();
                    }
                    catch (Exception ex)
                    {
                        var a = ex;
                    }
                }
            }
            return Ok(page);
        }
        [HttpGet]
        [AllowAnonymous]
        [Route("getPageName")]
        public IHttpActionResult getAllRolebyBranch(bool IsAdminPage)
        {
            List<PageName> pageNames = db.PageNames.Where(x => x.IsActive == true && x.IsAdminPage == IsAdminPage).ToList();
            if (pageNames == null)
            {
                return NotFound();
            }
            return Ok(pageNames);
        }
        //getPagePermission
        [HttpGet]
        [AllowAnonymous]
        [Route("getPagePermission")]
        public IHttpActionResult GetPagePermission(int RoleId, bool IsAdminPage)
        {
            List<PagePermission> pageNames = db.PagePermissions.Where(x => x.IsActive == true && x.RoleId == RoleId && x.IsAdminPage == IsAdminPage).Include(i => i.PageName).ToList();
            if (pageNames == null)
            {
                return NotFound();
            }
            return Ok(pageNames);
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool RoleExists(int id)
        {
            return db.Roles.Count(e => e.Id == id) > 0;
        }
    }
}
