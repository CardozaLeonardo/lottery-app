using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Domain.Entities
{
    public class User: KeyedEntity
    {
        public long Id { set; get; }
        public string Name { set; get; }
        public string LastName { set; get; }
        public string Username { set; get; }
        public string Email { set; get; }
        public string Password { set; get; }

        public ICollection<UserRole> UserRoles { get; set; }
        public virtual Player Player { get; set; }

        [NotMapped]
        public override long Key { get { return this.Id; } set { this.Id = value; } }
    }
}
