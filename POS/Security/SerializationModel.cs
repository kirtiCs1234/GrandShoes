﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace POS.Security
{
    public class SerializationModel
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string Email { get; set; }
        public string LastName { get; set; }
        public int? BranchId { get; set; }
        public int RoleId { get; set; }

    }
}