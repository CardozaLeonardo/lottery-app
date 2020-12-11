using Application.Dto;
using Domain.Dtos;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Managers
{
    public interface IRaffleManager:IBaseManager<Raffle>
    {
        BetAttemptResult AddBetToRaffle(BetAttemptStart bet);
        RaffleResultsWrapper RunRaffle(long raffleId);
        RaffleResultsWrapper GetRaffleWinners(long raffleId);
        RaffleResultWrapper GetPlayerNumberResult(long playerId, long raffleId, int raffleNumber);
        BetAttemptResult EditBet(long playerId, long raffleId, int raffleNumber ,int newAmount);
        RaffleResultsWrapper GetPlayerResults(long playerId, long raffleId);
    }
}
