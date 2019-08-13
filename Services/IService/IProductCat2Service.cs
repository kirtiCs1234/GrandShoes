using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public interface IProductCat2Service
    {
        List<ProductCat2Model> GetAll();
        ProductCat2Model GetById(int? Id);
        bool Delete(int? Id);
        bool Edit(ProductCat2Model model);
        bool Create(ProductCat2Model model);
        List<ProductCat2Model> GetPaging(int? page, out int TotalCount);
        List<ProductCat2Model> GetSearchData(ProductCatSearch areaSearch, int? page, out int TotalCount);
    }
}
