using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Report
{
    public class HelpReportModel
    {
        public string startDate { get; set; }
        public string endDate { get; set; }
        public string branchFrom { get; set; }
        public string branchTo { get; set; }
        public string productSKUFrom { get; set; }
        public string productSKUTo { get; set; }
        public string styleSKUFrom { get; set; }
        public string styleSKUTo { get; set; }
    }
}
