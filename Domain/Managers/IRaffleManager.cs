using Application.Dto;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Managers
{
    public interface IRaffleManager:IBaseManager<Raffle>
    {
        BetAttemptResult AddBetToRaffle(BetAttemptStart bet);
    }
}
