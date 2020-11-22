using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Services.Security
{
    public class Permissions
    {
        public class CustomClaimTypes
        {
            public const string Permission = "Application.Permission";
        }

        public static class UserPermissions
        {
            public const string Add = "user.add";
            public const string Edit = "user.edit";
            public const string Modify = "user.modify";
            public const string Delete = "user.delete";
            public const string TestPermission = "user.test";
        }

        public static class RolePermissions
        {
            public const string Add = "role.add";
            public const string Edit = "role.edit";
            public const string ListPermissions = "role.permission.list"; 
        }

    }
}
