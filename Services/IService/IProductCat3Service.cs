using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public interface IProductCat3Service
    {
        List<ProductCat3Model> GetAll();
        ProductCat3Model GetById(int? Id);
        bool Delete(int? Id);
        bool Edit(ProductCat3Model model);
        bool Create(ProductCat3Model model);
        List<ProductCat3Model> GetPaging(int? page, out int TotalCount);
        List<ProductCat3Model> GetSearchData(ProductCatSearch areaSearch, int? page, out int TotalCount);
    }
}
