using System;
using System.Collections.Generic;
using System.Text;

namespace MemorySaver.Domain.Entities
{
    public class File : BaseEntity
    {
        public string Description { get; set; }

        public string FileName { get; set; }

        public string Type { get; set; }

        public string FacebookId { get; set; }

        public string VimeoId { get; set; }

        public Guid ChestId { get; set; }

        public Chest Chest { get; set; }
    }
}
