using System;
using System.Linq;
using MemorySaver.Domain.Entities;
using MemorySaver.Domain.Interfaces.RepositoriesInterfaces;
using Microsoft.EntityFrameworkCore;

namespace MemorySaver.Data.Repositories
{
    public class ChestRepository : GenericRepository<Chest>, IChestRepository
    {
        private MemorySaverDBContext context;

        public ChestRepository(MemorySaverDBContext context) : base(context)
        {
            this.context = context;
        }

        public Chest GetByIdIncludeAll(Guid chestId)
        {
            return context.Chests.Include(c => c.FilesInChest).FirstOrDefault(c => c.Id == chestId);
        }
    }
}
