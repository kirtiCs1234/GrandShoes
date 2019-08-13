using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;

namespace POS.Security
{
    public class MyPrincipal : IPrincipal
    {
        public IIdentity Identity { get; set; }

        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string FullName { get { return FirstName + " " + LastName; } }

        public string Email { get; set; }
        public int RoleId { get; set; }

        public int? BranchId { get; set; }

        public bool IsAdmin { get; set; }

        public MyPrincipal(string username)
        {
            this.Identity = new GenericIdentity(username);
        }

        public bool IsInRole(string role)
        {
            return true;
        }
    }
}