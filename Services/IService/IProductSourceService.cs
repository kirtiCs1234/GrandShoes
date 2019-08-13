using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.IService
{
    public interface IProductSourceService
    {
        List<ProductSourceModel> GetAll();
        List<ProductSourceModel> GetPaging(int? page, out int TotalCount);
        List<ProductSourceModel> GetSearchData(ProductSourceSearch productSourceSearch, int? page, out int TotalCount);
        ProductSourceModel GetById(int? id);
        bool Create(ProductSourceModel productSourceModel);
        bool Edit(ProductSourceModel productSourceModel);
        bool Delete(ProductSourceModel productSourceModel);
        bool CheckSource(ProductSourceModel model);
        ProductSourceModel GetProductSourceId(string sku);
    }
}
