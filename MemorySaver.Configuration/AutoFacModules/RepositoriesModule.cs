using Autofac;
using MemorySaver.Data.Repositories;
using MemorySaver.Domain.Interfaces.RepositoriesInterfaces;

namespace MemorySaver.Configuration.AutoFacModules
{
    public class RepositoriesModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterGeneric(typeof(GenericRepository<>)).As(typeof(IGenericRepository<>));
            builder.RegisterType<UserRepository>().As<IUserRepository>();
            builder.RegisterType<ChestRepository>().As<IChestRepository>();
            builder.RegisterType<FileRepository>().As<IFileRepository>();
        }
    }
}
