using Autofac;
using Hermes.Core.Repositories;
using System.Reflection;

namespace Hermes.Infrastructure.Modules
{
    public class RepositoryModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            var assembly = typeof(RepositoryModule).GetTypeInfo().Assembly;

            builder.RegisterAssemblyTypes(assembly)
                .Where(o => o.IsAssignableTo<IRepository>())
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope();
        }
    }
}
