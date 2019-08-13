using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class Paging
    {
       // public List<AreaModel> areaModels { get; set; }
        const int maxPageSize = 120;

        public int pageNumber { get; set; } = 1;

        public int _pageSize { get; set; } = 10;
        public int TotalCount { get; set; }

        public int pageSize
        {

            get { return _pageSize; }
            set
            {
                _pageSize = (value > maxPageSize) ? maxPageSize : value;
            }
        }
        //public Paging()
        //{
        //    areaModels = new List<AreaModel>();
        //}
    }
}
