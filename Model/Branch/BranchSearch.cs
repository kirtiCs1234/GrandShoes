﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public partial class BranchSearch
    {
        public int Id { get; set; }
        public string BranchCode { get; set; }
        public string Name { get; set; }
        public bool IsActive { get; set; }
        public int? Page { get; set; }
        public int PageSize { get; set; }
        public bool IsAdvanceSearch { get; set; }

    }
}
