using DAL;
using Helper;
using Model;
using Newtonsoft.Json;
using Services.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Services.Service
{
   public class ColorService: IColorService
    {
        public List<ColorModel> GetAll()
        {
            var body = "";
            List<ColorModel> ColorModelList= ServerResponse.Invoke<List<ColorModel>>("api/color/getDetails", body, "get");
            return ColorModelList;
        }
        public List<Color> GetAllDAL()
        {
            var body = "";
            List<Color> ColorModelList = ServerResponse.Invoke<List<Color>>("api/color/getDetails", body, "get");
            return ColorModelList;
        }
        public bool AddColor(ColorModel Color)
        {
            return ServerResponse.Invoke<bool>("/api/Color/create", JsonConvert.SerializeObject(Color), "post");
        }
        public List<ColorModel> GetPaging(int? page, out int TotalCount)
        {
            var body = "";
           // int pageSize = 10;
            int pageNumber = (page ?? 1);
            var result = ServerResponse.Invoke<ServiceResult<List<ColorModel>>>("api/color/getColorPaging?pageNumber=" + page, body, "GET");
            TotalCount = result.TotalCount;
            if (result.data != null)
            {
                var model = result.data.ToList();
                return model;
            }
            else
            {

            }
            return result.data.ToList();
        }
        public Dictionary<string,string> CreateList(Dictionary<int,ColorModel> list)
		{
			return ServerResponse.Invoke<Dictionary<string,string>>("api/color/createList", JsonConvert.SerializeObject(list), "Post");
		}
        public Dictionary<string, string> UpdateList(Dictionary<int, ColorModel> list)
        {
            return ServerResponse.Invoke<Dictionary<string, string>>("api/color/updateList", JsonConvert.SerializeObject(list), "Post");
        }
        public List<ColorModel> GetSearchData(ColorSearch colorSearch, int? page, out int TotalCount)
        {

          //  int pageSize = 10;
            int pageNumber = (page ?? 1);
            var body = JsonConvert.SerializeObject(colorSearch);
            var result = ServerResponse.Invoke<ServiceResult<List<ColorModel>>>("api/color/getSearchData", body, "Post");
            TotalCount = result.TotalCount;
            if (result.data != null)
            {
                var model = result.data.ToList();
                return model;
            }
            else
            {

            }
            return result.data.ToList();
        }
        public bool CheckColorCode(string chk)
        {
            var body = JsonConvert.SerializeObject(chk);
            var CheckColorCode = ServerResponse.Invoke<bool>("api/color/checkColorCode?chk=" + chk, body, "POST");
            return CheckColorCode;
        }
        public bool DeleteColor(int id)
        {
            return ServerResponse.Invoke<bool>("/api/color/deleteColor?id=" + id, "", "post");
        }

        public ColorModel GetColorByColorId(int id)
        {
            return ServerResponse.Invoke<ColorModel>("/api/color/getColorById?id=" + id, "", "get");
        }

        public bool UpdateColor(ColorModel Color)
        {
            return ServerResponse.Invoke<bool>("/api/color/edit", JsonConvert.SerializeObject(Color), "post");
        }
        public bool IsColorExist(int Id, string ColorCode)
        {
            ColorCode = System.Web.HttpUtility.UrlEncode(ColorCode);
            return ServerResponse.Invoke<bool>("/api/color/iscolorexist?id=" + Id + "&code=" + ColorCode, "", "Get");
        }

        public List<ColorModel> SearchColor(ColorModel Color)
        {
            return ServerResponse.Invoke<List<ColorModel>>("/api/color/SearchColor", JsonConvert.SerializeObject(Color), "post");
        }
        public List<ProductVarianceModel> ColorAutocompleteSelected(string name, int? id)
        {
            name = System.Web.HttpUtility.UrlEncode(name);
            return ServerResponse.Invoke<List<ProductVarianceModel>>("api/color/ColorsAutocompleteSelected?name=" + name+"&&id="+id, "", "get");

        }
        public List<ColorModel> ColoeAutocomplete(string name)
        {
            name = System.Web.HttpUtility.UrlEncode(name);
            return ServerResponse.Invoke<List<ColorModel>>("api/color/ColorsAutocomplete?name=" + name, "", "get");
        }
        public ColorModel GetColorId(string sku)
        {
            var getColorId = ServerResponse.Invoke<ColorModel>("api/color/getColorId?sku=" + sku, "", "POST");
            return getColorId;
        }
        public Dictionary<int,bool> CheckColor(Dictionary<int,string> list)
        {
            return ServerResponse.Invoke<Dictionary<int, bool>>("api/color/checkColor", JsonConvert.SerializeObject(list), "Post");
        }
    }
}
