using MemorySaver.Domain.Entities;
using System;

namespace MemorySaver.Domain.Interfaces.RepositoriesInterfaces
{
    public interface IGenericRepository<T>
        where T : BaseEntity
    {
        void Add(T entity);

        T GetById(Guid id);

        void Update(T entity);

        void Delete(Guid id);

        bool SaveChages();
    }
}
