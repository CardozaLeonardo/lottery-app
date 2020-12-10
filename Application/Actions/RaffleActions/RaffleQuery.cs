using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Actions.RaffleActions
{
    public class RaffleQuery: ApiAction
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public bool IsActive { get; set; }
        public int WinMultiplier { get; set; }
        public int MaxBets { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
