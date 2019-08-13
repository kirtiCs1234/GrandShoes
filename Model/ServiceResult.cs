using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class ServiceResult<T> where T: class
    {
      public bool Status { get; set; }
      public T data { get; set;}
        public int pageSize { get; set; }
      public int TotalCount { get; set; }
    }
}
