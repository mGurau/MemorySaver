using System;
using System.Collections.Generic;
using System.Text;

namespace MemorySaver.Domain.Entities
{
    public class User : BaseEntity
    {
        public string Email { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Password { get; set; }

        public IEnumerable<Chest> OwnedChests { get; set; }
    }
}
