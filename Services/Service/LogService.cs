using Helper;
using Model;
using Model.Log;
using Newtonsoft.Json;
using Services.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class LogService: ILogService
    {
        public List<LogModel> GetAll()
        {
            var body = "";
            var  LogModelList = ServerResponse.Invoke<List<LogModel>>("api/log/getDetails", body, "get");
            return LogModelList;
        }
        public LogModel GetById(int? id)
        {
           var body = "";
            var LogById = ServerResponse.Invoke<LogModel>("api/log/getDetail?id=" + id , body, "get");
            return LogById;
        }
        public List<LogModel> GetPaging(int? page, out int TotalCount)
        {
            var body = "";
            int pageNumber = (page ?? 1);
            var result = ServerResponse.Invoke<ServiceResult<List<LogModel>>>("api/log/getLogPaging?pageNumber=" + page, body, "GET");
            TotalCount = result.TotalCount;
            if (result.data != null)
            {
                var model = result.data.ToList();
                return model;
            }
            else
            {

            }
            return result.data.ToList();
        }
        public List<LogModel> GetSearchData(LogSearch LogSearch, int? page, out int TotalCount)
        {
            int pageNumber = (page ?? 1);
            var body = JsonConvert.SerializeObject(LogSearch);
            var result = ServerResponse.Invoke<ServiceResult<List<LogModel>>>("api/log/getSearchData", body, "Post");
            TotalCount = result.TotalCount;

            if (result.data != null)
            {
                var model = result.data.ToList();
                return model;
            }
          
            return result.data.ToList();
        }

    }
}
