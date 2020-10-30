
using System.ComponentModel.DataAnnotations;

namespace Application.Actions.UserActions
{
    public class AuthCommand: ApiAction
    {
        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
