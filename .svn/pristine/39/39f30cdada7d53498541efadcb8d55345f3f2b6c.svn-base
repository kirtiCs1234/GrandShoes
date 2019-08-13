using Helper;
using Model;
using Newtonsoft.Json;
using Services.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Service
{
   public class StaffService :IStaffService
    {
        public List<StaffModel> GetAll()
        {
            var body = "";
            List<StaffModel> StaffModelList = ServerResponse.Invoke<List<StaffModel>>("api/staff/getDetails", body, "get");
            return StaffModelList;
        }
        public List<StaffModel> GetPaging(int? page, out int TotalCount)
        {
            var body = "";
           // int pageSize = 1;
            int pageNumber = (page ?? 1);
            var result = ServerResponse.Invoke<ServiceResult<List<StaffModel>>>("api/staff/getStaffPaging?pageNumber=" + page, body, "GET");
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
        public List<StaffModel> GetSearchData(StaffSearch staffSearch, int? page, out int TotalCount)
        {

           // int pageSize = 4;
            int pageNumber = (page ?? 1);
            var body = JsonConvert.SerializeObject(staffSearch);
            var result = ServerResponse.Invoke<ServiceResult<List<StaffModel>>>("api/staff/getSearchData", body, "Post");
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

        public StaffModel GetById(int? id)
        {
            var body = "";
            StaffModel StaffById = ServerResponse.Invoke<StaffModel>("api/staff/getDetail?id=" + id, body, "get");
            return StaffById;
        }
        public StaffModel Create(StaffModel staffModel)
        {
            var body = JsonConvert.SerializeObject(staffModel);
            StaffModel StaffCreate = ServerResponse.Invoke<StaffModel>("api/staff/create", body, "Post");
            return StaffCreate;
        }
        public StaffModel Edit(StaffModel staffModel)
        {
            var body = JsonConvert.SerializeObject(staffModel);
            StaffModel StaffEdit = ServerResponse.Invoke<StaffModel>("api/staff/edit?id=" + staffModel.Id, body, "POST");
            return StaffEdit;
        }
        public StaffModel Delete(StaffModel staffModel)
        {
            var body = JsonConvert.SerializeObject(staffModel);
            StaffModel StaffDelete = ServerResponse.Invoke<StaffModel>("api/staff/Delete?id=" + staffModel.Id, body, "POST");
            return StaffDelete;
        }

    }
}
