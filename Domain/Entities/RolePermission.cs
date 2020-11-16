using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    public class RolePermission
    {
        public long RoleId { get; set; }
        public Role Role { get; set; }
        public long PermissionID { get; set; }
        public Permission Permission { get; set; }
    }
}