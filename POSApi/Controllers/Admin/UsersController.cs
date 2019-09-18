﻿using DAL;
using Helper;
using Model;
using Newtonsoft.Json;
using Helper.ExtensionMethod;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using static Helper.CommonFunction;

namespace POSApi.Controllers
{
    [RoutePrefix("api/user")]
    public class UsersController : ApiController
    {
        private GrandShoesEntities db = new GrandShoesEntities();
        public UsersController()
        {
            db.Configuration.ProxyCreationEnabled = false;
            db.Configuration.LazyLoadingEnabled = false;
        }
        [HttpGet]
        [AllowAnonymous]
        [Route("getDetails")]
        public List<User> GetUsers()
        {
            var users = db.Users.Where(x=>x.IsActive==true).OrderByDescending(x => x.Id).ToList().RemoveReferences();
            return users;
        }
        [Route("userByEmail")]
        public IHttpActionResult GetByEmail(string email)
        {
            var result = db.Users.Where(x => x.IsActive == true && x.Email == email).FirstOrDefault().RemoveReferences();
            return Ok(result);
        }
        [HttpPost]
        [AllowAnonymous]
        [Route("getUserId")]
        public User GetUserCode(string sku)
        {
            User model = new User();
            bool data = db.Users.Any(x => x.FirstName == sku && x.IsActive == true);
            if (data == true)
            {
                model = db.Users.Where(x => x.IsActive == true && x.FirstName == sku).FirstOrDefault();
            }
            else
            {
                string[] sizes = sku.Split(' ');
                int c = sizes.Count();
                if (c == 3)
                {
                    model.FirstName = sizes[0];
                    model.MiddleName = sizes[1];
                    model.LastName = sizes[2];
                }
               else if (c == 2)
                {
                    model.FirstName = sizes[0];
                    
                    model.LastName = sizes[1];
                }
                model.IsActive = true;
                db.Users.Add(model);
                db.SaveChanges();
                var list = db.Users.Where(x => x.IsActive == true).ToList();
                model = list.LastOrDefault().RemoveReferences();
            }
            return model;
        }
        [HttpPost]
        [Route("login")]
        public IHttpActionResult LoginUser(User model)
        {
			//var data = db.Users.ToList();
            var user = db.Users.Where( x=>x.Email.Equals(model.Email) && x.Password.Equals(model.Password)).FirstOrDefault();
			if (user!= null) {
				user = user.RemoveReferences();
			}

            return Ok(user);
        }
		[HttpPost]
		[Route("createList")]
		public IHttpActionResult CreateList(Dictionary<int, User> list)
		{
            Dictionary<string, string> result = new Dictionary<string, string>();
            foreach (var item in list)
			{
                item.Value.CreatedOn = System.DateTime.UtcNow;
                item.Value.UpdatedOn = System.DateTime.UtcNow;
                result.Add(item.Key + "#" + item.Value.Email, "");
                try
				{
					db.Users.Add(item.Value);
                    result[item.Key + "#" + item.Value.Email] = "Add";
                    db.SaveChanges();
				}catch(Exception ex)
				{
                    if (ex.Message != null)
                    {
                        result[item.Key + "#" + item.Value.Email] = ex.Message;
                    }
                }
			}
			return Ok(result);
		}
        [HttpPost]
        [Route("updateList")]
        public IHttpActionResult UpdateList(Dictionary<int, User> list)
        {
            Dictionary<string, string> result = new Dictionary<string, string>();
            foreach (var item in list)
            {
                item.Value.UpdatedOn = System.DateTime.UtcNow;
                result.Add(item.Key + "#" + item.Value.Email, "");
                if(item.Value.Email != null)
                {
                    var obj = db.Users.Where(x => x.Email == item.Value.Email).FirstOrDefault();
                    obj.BranchID = item.Value.BranchID;
                    obj.Email = item.Value.Email;
                    obj.FirstName = item.Value.FirstName;
                    obj.IsActive = item.Value.IsActive;
                    obj.IsPrimaryAccountHolder = item.Value.IsPrimaryAccountHolder;
                    obj.IsVerified = item.Value.IsVerified;
                    obj.LastName = item.Value.LastName;
                    obj.MiddleName = item.Value.MiddleName;
                    obj.Password = item.Value.Password;
                    obj.PasswordHash = item.Value.PasswordHash;
                    obj.PasswordSalt = item.Value.PasswordSalt;
                    obj.Phone = item.Value.Phone;
                    obj.RoleID = item.Value.RoleID;
                    obj.UpdatedOn = System.DateTime.UtcNow;
                    try
                    {
                        result[item.Key + "#" + item.Value.Email] = "Update";
                        db.SaveChanges();
                    }
                    catch (Exception ex)
                    {
                        if (ex.Message != null)
                        {
                            result[item.Key + "#" + item.Value.Email] = ex.Message;
                        }
                    }
                }
                
            }
            return Ok(result);
        }
        [HttpPost]
        [Route("getUserImportFilter")]
        public IHttpActionResult getProductImportFilter(Dictionary<int, Tuple<string, string, string, string>> model)
        {
            Dictionary<int, Tuple<string, string, string, string>> tuple = new Dictionary<int, Tuple<string, string, string, string>>();
            if (model!= null && model.Count > 0)
            {
                var role = model.Select(x => x.Value.Item1).Distinct().ToList();
                var branch = model.Select(x => x.Value.Item2).Distinct().ToList();
                var dbRole = db.Roles.Where(x => x.IsActive == true && role.Contains(x.RoleName)).ToList();
                var dbBranch = db.Branches.Where(x => x.IsActive == true && branch.Contains(x.Name)).ToList();
                foreach (var item in model)
                {
                    var a = (dbRole.Where(x => x.RoleName == item.Value.Item1).FirstOrDefault()?.Id).ToString();
                    var b = (db.Branches.Where(x => x.Name == item.Value.Item2).FirstOrDefault()?.Id).ToString();
                    tuple.Add(item.Key, Tuple.Create(item.Value.Item1, item.Value.Item2, a, b));
                }
            }
            return Ok(tuple);
        }
        [HttpPost]
		[Route("CheckUserName")]
		public IHttpActionResult CheckName(string name)
		{
			var data = db.Users.Where(x => x.IsActive == true && (x.FirstName + " " + x.MiddleName + " " + x.LastName).Contains(name)).FirstOrDefault();
			return Ok(data);
		}
		[HttpPost]
		[Route("CheckUserFullName")]
		public IHttpActionResult CheckFullName(string name)
		{
			var status = db.Users.Any(x => x.IsActive == true && (x.FirstName + " " + x.MiddleName + " " + x.LastName).Contains(name));
			return Ok(status);
		}
		[HttpGet]
        [AllowAnonymous]
        [Route("getUserPaging")]
        public ServiceResult<List<User>> GetUserPaging([FromUri]Paging paging)

        {
            ServiceResult<List<User>> model = new ServiceResult<List<User>>();
            var source = db.Users.Where(x => x.IsActive == true).OrderByDescending(x => x.Id).ToList().RemoveReferences();
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

            // if CurrentPage is greater than 1 means it has previousPage  
            var previousPage = CurrentPage > 1 ? "Yes" : "No";

            // if TotalPages is greater than CurrentPage means it has nextPage  
            var nextPage = CurrentPage < TotalPages ? "Yes" : "No";

            model.TotalCount = count;
            model.data = items;

            return model;

        }
        [HttpPost]
        [AllowAnonymous]
        [Route("getSearchData")]
        public ServiceResult<List<User>> GetSearchData(UserSearch userSearch)

        {
            ServiceResult<List<User>> model = new ServiceResult<List<User>>();
            var source = db.Users.Where(x => x.IsActive == true);
            var pageSize = 10;
            if (userSearch != null)
            {
                if (userSearch.IsActive == false)
                {
                    source = db.Users.Where(x => x.IsActive == true);
                }

                    if (!string.IsNullOrEmpty(userSearch.FirstName) || !string.IsNullOrEmpty(userSearch.LastName) || !string.IsNullOrEmpty(userSearch.Email) || !string.IsNullOrEmpty(userSearch.Phone) || !string.IsNullOrEmpty(userSearch.iFirstName))

                    { source = source.Where(m => m.FirstName == userSearch.FirstName || m.LastName == userSearch.LastName || m.Phone == userSearch.Phone || m.Email == userSearch.Email || m.FirstName == userSearch.iFirstName);
                    }
                if (userSearch.FirstName != null)
                {
                    source = db.Users.Where(x => x.IsActive == true && x.FirstName.Contains(userSearch.FirstName.ToLower()));
                }
                if(userSearch.LastName!=null)
                {
                    source = source.Where(x => x.LastName.Contains(userSearch.LastName.ToLower()));
                }
                if (userSearch.Email != null)
                {
                    source = source.Where(x => x.Email.Contains(userSearch.Email.ToLower()));
                }
                if (userSearch.Phone != null)
                {
                    source = source.Where(x => x.Phone.Contains(userSearch.Phone.ToLower()));
                }
            }

            int count =source.Count();
            var items = source.OrderBy(m => m.Id).Skip(((userSearch.Page ?? 1) - 1) * pageSize).Take(pageSize).ToList();
            model.data = items.Select(x => new DAL.User
            {
                Id=x.Id,
                FirstName=x.FirstName,
                MiddleName=x.MiddleName,
                LastName=x.LastName,
                Phone=x.Phone,
                Email=x.Email,
                BranchID=x.BranchID,
                IsVerified=x.IsVerified,
                RoleID=x.RoleID,
                Password=x.Password
               
            }).ToList().RemoveReferences();
            model.TotalCount = count;
           
            return model; ;
        }

        // GET: api/Users/5
        [HttpGet]
        [AllowAnonymous]
        [Route("getDetail")]
        [ResponseType(typeof(User))]
        public IHttpActionResult GetUser(int id)
        {
            User user = db.Users.Find(id).RemoveReferences();
            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }
        [HttpPost]
        [Route("checkUser")]
        public IHttpActionResult CheckUser(Dictionary<int, string> list)
        {
            var obj = new Dictionary<int, bool>();
            if (list != null && list.Count > 0)
            {
                var UserList = db.Users.Where(x => x.IsActive == true);
                var email = list.Select(x => x.Value).Distinct().ToList();
                if (email!= null && email.Count > 0)
                {
                    UserList = UserList.Where(x => email.Contains(x.Email));
                }
                var Color = UserList.ToList();
                foreach (var item in list)
                {
                    var result = Color.Any(x => x.Email == item.Value);
                    obj.Add(item.Key, result);
                }
            }
            return Ok(obj);
        }
        // PUT: api/Users/5
        [HttpPost]
        [AllowAnonymous]
        [Route("edit")]
        [ResponseType(typeof(User))]
        public IHttpActionResult PutUser(int id, User user)
        {
			var pageName = Request.RequestUri.LocalPath.getRouteName();
			Object obj = null;
			var UserId = 0;
			if (Request.Headers.Contains("Email"))
			{
				var email = ((string[])(Request.Headers.GetValues("Email")))[0].ToString();
				UserId = db.Users.Where(x => x.IsActive == true && x.Email.Contains(email)).FirstOrDefault().Id;
			}
			var model = db.Users.Find(id);
            model.FirstName = user.FirstName;
            model.MiddleName = user.MiddleName;
            model.LastName = user.LastName;
            model.Email = user.Email;
            model.Phone = user.Phone;
            model.RoleID = user.RoleID;
            model.IsActive = true;model.UpdatedOn = System.DateTime.UtcNow;
            model.IsVerified = user.IsVerified;
            if (user.Password != null)
            {
                var salt = CommonFunction.CreateSalt(64);      //Generate a cryptographic random number.  
                var hashAlgorithm = new SHA512HashAlgorithm();
                user.PasswordHash = hashAlgorithm.GenerateSaltedHash(CommonFunction.GetBytes(user.Password), salt);
                user.PasswordSalt = salt;
               // db.Entry(user).State = EntityState.Modified;
            }
			try
			{
				db.SaveChanges();
			}
			catch (Exception ex) { obj = ex; }
			finally
			{
				if (obj == null)
				{

					var logTable = Newtonsoft.Json.JsonConvert.SerializeObject(model, new JsonSerializerSettings()
					{
						PreserveReferencesHandling = PreserveReferencesHandling.Objects,
						Formatting = Formatting.Indented
					});
					var flag = model.CreateLog(pageName, logTable, UserId);
				}
			}
			return Ok(true);
          
        }
        [HttpPost]
        [AllowAnonymous]
        [Route("resetPassword")]
        [ResponseType(typeof(void))]
        public IHttpActionResult PutUserPass(int id,User user)
        {
            var users = db.Users.Find(id);
            var salt = CommonFunction.CreateSalt(64);      //Generate a cryptographic random number.  
            var hashAlgorithm = new SHA512HashAlgorithm();
            users.PasswordHash = hashAlgorithm.GenerateSaltedHash(CommonFunction.GetBytes(user.Password), salt);
            users.PasswordSalt = salt;
            users.UpdatedOn = System.DateTime.UtcNow;
            users.Password = user.Password;
            db.SaveChanges();
            return Ok(true);
        }
        [HttpPost]
        [AllowAnonymous]
        [Route("create")]
        [ResponseType(typeof(User))]
        public IHttpActionResult PostUser(User user)
        {
            User model = new DAL.User();
            var salt = CommonFunction.CreateSalt(64);      //Generate a cryptographic random number.  
            var hashAlgorithm = new SHA512HashAlgorithm();
            model.PasswordSalt = salt;
            model.PasswordHash= hashAlgorithm.GenerateSaltedHash(CommonFunction.GetBytes(user.Password), salt);
            model.FirstName = user.FirstName;
            model.MiddleName = user.MiddleName;
            model.LastName = user.LastName;
            model.IsVerified = user.IsVerified;
            model.Password = user.Password;
            model.Phone = user.Phone;
            model.RoleID = user.RoleID;
            model.BranchID = user.BranchID;
            model.Email = user.Email;
            model.IsActive = true;
            model.CreatedOn = System.DateTime.UtcNow;
            model.UpdatedOn = System.DateTime.UtcNow;
            db.Users.Add(model);
            db.SaveChanges();
            return Ok(true);
        }
        [HttpPost]
        [AllowAnonymous]
        [Route("checkUserPhoneNumber")]
        public bool GetPhoneNumber(User user)
        {
            if (user.Id > 0)
            {
                var phoneNumber = db.Users.Where(x => x.Id == user.Id && x.IsActive == true).FirstOrDefault();
                if (phoneNumber.Phone.Equals(user.Phone))
                {
                    return false;
                }

            }
            var data = db.Users.Any(x => x.Phone == user.Phone && x.IsActive == true);
            return data;
        }
        [HttpPost]
        [AllowAnonymous]
        [Route("checkUserEmail")]
        public bool GetEmail(User user)
        {
            if (user.Id > 0)
            {
                var email = db.Users.Where(x => x.Id == user.Id && x.IsActive == true).FirstOrDefault();
                if (email.Email.Equals(user.Email))
                {
                    return false;
                }

            }
            var data = db.Users.Any(x => x.Email == user.Email && x.IsActive == true);
            return data;
        }
        [HttpPost]
        [AllowAnonymous]
        [Route("checkUserEmail1")]
        public bool GetEmail1(User user)
        {
            if (user.Id > 0)
            {
                var email = db.Users.Where(x => x.Id == user.Id && x.IsActive == true).FirstOrDefault();
                if (email.Email.Equals(user.Email))
                {
                    return false;
                }

            }
            var data = db.Users.Any(x => x.Email != user.Email && x.IsActive == true);
            return data;
        }
        [HttpPost]
        [AllowAnonymous]
        [Route("Delete")]
        [ResponseType(typeof(User))]
        public IHttpActionResult DeleteUser(int id)
        {
            User user = db.Users.Find(id);
            if (user == null)
            {
                return NotFound();
            }
            user.IsActive = false;
            user.UpdatedOn = System.DateTime.UtcNow;
            db.SaveChanges();
            return Ok(user);
        }


        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool UserExists(int id)
        {
            throw new NotImplementedException();
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("checkEmail")]
        public IHttpActionResult CheckUserEmail(string email)
        {
            var user = db.Users.Any(x => x.IsActive == true && x.Email == email);
            return Ok(user);
        }
     
    }
}
