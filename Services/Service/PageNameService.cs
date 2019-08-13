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
    class PageNameService:IPageNameService
    {
        public List<PageNameModel> GetAll(bool IsAdminPage)
        {
            List<PageNameModel> list = new List<PageNameModel>();
            list = ServerResponse.Invoke<List<PageNameModel>>("api/page/getAll?IsAdminPage="+IsAdminPage,"","GET");
            return list;
        }
    }
}
