using System;
using System.Collections.Generic;
using System.Text;

namespace MemorySaver.Domain.ServiceContracts.DTOs.Request
{
    public class EditChestRequestDTO
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public bool IsPublic { get; set; }
    }
}
