using Application.Actions.UserActions;
using AutoMapper;
using Domain.Entities;

namespace Application.Mapping
{
    class UserProfile: Profile
    {

        public UserProfile()
        {
            CreateMap<CreateUserCommand, User>();
            CreateMap<User, UserQuery>();
        }
    }
}
