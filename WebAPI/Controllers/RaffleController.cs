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
        public ActionResult<BetAttemptResultQuery> RunRaffle(long raffleId)
        {
            List<RaffleResultQuery> result = _mapper.Map<List<RaffleResultQuery>>(_raffleManager.RunRaffle(raffleId));
            return Ok(result);
        }

        [Route("get-raffle-winners/{raffleId}")]
        public ActionResult<List<RaffleResultQuery>> GetRaffleWinners(long raffleId)
        {
            List<RaffleResultQuery> result = _mapper.Map<List<RaffleResultQuery>>(_raffleManager.GetRaffleWinners(raffleId));
            return Ok(result);
        }

        [Route("get-player-result/{raffleId}/{raffleNumber}")]
        public ActionResult<List<RaffleResultQuery>> GetPlayerNumberResult(long raffleId, int raffleNumber)
        {
            User user = _userManager.GetByUsernameOrEmailWithRole(HttpContext.User.Identity.Name);
            List<BetAttemptResultQuery> result = _mapper.Map<List<BetAttemptResultQuery>>(_raffleManager.GetPlayerNumberResult(user.Player.Id , raffleId, raffleNumber));
            return Ok(result);
        }

        [Route("get-player-result/{raffleId}")]
        public ActionResult<List<RaffleResultQuery>> GetPlayerResults(long raffleId)
        {
            User user = _userManager.GetByUsernameOrEmailWithRole(HttpContext.User.Identity.Name);
            List<BetAttemptResultQuery> result = _mapper.Map<List<BetAttemptResultQuery>>(_raffleManager.GetPlayerResults(user.Player.Id, raffleId));
            return Ok(result);
        }

        [Route("edit-bet/{raffleId}/{betNumber}/{betAmount}")]
        public ActionResult<List<RaffleResultQuery>> EditBet(long raffleId , int betNumber, int betAmount)
        {

            User user = _userManager.GetByUsernameOrEmailWithRole(HttpContext.User.Identity.Name);
            List<BetAttemptResultQuery> result = _mapper.Map<List<BetAttemptResultQuery>>(_raffleManager.EditBet(user.Player.Id, raffleId , betNumber , betAmount));
            return Ok(result);
        }

    }
}