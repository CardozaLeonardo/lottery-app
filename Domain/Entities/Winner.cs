using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Domain.Entities
{
    public class Winner
    {
        public long Id { get; set; }
        public int AmountEarned { get; set; }

        [ForeignKey("PlayerRaffle")]
        public long PlayerRaffleId { get; set; }
        public virtual PlayerRaffle PlayerRaffle { get; set; }
    }
}
