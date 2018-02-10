using System;
using System.Collections.Generic;
using System.Linq;
using MemorySaver.Domain.Entities;
using MemorySaver.Domain.Interfaces.RepositoriesInterfaces;
using Microsoft.EntityFrameworkCore;

namespace MemorySaver.Data.Repositories
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        private readonly MemorySaverDBContext context;

        public UserRepository(MemorySaverDBContext context) : base(context)
        {
            this.context = context;
        }

        public User GetByEmail(string userEmail)
        {
            return context.Users.FirstOrDefault(u => u.Email == userEmail);
        }

        public IEnumerable<Chest> GetUserChests(Guid userId)
        {
            var user = context.Users.Where(u => u.Id == userId).Include(u => u.OwnedChests).ToList();
            var chests = user.FirstOrDefault().OwnedChests;

            return chests;
        }
    }
}
