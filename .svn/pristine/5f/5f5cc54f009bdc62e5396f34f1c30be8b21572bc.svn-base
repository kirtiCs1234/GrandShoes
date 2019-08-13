using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.IService
{
    public interface IProductStyleService
    {
        List<ProductStyleModel> GetAll();
        List<string> GetCode();
        List<ProductStyleModel> ProductStyleAutocomplete(string name);
        bool Create(ProductStyleModel model);
        bool Edit(ProductStyleModel model);
        ProductStyleModel Details(int id);
        ProductStyleModel Delete(ProductStyleModel style);
        List<ProductStyleModel> GetPaging(int? page, out int TotalCount);
        List<ProductStyleModel> GetSearchData(ProductStyleSearch styleSearch, int? page, out int TotalCount);
        ProductStyleModel GetStyleSKUId(string sku);
    }
}

