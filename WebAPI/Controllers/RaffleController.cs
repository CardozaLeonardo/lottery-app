using Application;
using Application.Actions.RaffleActions;
using Application.Dto;
using AutoMapper;
using Domain.Entities;
using Domain.Managers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WebAPI.Controllers
{
   [Route("api/[controller]")]
    [ApiController]
    public class RaffleController : BaseController<Raffle, CreateRaffleCommand, RaffleQuery>
    {
        private readonly IRaffleManager _raffleManager;
        private readonly IUserManager _userManager;

        public RaffleController(IObjectFactory factory, IMapper mapper) : base()
        {
            _mapper = mapper;
            _raffleManager = factory.Resolve<IRaffleManager>();
            _manager = factory.Resolve<IRaffleManager>();
            _userManager = factory.Resolve<IUserManager>();
        }

        [Route("add-bet")]
        public ActionResult<BetAttemptResultQuery> AddBet(CreateBetCommand betCommand)
        {
            betCommand.PlayerId = _userManager.GetByUsernameOrEmailWithRole(HttpContext.User.Identity.Name).Player.Id;
            BetAttemptResultQuery result = _mapper.Map<BetAttemptResultQuery>(_raffleManager.AddBetToRaffle(_mapper.Map<BetAttemptStart>(betCommand)));
            return Ok(result);
        }

        [Route("run-raffle/{raffleId}")]
        public ActionResult<RaffleResultsQueryWrapper> RunRaffle(long raffleId)
        {
            RaffleResultsQueryWrapper result = _mapper.Map<RaffleResultsQueryWrapper>(_raffleManager.RunRaffle(raffleId));
            return Ok(result);
        }

        [Route("get-raffle-winners/{raffleId}")]
        public ActionResult<RaffleResultQueryWrapper> GetRaffleWinners(long raffleId)
        {
            RaffleResultsQueryWrapper result = _mapper.Map<RaffleResultsQueryWrapper>(_raffleManager.GetRaffleWinners(raffleId));
            return Ok(result);
        }

        [Route("get-player-result/{raffleId}/{raffleNumber}")]
        public ActionResult<RaffleResultQueryWrapper> GetPlayerNumberResult(long raffleId, int raffleNumber)
        {
            User user = _userManager.GetByUsernameOrEmailWithRole(HttpContext.User.Identity.Name);
            RaffleResultQueryWrapper result = _mapper.Map<RaffleResultQueryWrapper>(_raffleManager.GetPlayerNumberResult(user.Player.Id , raffleId, raffleNumber));
            return Ok(result);
        }

        [Route("get-player-result/{raffleId}")]
        public ActionResult<RaffleResultQueryWrapper> GetPlayerResults(long raffleId)
        {
            User user = _userManager.GetByUsernameOrEmailWithRole(HttpContext.User.Identity.Name);
            RaffleResultQueryWrapper result = _mapper.Map<RaffleResultQueryWrapper>(_raffleManager.GetPlayerResults(user.Player.Id, raffleId));
            return Ok(result);
        }

        [Route("edit-bet/{raffleId}/{betNumber}/{betAmount}")]
        public ActionResult<List<BetAttemptResultQuery>> EditBet(long raffleId , int betNumber, int betAmount)
        {
            User user = _userManager.GetByUsernameOrEmailWithRole(HttpContext.User.Identity.Name);
            List<BetAttemptResultQuery> result = _mapper.Map<List<BetAttemptResultQuery>>(_raffleManager.EditBet(user.Player.Id, raffleId , betNumber , betAmount));
            return Ok(result);
        }

    }
}