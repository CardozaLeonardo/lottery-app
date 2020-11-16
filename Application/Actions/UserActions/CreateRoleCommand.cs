using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Application.Actions.UserActions
{
    public class CreateRoleCommand:ApiAction
    {
        [Required]
        [MinLength(4)]
        [MaxLength(60)]
        public string Name { get; set; }
    }
}
