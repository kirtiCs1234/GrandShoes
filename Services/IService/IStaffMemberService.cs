using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.IService
{
    public interface IStaffMemberService
    {
        List<StaffMemberModel> GetAll();
        StaffMemberModel GetById(int? id);
        StaffMemberModel GetByIdLogCheckDetails(int? id);
        bool Create(StaffMemberModel staffMemberModel);
        bool Edit(StaffMemberModel staffMemberModel);
        bool Delete(StaffMemberModel staffMemberModel);
        List<StaffMemberModel> GetPaging(int? page, out int totalCount);
        List<StaffMemberModel> GetSearchData(StaffMemberSearch areaSearch, int? page, out int TotalCount);
        bool Disable(StaffMemberModel model);
        bool Enable(StaffMemberModel model);
    }
}
