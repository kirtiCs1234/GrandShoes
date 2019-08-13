using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.IService
{
   public interface IRoleService
    {
        List<RoleModel> GetAll();
        RoleModel GetById(int? id);
        bool Create(RoleModel roleModel);
        bool Edit(RoleModel roleModel);
        RoleModel Delete(RoleModel roleModel);
        List<RoleModel> GetPaging(int? page, out int TotalCount);
        List<RoleModel> GetSearchData(RoleModel search, int? page, out int TotalCount);
		RoleModel GetByRoleName(string name);
        List<PagePermissionModel> GetGrantPermission();
        List<PagePermissionModel> SetGrantPermission(List<PagePermissionModel> model);
        List<PageNameModel> getPageNameList(bool IsAdminPage);
        List<PagePermissionModel> getPagePermissionList(int RoleId, bool IsAdminPage);
    }
}
