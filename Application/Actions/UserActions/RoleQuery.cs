using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Actions.UserActions
{
    public class RoleQuery:ApiAction
    {
        public long Id { set; get; }
        public string Name { get; set; }
    }
}
