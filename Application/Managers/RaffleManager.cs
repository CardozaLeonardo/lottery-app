using Application.Dto;
using Domain.Entities;
using Domain.Managers;
using Persistence;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Application.Managers
{
    public class RaffleManager : BaseManager<Raffle>, IRaffleManager
    {
        private int _amountOfWinners = 1;
        private int _maxNumber = 99;

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
                        PlayerId = bet.PlayerId,
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

        private BetAttemptResult CheckBetsValid(long playerId, long raffleId, int betNumber)
        {
            IQueryable<PlayerRaffle> currentBetsForPlayer = _context.PlayerRaffle.Where(pr => pr.PlayerId == playerId && pr.RaffleId == raffleId);

            if (currentBetsForPlayer.Any(p => p.SelectedNumber == betNumber))
            {
                return new BetAttemptResult {
                    Approved = false,
                    Message = "There's a bet in this number already"

                };
            }

            return new BetAttemptResult
            {
                Approved = true,
                Message = "Bet Added"
            };

        }

        public List<Winner> GetRaffleWinners(long raffleId)
        {
            List<int> numbers = new List<int>();
            Raffle currentRaffle = _dbSet.Find(raffleId);

            if (currentRaffle != null)
            {
                List<Winner> winners = new List<Winner>();

                for (var i = 1; i <= _maxNumber; i++)
                {
                    numbers.Add(i);
                }

                IEnumerable<int> winnerNumbers = GetRandomNumber(numbers, _amountOfWinners);
                IQueryable<PlayerRaffle> winnerPlayerTickets = _context.PlayerRaffle.Where(p => p.RaffleId == raffleId && winnerNumbers.Contains(p.SelectedNumber));

                foreach (var winnerTicket in winnerPlayerTickets)
                {
                    winners.Add(new Winner
                    {
                        AmountEarned = winnerTicket.BetAmount * currentRaffle.WinMultiplier,
                        PlayerRaffleId = winnerTicket.Id
                    });
                }

                return winners;
            }

            return null;
        }

        private IEnumerable<int> GetRandomNumber(IEnumerable<int> numbers, int maxCount)
        {
            Random random = new Random(DateTime.Now.Millisecond);
            Dictionary<double, int> randomSortTable = new Dictionary<double, int>();

            foreach (int someType in numbers)
                randomSortTable[random.NextDouble()] = someType;

           return randomSortTable.OrderBy(KVP => KVP.Key).Take(maxCount).Select(KVP => KVP.Value);
        }


    }
}
