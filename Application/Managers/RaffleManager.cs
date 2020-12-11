using Application.Dto;
using Domain.Dtos;
using Domain.Entities;
using Domain.Managers;
using Microsoft.EntityFrameworkCore;
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
                        RaffleId = raffle.Id,
                        BetAmount = bet.Bet,
                        SelectedNumber = bet.BetNumber
                    });

                    _context.SaveChanges();
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

        public RaffleResultsWrapper RunRaffle(long raffleId)
        {
            List<int> numbers = new List<int>();
            Raffle currentRaffle = _dbSet.Find(raffleId);
            List<RaffleResult> raffleResult = new List<RaffleResult>();

            if (currentRaffle != null && currentRaffle.WinningNumber == 0)
            {
                List<Winner> winners = new List<Winner>();

                for (var i = 1; i <= _maxNumber; i++)
                {
                    numbers.Add(i);
                }

                IList<int> winnerNumbers = GetRandomNumber(numbers, _amountOfWinners);

                currentRaffle.WinningNumber = winnerNumbers[0];

                IQueryable<PlayerRaffle> winnerPlayerTickets = _context.PlayerRaffle.Include(pr => pr.Player).ThenInclude(p => p.User).Where(p => p.RaffleId == raffleId && winnerNumbers.Contains(p.SelectedNumber));

                foreach (var winnerTicket in winnerPlayerTickets)
                {
                    Winner winner = new Winner
                    {
                        AmountEarned = winnerTicket.BetAmount * currentRaffle.WinMultiplier,
                        PlayerRaffleId = winnerTicket.Id
                    };

                    winners.Add(winner);
                    _context.Winner.Add(winner);

                    raffleResult.Add(new RaffleResult
                    {
                        PlayerId = winnerTicket.PlayerId,
                        RaffleId = winnerTicket.RaffleId,
                        PlayerFirstName = winnerTicket.Player.User.Name,
                        PlayerLastName = winnerTicket.Player.User.LastName,
                        BetAmount = winnerTicket.BetAmount,
                        BetNumber = winnerTicket.SelectedNumber,
                        WinAmount = winner.AmountEarned,
                        BetResult = true
                    });

                }

                currentRaffle.IsActive = false;
                currentRaffle.EndDate = DateTime.Now;

                _context.SaveChanges();

                if(raffleResult.Count == 0)
                {
                    return new RaffleResultsWrapper
                    {
                        RaffleResults = raffleResult,
                        Amount = raffleResult.Count,
                        Message = "No winners"
                    };
                }

                return new RaffleResultsWrapper
                {
                    RaffleResults = raffleResult,
                    Amount = raffleResult.Count,
                    Message = "Success"
                };
            }

            var message = "Something went wrong";

            if(currentRaffle == null)
                message = "No raffle with this id";
            else
            {
                if (currentRaffle.WinningNumber != 0)
                    message = "Raffle already played";
            }
           

            return new RaffleResultsWrapper
            {
                Message = message
            };

        }

        private IList<int> GetRandomNumber(IEnumerable<int> numbers, int maxCount)
        {
            Random random = new Random(DateTime.Now.Millisecond);
            Dictionary<double, int> randomSortTable = new Dictionary<double, int>();

            foreach (int someType in numbers)
                randomSortTable[random.NextDouble()] = someType;

           return randomSortTable.OrderBy(KVP => KVP.Key).Take(maxCount).Select(KVP => KVP.Value).ToList();
        }

        public RaffleResultsWrapper GetRaffleWinners(long raffleId)
        {
            bool raffle = _dbSet.Any(p => p.Id == raffleId);

            if (raffle)
            {
               List<Winner> winners = _context.Winner.Include(w => w.PlayerRaffle).ThenInclude( pr => pr.Player).ThenInclude(p => p.User).Where(p => p.PlayerRaffle.RaffleId == raffleId).ToList();
               List<RaffleResult> raffleResult = new List<RaffleResult>();

                foreach (var winner in winners)
                {
                    raffleResult.Add(new RaffleResult
                    {
                        PlayerId = winner.PlayerRaffle.PlayerId,
                        RaffleId = winner.PlayerRaffle.RaffleId,
                        PlayerFirstName = winner.PlayerRaffle.Player.User.Name,
                        PlayerLastName = winner.PlayerRaffle.Player.User.LastName,
                        BetAmount = winner.PlayerRaffle.BetAmount,
                        BetNumber = winner.PlayerRaffle.SelectedNumber,
                        WinAmount = winner.AmountEarned,
                        BetResult = true
                    });
                }

                if (raffleResult.Count > 0)
                {
                    return new RaffleResultsWrapper
                    {
                        RaffleResults = raffleResult,
                        Amount = raffleResult.Count,
                        Message = "Success"
                    };
                }
                else
                {
                    return new RaffleResultsWrapper
                    {
                        RaffleResults = raffleResult,
                        Amount = raffleResult.Count,
                        Message = "No winners in this raffle"
                    };
                } 
            }

            return new RaffleResultsWrapper
            {
                Message = "No raffle found with this Id"
            };

        }

        public RaffleResultWrapper GetPlayerNumberResult(long playerId, long raffleId, int raffleNumber)
        {
            bool raffle = _dbSet.Any(p => p.Id == raffleId);

            if (raffle)
            {
                PlayerRaffle playerRaffle = _context.PlayerRaffle.Include(pr => pr.Winner).Where(pr => pr.PlayerId == playerId && pr.RaffleId == raffleId && pr.SelectedNumber == raffleNumber).FirstOrDefault();
                int winAmount = 0;

                if (playerRaffle.Id == 0)
                {
                    return new RaffleResultWrapper
                    {
                        Message = "No bet found with that number and player"
                    };
                }

                if (playerRaffle.Winner != null)
                    winAmount = playerRaffle.Winner.AmountEarned;


                return new RaffleResultWrapper
                {
                    RaffleResult = new RaffleResult
                    {
                        PlayerId = playerId,
                        RaffleId = raffleId,
                        BetAmount = playerRaffle.BetAmount,
                        BetNumber = playerRaffle.SelectedNumber,
                        WinAmount = winAmount,
                        BetResult = playerRaffle.Winner != null ?
                    true : false
                    },
                    Message = "Success"
                };

            }

            return new RaffleResultWrapper{
                Message = "No raffle found with that Id"    
            };
        }

        public RaffleResultsWrapper GetPlayerResults(long playerId, long raffleId)
        {
            bool raffle = _dbSet.Any(p => p.Id == raffleId);
            List<RaffleResult> results = new List<RaffleResult>();

            if (raffle)
            {
                List<PlayerRaffle> playerRaffles = _context.PlayerRaffle.Include(pr => pr.Winner).Where(pr => pr.PlayerId == playerId && pr.RaffleId == raffleId).ToList();
                int winAmount = 0;

                foreach(var playerRaffle in playerRaffles)
                {
                    if (playerRaffle.Winner != null)
                        winAmount = playerRaffle.Winner.AmountEarned;

                    results.Add(new RaffleResult
                    {
                        PlayerId = playerId,
                        RaffleId = raffleId,
                        BetAmount = playerRaffle.BetAmount,
                        BetNumber = playerRaffle.SelectedNumber,
                        WinAmount = winAmount,
                        BetResult = playerRaffle.Winner != null ?
                        true : false
                    });

                    if(results.Count > 0)
                    {
                        return new RaffleResultsWrapper
                        {
                            Message = "Success",
                            RaffleResults = results,
                            Amount = results.Count
                        };
                    }
                    else
                    {
                        return new RaffleResultsWrapper
                        {
                            Message = "No Bets found for current player in raffle",
                            RaffleResults = results,
                            Amount = results.Count
                        };
                    }
                  
                }
            }

            return new RaffleResultsWrapper
            {
                Message = "No raffle found with that id",
                RaffleResults = results,
                Amount = results.Count
            }; ;
        }



        public BetAttemptResult EditBet(long playerId, long raffleId, int raffleNumber ,int newAmount)
        {
           PlayerRaffle playerRaffle = _context.PlayerRaffle.Where(pr => pr.PlayerId == raffleId && pr.PlayerId == playerId && pr.SelectedNumber == raffleNumber).FirstOrDefault();
            playerRaffle.BetAmount = newAmount;

            _context.SaveChanges();

            return new BetAttemptResult {Message = "Bet updated" ,  Approved = true};
        }


    }
}
