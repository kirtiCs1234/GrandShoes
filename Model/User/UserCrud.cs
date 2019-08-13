﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.User
{
    public class UserCrud
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Password { get; set; }
        public Nullable<int> RoleID { get; set; }
        public Nullable<int> BranchID { get; set; }
        public Nullable<bool> IsVerified { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        public virtual AreaModel Area { get; set; }
        public virtual BranchModel Branch { get; set; }
        public string UserGuid { get; set; }
        public virtual RoleModel Role { get; set; }

    }
}
