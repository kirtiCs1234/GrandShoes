﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class ChangeModel
    {
        public int Id { get; set; }
        public string Fieldname { get; set; }
        public string FieldValue { get; set; }
        public int RowNo { get; set; }
        public bool IsActive { get; set; }
    }
}
