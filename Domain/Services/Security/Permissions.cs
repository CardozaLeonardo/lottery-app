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
            public const string Add = "users.add";
            public const string Edit = "users.edit";
            public const string EditRole = "users.edit.role";
            public const string TestPermission = "users.test";
        }
    }
}
