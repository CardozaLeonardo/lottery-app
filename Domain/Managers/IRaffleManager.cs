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
        List<RaffleResult> RunRaffle(long raffleId);
        List<RaffleResult> GetRaffleWinners(long raffleId);
        RaffleResult GetPlayerNumberResult(long playerId, long raffleId, int raffleNumber);
        BetAttemptResult EditBet(long playerId, long raffleId, int raffleNumber ,int newAmount);
        List<RaffleResult> GetPlayerResults(long playerId, long raffleId);
    }
}
