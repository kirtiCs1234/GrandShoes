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
    public class YearServices:IYearServices
    {
        public YearModel GetYearId(string sku)
        {
            var getYearId = ServerResponse.Invoke<YearModel>("api/year/getYearId?sku=" + sku, "", "POST");
            return getYearId;
        }
    }
}
