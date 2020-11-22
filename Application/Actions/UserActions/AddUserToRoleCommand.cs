using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Application.Actions.UserActions
{
    public class AddUserToRoleCommand:ApiAction
    {
        [Required]
        public long RoleId { get; set; }

        [Required]
        public long UserId { get; set; }
    }
}
