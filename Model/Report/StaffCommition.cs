using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Report
{
    public class StaffCommitionListModel
    {
        public string Staff { get; set; }
        public string Category { get; set; }
        public string EmployeeCode { get; set; }
        public string BranchNumber { get; set; }
        public int SalesId { get; set; }
        public Decimal Total { get; set; }
        public Decimal CommitionPercent { get; set; }
        public Decimal CommitionValue { get; set; }
    }
}
