﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
   public class BaseModel
    {
        public int ReturnCode { get; set; }
        public string ReturnMessage { get; set; }
        public string RedirectTo { get; set; }
    }
}
