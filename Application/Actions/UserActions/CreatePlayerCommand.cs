using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Application.Actions.UserActions
{
    public class CreatePlayerCommand:ApiAction
    {
        public string Identification { get; set; }
        [Required]
        public long UserId { get; set; }
    }
}
