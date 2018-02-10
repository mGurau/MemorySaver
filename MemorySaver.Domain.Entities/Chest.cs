using System;
using System.Collections.Generic;

namespace MemorySaver.Domain.Entities
{
    public class Chest : BaseEntity
    {
        public Guid OwnerId { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public bool IsPublic { get; set; }

        public User Owner { get; set; }

        public IEnumerable<File> FilesInChest { get; set; }
    }
}
