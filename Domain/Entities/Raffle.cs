using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Domain.Entities
{
    public class Raffle:KeyedEntity
    {
        public long Id { get; set; }
        public string Name { get; set;}
        public bool IsActive { get; set;}
        public int WinMultiplier { get; set; }
        public int MaxBets { get; set; }
        public DateTime StartDate { get; set;}  
        public DateTime EndDate {get; set;}

        /*for multiple numbers we would need to modify this*/
        public int WinningNumber { get; set; }
        
        public virtual ICollection<PlayerRaffle> PlayerRaffles { get; set; } 
        public virtual ICollection<Winner> Winners { get; set; }

        [NotMapped]
        public override long Key { get { return this.Id; } set { this.Id = value; } }
    }
}
