using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Dtos
{
    public class RaffleResult
    {
        public long PlayerId { get; set; }
        public long RaffleId { get; set; }
        public string PlayerFirstName { get; set; }
        public string PlayerLastName { get; set; }
        public bool BetResult { get; set; }
        public int BetAmount { get; set; }
        public int WinAmount { get; set; }
        public int BetNumber { get; set; }
    }

}
