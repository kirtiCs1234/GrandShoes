using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using Model.Color;
using DAL;

namespace POSApi.Controllers
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

    [RoutePrefix("api/Color")]
    public class ColorController : ApiController
    {
        private GrandShoesEntities Entities = new GrandShoesEntities();

        // GET: Colors
        [HttpGet]
        [Route("GetAllColor")]
        public IHttpActionResult GetAllColor()
        {
            ResponsePaging<List<ColorModel>> response = new ResponsePaging<List<ColorModel>>();
            var ColorsList = Entities.Colors.Where(s => s.IsActive == true).ToList().Select(m => new ColorModel
            {
                Id = m.Id,
                Code = m.Code,
                ColorShort = m.ColorShort,
                ColorLong = m.ColorLong,
                IsActive=m.IsActive
            }).ToList();       
            return Ok(ColorsList);
        }

        //Add Colors
        [HttpPost]
        [Route("addcolor")]
        public IHttpActionResult AddColor(ColorModel Color)
        {
            DAL.Color color = new DAL.Color();
            color.Code = Color.Code;
            color.ColorLong = Color.ColorLong;
            color.ColorShort = Color.ColorShort;
            color.IsActive = true;
            Entities.Colors.Add(color);
            Entities.SaveChanges();
            return Ok(true);
        }

        //Delete a color
        [HttpPost]
        [Route("deleteColor")]
        public IHttpActionResult DeleteColor(int Id)
        {
            DAL.Color color = new DAL.Color();
            color = Entities.Colors.Where(s => s.Id == Id).FirstOrDefault();
            color.IsActive = false;
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
        [Route("UpdateColor")]
        public IHttpActionResult updateColor(ColorModel Color)
        {
            DAL.Color color = new DAL.Color();
            color = Entities.Colors.Where(s => s.Id == Color.Id).FirstOrDefault();
            color.Code = Color.Code;
            color.ColorLong = Color.ColorLong;
            color.ColorShort = Color.ColorShort;
            color.IsActive = Color.IsActive;
            Entities.SaveChanges();
            return Ok(true);
        }

        //Checking Exdisting Color code
        [HttpGet]
        [Route("iscolorexist")]
        public IHttpActionResult isColorExist(int Id,string Code)
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
            var data = Entities.Colors.Where(x => x.IsActive == true && x.Code.Contains(name)).ToList().Select(m => new Model.Color.ColorModel
            {
                Code = m.Code,
                Id = m.Id,
            }).ToList();
            return Ok(data);
        }
    }
}