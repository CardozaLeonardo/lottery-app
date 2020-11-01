using System;
using System.Collections.Generic;

using Application;
using Application.Actions.UserActions;
using AutoMapper;
using Domain.Entities;
using Domain.Managers;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : BaseController<User, CreateUserCommand, UserQuery>
    {
        private readonly IUserManager _userManager;
        

        public UserController(IObjectFactory factory, IMapper mapper): base()
        {
            _userManager = factory.Resolve<IUserManager>();
            _manager = factory.Resolve<IUserManager>();
            _mapper = mapper;
        }
    }
}
