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
using Helper;
using Newtonsoft.Json;

namespace POSApi.Controllers.Admin
{
    public class Response<T> where T : class
    {
        public int Status { get; set; }
        public T Data { get; set; }
        public string Message { get; set; }
    }
    public class ResponsePaging<T> : Response<T> where T : class
    {
        public int TotalPages { get; set; }
    }

    [RoutePrefix("api/color")]
    public class ColorsController : ApiController
    {
        private GrandShoesEntities Entities = new GrandShoesEntities();
        public ColorsController()
        {
            Entities.Configuration.LazyLoadingEnabled = false;
            Entities.Configuration.ProxyCreationEnabled = false;
        }
        // GET: Colors
        [HttpGet]
        [AllowAnonymous]
        [Route("getDetails")]
        public IHttpActionResult GetAllColor()
        {
            ResponsePaging<List<ColorModel>> response = new ResponsePaging<List<ColorModel>>();
            var ColorsList = Entities.Colors.Where(s => s.IsActive == true).OrderByDescending(x=>x.Id).ToList().Select(m => new ColorModel
            {
                Id = m.Id,
                Code = m.Code,
                ColorShort = m.ColorShort,
                ColorLong = m.ColorLong,
                IsActive = m.IsActive
            }).ToList();
            return Ok(ColorsList);
        }
		[HttpPost]
		[Route("createList")]
		public IHttpActionResult CreateList(Dictionary<string,Color> colorList)
		{
            Dictionary<string, string> result = new Dictionary<string, string>();
            string Message = string.Empty;
            foreach (var item in colorList)
			{
                item.Value.CreatedOn = System.DateTime.UtcNow;
                item.Value.UpdatedOn = System.DateTime.UtcNow;
                result.Add(item.Key + "#" + item.Value.Code, "");
                try
                {
                    Entities.Colors.Add(item.Value);
                    result[item.Key + "#" + item.Value.Code] = "Add";
                    Entities.SaveChanges();
                }
                catch (Exception ex)
                {
                    if (ex.Message != null)
                    {
                        result[item.Key + "#" + item.Value.Code] = ex.Message;
                    }
                }
            }
			return Ok(result);
		}
        [HttpPost]
        [AllowAnonymous]
        [Route("getColorId")]
        public Color GetCode(string sku)
        {
            Color model = new Color();
            bool data = Entities.Colors.Any(x => x.Code== sku && x.IsActive == true);
            if (data == true)
            {
                model = Entities.Colors.Where(x => x.IsActive == true && x.Code== sku).FirstOrDefault();
            }
            else
            {
                model.Code = sku;
                model.IsActive = true;
                Entities.Colors.Add(model);
                Entities.SaveChanges();
               var list = Entities.Colors.Where(x => x.IsActive == true).ToList();
                model = list.LastOrDefault();
            }
            return model;
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("getColorPaging")]
        public ServiceResult<List<Color>> GetColor([FromUri]Paging paging)
        {
            ServiceResult<List<Color>> model = new ServiceResult<List<Color>>();
            var source = Entities.Colors.Where(x => x.IsActive == true).OrderByDescending(x => x.Id).ToList();
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

            return model;
        }
      
        [HttpPost]
        [AllowAnonymous]
        [Route("getSearchData")]
        public ServiceResult<List<Color>> GetSearchData(ColorSearch colorSearch)
        {
            ServiceResult<List<Color>> model = new ServiceResult<List<Color>>();
            var source = Entities.Colors.Where(x => x.IsActive == true);
            var pageSize = 10;
            if (colorSearch != null)
            {

                if (!string.IsNullOrEmpty(colorSearch.Code)|| !string.IsNullOrEmpty(colorSearch.ColorLong)|| !string.IsNullOrEmpty(colorSearch.ColorShort) || !string.IsNullOrEmpty(colorSearch.iCode))
                    source = source.Where(m => m.Code == colorSearch.Code||m.ColorLong==colorSearch.ColorLong||m.ColorShort==colorSearch.ColorShort||m.Code==colorSearch.iCode);
            }
            int count = source.Count();
            var items=source.OrderByDescending(m=>m.Id).Skip(((colorSearch.Page ?? 1) - 1) * pageSize).Take(pageSize).ToList();
            model.data = items.Select(x => new Color
            {
               Id=x.Id,
               Code=x.Code,
               ColorLong=x.ColorLong,
               ColorShort=x.ColorShort,
               IsActive=x.IsActive
            }).ToList();
            model.TotalCount = count;
            return model; ;
        }

        //Add Colors
        [HttpPost]
        [Route("create")]
        public IHttpActionResult AddColor(ColorModel Color)
        {
			var pageName = Request.RequestUri.LocalPath.getRouteName();
			Object obj = null;
			var UserId = 0;
			if (Request.Headers.Contains("Email"))
			{
				var email = ((string[])(Request.Headers.GetValues("Email")))[0].ToString();
				UserId = Entities.Users.Where(x => x.IsActive == true && x.Email.Contains(email)).FirstOrDefault().Id;
			}
			DAL.Color color = new DAL.Color();
            color.Code = Color.Code;
            color.CreatedOn = System.DateTime.UtcNow;
            color.UpdatedOn = System.DateTime.UtcNow;
            color.ColorLong = Color.ColorLong;
            color.ColorShort = Color.ColorShort;
            color.IsActive = true;
            Entities.Colors.Add(color);
			try
			{
				Entities.SaveChanges();
			}
			catch (Exception ex) { obj = ex; }
			finally
			{
				if (obj == null)
				{

					var logTable = Newtonsoft.Json.JsonConvert.SerializeObject(color, new JsonSerializerSettings()
					{
						PreserveReferencesHandling = PreserveReferencesHandling.Objects,
						Formatting = Formatting.Indented
					});
					var flag = color.CreateLog(pageName, logTable, UserId);
				}
			}
			return Ok(true);
        }
        [HttpPost]
        [AllowAnonymous]
        [Route("checkColorCode")]
        public bool GetCodeId(string chk)
        {

            var data = Entities.Colors.Any(x => x.Code == chk && x.IsActive == true);
            return data;
        }
        //Delete a color
        [HttpPost]
        [Route("deleteColor")]
        public IHttpActionResult DeleteColor(int Id)
        {
            DAL.Color color = new DAL.Color();
            color = Entities.Colors.Where(s => s.Id == Id).FirstOrDefault();
            color.IsActive = false;
            color.UpdatedOn = System.DateTime.UtcNow;
            Entities.SaveChanges();
            return Ok(true);
        }

        //Get color by ColorId(Edit(By Id))
        [HttpGet]
        [Route("getColorById")]
        public IHttpActionResult GetColorById(int Id)
        {
            ColorModel color = new ColorModel();
            color = Entities.Colors.Where(s => s.IsActive == true && s.Id == Id).ToList().Select(m => new ColorModel
            {
                Id = m.Id,
                Code = m.Code,
                ColorShort = m.ColorShort,
                ColorLong = m.ColorLong,
                IsActive = m.IsActive
            }).FirstOrDefault();
            return Ok(color);
        }

        //Search color
        [HttpPost]
        [Route("SearchColor")]
        public IHttpActionResult SearchColor(ColorModel Color)
        {
            List<ColorModel> color = new List<ColorModel>();
            color = Entities.Colors.Where(s => (s.IsActive == true) && (s.Code.Contains(Color.Code) || s.ColorLong.Contains(Color.ColorLong) || s.ColorShort.Contains(Color.ColorShort))).ToList().Select(m => new ColorModel
            {
                Id = m.Id,
                Code = m.Code,
                ColorShort = m.ColorShort,
                ColorLong = m.ColorLong,
                IsActive = m.IsActive
            }).ToList();
            return Ok(color);
        }

        //Update Color
        [HttpPost]
        [Route("edit")]
        public IHttpActionResult updateColor(ColorModel Color)
        {
			var list = new List<Color>();
			var pageName = Request.RequestUri.LocalPath.getRouteName();
			Object obj = null;
			var UserId = 0;
			if (Request.Headers.Contains("Email"))
			{
				var email = ((string[])(Request.Headers.GetValues("Email")))[0].ToString();
				UserId = Entities.Users.Where(x => x.IsActive == true && x.Email.Contains(email)).FirstOrDefault().Id;
			}
			string logTable = "";
			DAL.Color color = new DAL.Color();
            color = Entities.Colors.Where(s => s.Id == Color.Id ||s.Code==Color.Code).FirstOrDefault();
            color.Code = Color.Code;
            color.ColorLong = Color.ColorLong;
            color.ColorShort = Color.ColorShort;
            color.UpdatedOn = System.DateTime.UtcNow;
            color.IsActive = Color.IsActive;
			try
			{
				Entities.SaveChanges();
				list.Add(color);
				return Ok(true);
			}
			catch (DbUpdateConcurrencyException ex)
			{
				obj = ex;
			}

			finally
			{
				if (obj == null)
				{
					logTable = "";
					var change = list.ToDataTables().getChangedRecords();
					var c = change.Count() / 2;
					for (var i = 0; i < c; i++)
					{
						logTable += change[i].Fieldname + " Old Value=[" + change[i].FieldValue + "] New Value=[" + change[i + c].FieldValue + "], ";
					}
					//logTable = Newtonsoft.Json.JsonConvert.SerializeObject(change);
					var flag = color.CreateLog(pageName, logTable, UserId);
				}
			}
			return Ok(true);
        }

        //Checking Exdisting Color code
        [HttpGet]
        [Route("iscolorexist")]
        public IHttpActionResult isColorExist(int Id, string Code)
        {
            var color = Entities.Colors.Any(s => s.IsActive == true && s.Id != Id && s.Code == Code);
            if (color)
                return Ok(true);
            else
                return Ok(false);
        }

        //Colors Auto complete
        [HttpGet]
        [Route("ColorsAutocomplete")]
        public IHttpActionResult ColorsAutocomplete(string name)
        {
            var data = Entities.Colors.Where(x => x.IsActive == true && x.Code.Contains(name)).ToList().Select(m => new Model.ColorModel
            {
                ColorShort = m.ColorShort,
                Code=m.Code,
                Id = m.Id,
            }).ToList();
            return Ok(data);
        }
        [HttpPost]
        [Route("checkColor")]
        public IHttpActionResult CheckColor(Dictionary<int,string> list)
        {
            var obj = new Dictionary<int, bool>();
            if(list!=null && list.Count> 0)
            {
                var colorList = Entities.Colors.Where(x => x.IsActive == true);
                var code=list.Select(x=>x.Value).Select(s => s[0]).Distinct().ToList();
                if(code!=null && code.Count > 0)
                {
                    colorList = colorList.Where(x => code.ToString().Contains(x.Code));
                }
                var Color = colorList.ToList();
                foreach (var item in list)
                {
                    var result = Color.Any(x => x.Code == item.Value);
                    obj.Add(item.Key, result);
                }
            }
            return Ok(obj);
        }
    }
}