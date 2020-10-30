
namespace Application.Actions.UserActions
{
    public class UserQuery: ApiAction
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Lastname { get; set; }
        public string Email { get; set; }
        public string Username { get; set; }
    }
}
