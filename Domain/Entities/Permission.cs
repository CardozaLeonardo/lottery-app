using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    public class Permission: KeyedEntity 
    {
        public long PermissionId { get; set; }
        public string Name {get; set;}
        public string CodeName { get; set;}

        [NotMapped]
        public override long Key { get { return this.PermissionId; } set { this.PermissionId = value; } }
    }
}
