using Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Application.Actions.UserActions
{
    public class CreateUserCommand: ApiAction
    {
        [Required]
        [MinLength(5)]
        [MaxLength(60)]
        public string Name { get; set; }

        [Required]
        [MinLength(5)]
        [MaxLength(60)]
        public string Lastname { get; set; }

        [EmailAddress]
        [Required]
        public string Email { get; set; }

        [Required]
        public string Username { get; set; }

        [Required]
        [MinLength(8)]
        [MaxLength(40)]
        public string Password { get; set; }

        public int RoleId { get; set; }
        
    }
}
