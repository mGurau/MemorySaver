using System;
using MemorySaver.Domain.Entities;

namespace MemorySaver.Domain.Interfaces.RepositoriesInterfaces
{
    public interface IChestRepository : IGenericRepository<Chest>
    {
        Chest GetByIdIncludeAll(Guid chestId);
    }
}
