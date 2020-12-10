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
        List<Winner> RunRaffle(long raffleId);
        List<RaffleResult> GetRaffleWinners(long raffleId);
        RaffleResult GetPlayerResult(long playerId, long raffleId, int raffleNumber);
        BetAttemptResult EditBet(long playerId, long raffleId, int newAmount);
    }
}
