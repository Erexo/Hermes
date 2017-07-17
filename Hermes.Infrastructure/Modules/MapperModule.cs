using Autofac;
using AutoMapper;
using Hermes.Core.Domain;
using Hermes.Infrastructure.DTO;

namespace Hermes.Infrastructure.Modules
{
    public class MapperModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            var mapper = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<User, UserDTO>();
                cfg.CreateMap<Message, MessageDTO>();
            })
            .CreateMapper();

            builder.RegisterInstance(mapper).SingleInstance();
        }
    }
}
