using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.IService
{
   public interface IStaffService
    {
        List<StaffModel> GetAll();
        List<StaffModel> GetPaging(int? page, out int TotalCount);
        List<StaffModel> GetSearchData(StaffSearch staffSearch, int? page, out int TotalCount);
        StaffModel GetById(int? id);
        StaffModel Create(StaffModel staffModel);
        StaffModel Edit(StaffModel staffModel);
        StaffModel Delete(StaffModel staffModel);
    }
}
