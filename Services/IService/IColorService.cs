﻿using DAL;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.IService
{
   public interface IColorService
    {
        List<ColorModel> GetAll();
        bool AddColor(ColorModel Color);
        List<ColorModel> GetPaging(int? page, out int TotalCount);
        List<ColorModel> GetSearchData(ColorSearch colorSearch, int? page, out int TotalCount);
        bool DeleteColor(int id);
        bool IsColorExist(int Id, string ColorCode);
        bool UpdateColor(ColorModel Color);
        ColorModel GetColorByColorId(int id);
        bool CheckColorCode(string chk);
        List<ColorModel> SearchColor(ColorModel Color);
        List<ColorModel> ColoeAutocomplete(string name);
        ColorModel GetColorId(string sku);
        List<ProductVarianceModel> ColorAutocompleteSelected(string name, int? id);
        List<Color> GetAllDAL();
        Dictionary<int, bool> CheckColor(Dictionary<int, string> list);
        Dictionary<string, string> CreateList(Dictionary<int, ColorModel> list);
        Dictionary<string, string> UpdateList(Dictionary<int, ColorModel> list);
    }
}