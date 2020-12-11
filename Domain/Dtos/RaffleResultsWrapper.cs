using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Dtos
{
    public class RaffleResultsWrapper
    {
        public List<RaffleResult> RaffleResults { get; set; }
        public string Message { get; set; }
        public int Amount { get; set; }
    }
}
