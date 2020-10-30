using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    public abstract class KeyedEntity
    {
        public abstract long Key { get; set; }
    }
}
