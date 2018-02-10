using System;
using MemorySaver.Domain.ServiceContracts.DTOs.Request;
using MemorySaver.Domain.ServiceContracts.DTOs.Response;

namespace MemorySaver.Domain.ServiceContracts.Interfaces
{
    public interface IChestService
    {
        bool CreateChest(CreateChestRequestDTO chestModel);

        GetChestResponseDTO GetChest(Guid chestId);

        bool EditChest(EditChestRequestDTO newChestInfo);

        bool DeleteChest(Guid chestId);
    }
}