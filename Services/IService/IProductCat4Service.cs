using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public interface IProductCat4Service
    {
        List<ProductCat4Model> GetAll();
        ProductCat4Model GetById(int? Id);
        bool Delete(int? Id);
        bool Edit(ProductCat4Model model);
        bool Create(ProductCat4Model model);
        List<ProductCat4Model> GetPaging(int? page, out int TotalCount);
        List<ProductCat4Model> GetSearchData(ProductCatSearch areaSearch, int? page, out int TotalCount);
    }
}
