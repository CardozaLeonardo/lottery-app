using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Domain.Entities
{
    public class Role: KeyedEntity
    {
        public long Id { set; get; }
        public string Name { set; get; }

        [NotMapped]
        public override long Key { get { return this.Id; } set { this.Id = value; } }
    }
}
