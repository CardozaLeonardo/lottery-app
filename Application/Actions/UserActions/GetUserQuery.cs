

using Domain.Entities;
using System.Collections.Generic;

namespace Application.Actions.UserActions
{
    public class GetUserQuery
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Lastname { get; set; }
        public string Email { get; set; }
        public string Username { get; set; }
        public ICollection<RoleQuery> Roles { get; set; }
        public PlayerQuery Player { get; set;}
    }
}
