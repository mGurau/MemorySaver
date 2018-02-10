using System;
using MemorySaver.Domain.Entities;
using MemorySaver.Domain.Interfaces.RepositoriesInterfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace MemorySaver.Data.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T>
        where T : BaseEntity
    {
        private readonly MemorySaverDBContext context;
        private readonly DbSet<T> entityDbSet;

        public GenericRepository(MemorySaverDBContext context)
        {
            this.context = context;
            entityDbSet = this.context.Set<T>();
        }

        public void Add(T entity)
        {
            entityDbSet.Add(entity);
        }

        public void Delete(Guid id)
        {
            T entity = entityDbSet.FirstOrDefault(e => e.Id == id);

            if (entity != null)
            {
                entityDbSet.Remove(entity);
            }
        }

        public T GetById(Guid id)
        {
            return entityDbSet.FirstOrDefault(e => e.Id == id);
        }

        public bool SaveChages()
        {
            try
            {
                context.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public void Update(T entity)
        {
            entityDbSet.Update(entity);
        }
    }
}
