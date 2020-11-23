using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Domain.Entities
{
    public class PlayerRaffle
    {
        public long Id { get; set; }
        public int SelectedNumber { get; set;}
        public int BetAmount { get; set; }

        [ForeignKey("Player")]
        public long PlayerId { get; set; }
        [ForeignKey("Raffle")]
        public long RaffleId { get; set; }

        public virtual Player Player { get; set; }
        public virtual Raffle Raffle { get; set; }
        public virtual Winner Winner { get; set; }
    }
}
