using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Domain.Entities
{
    public class Player:KeyedEntity
    {
        public long Id { get; set; }
        public string Identification { get; set;}
        public ICollection<PlayerRaffle> RaffleTickets { get; set; }
        public ICollection<Winner> Win { get; set; }

        [ForeignKey("User")]
        public long UserId { get; set; }
        public User User { get; set; }

        [NotMapped]
        public override long Key { get { return this.Id; } set { this.Id = value; } }
    }
}
