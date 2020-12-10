using Application;
using Application.Actions.RaffleActions;
using AutoMapper;
using Domain.Entities;
using Domain.Managers;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
   [Route("api/[controller]")]
    [ApiController]
    public class RaffleController : BaseController<Raffle, CreateRaffleCommand, RaffleQuery>
    {
        private readonly IRaffleManager _roleManager;

        public RaffleController(IObjectFactory factory, IMapper mapper) : base()
        {
            _mapper = mapper;
            _roleManager = factory.Resolve<IRaffleManager>();
        }

    }
}