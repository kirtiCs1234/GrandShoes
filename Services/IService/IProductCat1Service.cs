using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public interface IProductCat1Service
    {
        List<ProductCat1Model> GetAll();
        ProductCat1Model GetById(int? Id);
        bool Delete(int? Id);
        bool Edit(ProductCat1Model model);
        bool Create(ProductCat1Model model);
        List<ProductCat1Model> GetPaging(int? page, out int TotalCount);
        List<ProductCat1Model> GetSearchData(ProductCatSearch areaSearch, int? page, out int TotalCount);
    }
}
