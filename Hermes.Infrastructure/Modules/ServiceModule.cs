using Autofac;
using Hermes.Infrastructure.Services;
using System.Reflection;

namespace Hermes.Infrastructure.Modules
{
    public class ServiceModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            var assembly = typeof(ServiceModule).GetTypeInfo().Assembly;

            builder.RegisterAssemblyTypes(assembly)
                .Where(o => o.IsAssignableTo<IService>())
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope();
        }
    }
}
