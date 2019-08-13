using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public partial class ColorSearch
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string iCode { get; set; }
        public string ColorLong { get; set; }
        public string ColorShort { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public int? Page { get; set; }
        public int PageSize { get; set; }
      
    }
}
