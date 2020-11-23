using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Actions.UserActions
{
    public class UserProfileQuery
    {
        public string Name { set; get; }
        public string LastName { set; get; }
        public string Username { set; get; }
        public string Email { set; get; }
        public ICollection<RoleQuery> Roles { get; set;}
        public ICollection<PermissionQuery> Permissions { get; set;}

    }
}
