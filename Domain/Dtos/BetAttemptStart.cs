using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Dto
{
    public class BetAttemptStart
    {
        public int Bet { get; set; }
        public int BetNumber { get; set; }
        public long PlayerId { get; set; }
        public long RaffleId { get; set; }
    }
}
