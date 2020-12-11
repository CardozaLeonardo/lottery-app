using Application;
using Application.Actions.UserActions;
using AutoMapper;
using Domain.Entities;
using Domain.Managers;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlayerController: BaseController<Player, CreatePlayerCommand, PlayerQuery>
    {
        public PlayerController(IObjectFactory factory, IMapper mapper) : base()
        {
            _mapper = mapper;
            _manager = factory.Resolve<IPlayerManager>();
        }
    }
}
