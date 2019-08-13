using Helper;
using Model.Report;
using Services.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Service
{
    public class Report : IReport
    {
        public Dictionary<int, string> GetAllBranch()
        {
            var obj = new Dictionary<int, string>();
            obj = ServerResponse.Invoke<Dictionary<int, string>>("api/report/branch/getAll", "", "GET");
            return obj;
        }
        public List<BranchStockStatusReport> GetAllbranchStockStatus()
        {
            var obj = new List<BranchStockStatusReport>();
            obj = ServerResponse.Invoke<List<BranchStockStatusReport>>("api/report/branchStockStatus/getAll", "", "GET");
            return obj;
        }
        public List<StaffCommitionListModel> GetAllStaff()
        {
            var obj = new List<StaffCommitionListModel>();
            obj = ServerResponse.Invoke<List<StaffCommitionListModel>>("api/report/staff/getAll", "", "GET");
            return obj;
        }
    }
}
