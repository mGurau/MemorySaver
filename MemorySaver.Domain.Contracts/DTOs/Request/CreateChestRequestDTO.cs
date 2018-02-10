using System;

namespace MemorySaver.Domain.ServiceContracts.DTOs.Request
{
    public class CreateChestRequestDTO
    {
        public Guid OwnerId { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public bool IsPublic { get; set; }
    }
}
