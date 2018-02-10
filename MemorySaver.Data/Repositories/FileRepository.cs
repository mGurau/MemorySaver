using MemorySaver.Domain.Entities;
using MemorySaver.Domain.Interfaces.RepositoriesInterfaces;

namespace MemorySaver.Data.Repositories
{
    public class FileRepository : GenericRepository<File>, IFileRepository
    {
        public FileRepository(MemorySaverDBContext context) : base(context)
        {
        }
    }
}
