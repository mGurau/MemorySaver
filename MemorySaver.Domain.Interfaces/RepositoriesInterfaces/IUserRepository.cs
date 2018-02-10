using System;
using System.Collections.Generic;
using MemorySaver.Domain.Entities;

namespace MemorySaver.Domain.Interfaces.RepositoriesInterfaces
{
    public interface IUserRepository : IGenericRepository<User>
    {
        User GetByEmail(string userEmail);

        IEnumerable<Chest> GetUserChests(Guid userId);
    }
}
