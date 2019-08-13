using Model;
using Model.Log;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.IService
{
  public  interface ILogService
    {
        List<LogModel> GetAll();
        LogModel GetById(int? id);
        List<LogModel> GetPaging(int? page, out int totalCount);
        List<LogModel> GetSearchData(LogSearch LogSearch, int? page, out int TotalCount);


    }
}
