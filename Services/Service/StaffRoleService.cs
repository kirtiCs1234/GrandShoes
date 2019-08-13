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
    class StaffRoleService: IStaffRoleService
    {
        public List<StaffRoleModel> GetAll()

        {
            var body = "";
            List<StaffRoleModel> StaffRoleModelList = ServerResponse.Invoke<List<StaffRoleModel>>("api/staffRole/getDetails", body, "get");
            return StaffRoleModelList;
        }
        public StaffRoleModel GetById(int? id)
        {
            var body = "";
            StaffRoleModel StaffRoleModelById = ServerResponse.Invoke<StaffRoleModel>("api/staffRole/getDetail?id=" + id, body, "GET");
            return StaffRoleModelById;
        }
    }
}
