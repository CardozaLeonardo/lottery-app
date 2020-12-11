using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Actions.UserActions
{
    public class PlayerQuery:ApiAction
    {
        public long Id { get; set; }
        public string Identification { get; set; }
        public long UserId { get; set; }
    }
}
