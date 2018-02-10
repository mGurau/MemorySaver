using System;
using AutoMapper;
using EnsureThat;
using MemorySaver.Domain.Entities;
using MemorySaver.Domain.Interfaces.RepositoriesInterfaces;
using MemorySaver.Domain.ServiceContracts.DTOs.Request;
using MemorySaver.Domain.ServiceContracts.DTOs.Response;
using MemorySaver.Domain.ServiceContracts.Interfaces;

namespace MemorySaver.Services
{
    public class ChestService : IChestService
    {
        private readonly IChestRepository chestRepository;
        private readonly IMapper mapper;

        public ChestService(IChestRepository chestRepository, IMapper mapper)
        {
            Ensure.That(chestRepository).IsNotNull();
            Ensure.That(mapper).IsNotNull();

            this.mapper = mapper;
            this.chestRepository = chestRepository;
        }

        public bool CreateChest(CreateChestRequestDTO chestModel)
        {
            Chest newChest = new Chest
            {
                OwnerId = chestModel.OwnerId,
                Name = chestModel.Name,
                Description = chestModel.Description,
                IsPublic = chestModel.IsPublic
            };
            chestRepository.Add(newChest);
            return chestRepository.SaveChages();
        }

        public GetChestResponseDTO GetChest(Guid chestId)
        {
            Chest chest = chestRepository.GetByIdIncludeAll(chestId);

            GetChestResponseDTO responseDTO = new GetChestResponseDTO
            {
                Name = chest.Name,
                Description = chest.Description,
                IsPublic = chest.IsPublic,
                FilesInChest = chest.FilesInChest
            };

            return responseDTO;
        }

        public bool EditChest(EditChestRequestDTO newChestInfo)
        {
            Chest chest = chestRepository.GetByIdIncludeAll(newChestInfo.Id);

            chest.Name = newChestInfo.Name;
            chest.Description = newChestInfo.Description;
            chest.IsPublic = newChestInfo.IsPublic;

            chestRepository.Update(chest);

            return chestRepository.SaveChages();

        }

        public bool DeleteChest(Guid chestId)
        {
            chestRepository.Delete(chestId);

            return chestRepository.SaveChages();
        }
    }
}