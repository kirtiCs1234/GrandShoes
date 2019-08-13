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
    class StaffStatusService:IStaffStatusService
    {
        public List<StaffStatusModel> GetAll()

        {
            var body = "";
            List<StaffStatusModel> StaffRoleModelList = ServerResponse.Invoke<List<StaffStatusModel>>("api/staffStatus/getDetails", body, "get");
            return StaffRoleModelList;
        }
        public StaffStatusModel GetById(int? id)
        {
            var body = "";
            StaffStatusModel StaffRoleModelById = ServerResponse.Invoke<StaffStatusModel>("api/staffStatus/getDetail?id=" + id, body, "GET");
            return StaffRoleModelById;
        }
    }
}

