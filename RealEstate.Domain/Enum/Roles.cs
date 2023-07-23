using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstate.Domain.Enum
{
    public enum Roles
    {
        SuperAdministrator = 1,
        Admin = 2,
        User = 3
    }

    public static class RoleType
    {
        public const string SuperAdministrator = "SuperAdministrator";
        public const string Admin = "Admin";
        public const string User = "User";
        public const string SuperAdminOrOwner = SuperAdministrator + "," + Admin;
    }
}
