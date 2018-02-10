using Autofac;
using MemorySaver.Domain.ServiceContracts.Interfaces;
using MemorySaver.Services;

namespace MemorySaver.Configuration.AutoFacModules
{
    public class ServicesModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<UserService>().As<IUserService>();
            builder.RegisterType<ChestService>().As<IChestService>();
            builder.RegisterType<FileService>().As<IFileService>();
        }
    }
}
