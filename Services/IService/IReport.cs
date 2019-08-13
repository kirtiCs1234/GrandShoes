using Model.Report;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.IService
{
    public interface IReport
    {
        Dictionary<int, string> GetAllBranch();
        List<BranchStockStatusReport> GetAllbranchStockStatus();
        List<StaffCommitionListModel> GetAllStaff();
    }
}
