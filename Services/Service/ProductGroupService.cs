using Helper;
using Model;
using Services.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Service
{
   public class ProductGroupService :IProductGroupService
    {
        public List<ProductGrpModel> GetAll()
        {
            var body = "";
            List<ProductGrpModel> ProductGroupModelList= ServerResponse.Invoke< List<ProductGrpModel >> ("api/productGroup/getDetails", body, "get");
            return ProductGroupModelList;
        }
        public ProductGrpModel GetProductGroupId(string sku)
        {
            var getStyleSkuId = ServerResponse.Invoke<ProductGrpModel>("api/productGroup/getProductGroupId?sku=" + sku, "", "POST");
            return getStyleSkuId;
        }
    }
}
