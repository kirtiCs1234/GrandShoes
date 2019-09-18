using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class SalesWeeklyData
    {
        public int Id { get; set; }
        public string WeekDate { get; set; }
        public int? TotalData{get;set;}
        public int? SoldData { get; set; }
        public double? percent { get; set; }
        public double? TotalPercent { get; set; }
        public int? Totalsold { get; set; }
    }
}
