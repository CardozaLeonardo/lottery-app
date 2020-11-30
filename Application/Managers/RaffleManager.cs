using Application.Dto;
using Domain.Entities;
using Domain.Managers;
using Persistence;
using System.Collections.Generic;
using System.Linq;

namespace Application.Managers
{
    public class RaffleManager: BaseManager<Raffle> , IRaffleManager
    {
        public RaffleManager(LotteryAppContext context) : base(context)
        {
            _context = context;
            _dbSet = context.Raffles;
        }

        public BetAttemptResult AddBetToRaffle(BetAttemptStart bet)
        {
            Raffle raffle = _dbSet.SingleOrDefault(b => b.Id == bet.RaffleId);
            Player player = _context.Players.SingleOrDefault(p => p.Id == bet.PlayerId);

            if (raffle != null && player != null && bet.BetNumber > 0 && bet.BetNumber < 99)
            {
                BetAttemptResult checkValid = CheckBetsValid(raffle.Id, player.Id, bet.BetNumber);

                if (checkValid.Approved)
                {
                    _context.PlayerRaffle.Add(new PlayerRaffle { 
                        PlayerId =  bet.PlayerId,
                        RaffleId = raffle.Id
                    });
                }

                return checkValid;

            }

            return new BetAttemptResult
            {
                Approved = false,
                Message = "Invalid Number"
            };

        }

        private BetAttemptResult CheckBetsValid(long playerId, long raffleId , int betNumber)
        {
           IQueryable<PlayerRaffle> currentBetsForPlayer = _context.PlayerRaffle.Where(pr => pr.PlayerId == playerId && pr.RaffleId == raffleId);

            if (currentBetsForPlayer.Any(p => p.SelectedNumber == betNumber))
            {
                return new BetAttemptResult {
                    Approved = false,
                    Message  = "There's a bet in this number already"

                };
            }

            return new BetAttemptResult
            {
                Approved = true,
                Message = "Bet Added"
            };

        }

    }
}
