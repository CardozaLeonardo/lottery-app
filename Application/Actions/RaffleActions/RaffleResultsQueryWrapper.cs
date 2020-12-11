using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Actions.RaffleActions
{
    public class RaffleResultsQueryWrapper:ApiAction
    {
        public List<RaffleResultQuery> RaffleResults { get; set; }
        public string Message { get; set; }
        public int Amount { get; set; }
    }
}
