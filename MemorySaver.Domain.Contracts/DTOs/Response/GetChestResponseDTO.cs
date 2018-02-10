using System.Collections.Generic;
using MemorySaver.Domain.Entities;

namespace MemorySaver.Domain.ServiceContracts.DTOs.Response
{
    public class GetChestResponseDTO
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public bool IsPublic { get; set; }

        public IEnumerable<File> FilesInChest { get; set; }
    }
}
