
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Application.Actions.RaffleActions
{
    public class CreateBetCommand:ApiAction
    {   [Required]
        public int Bet { get; set; }
        [Required]
        public int BetNumber { get; set; }
        [Required]
        public long PlayerId { get; set; }
        [Required]
        public long RaffleId { get; set; }
    }
}
