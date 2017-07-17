using Autofac;
using Hermes.Infrastructure.Extensions;
using Hermes.Infrastructure.Settings;
using Microsoft.Extensions.Configuration;

namespace Hermes.Infrastructure.Modules
{
    public class SettingsModule : Autofac.Module
    {
        private readonly IConfiguration _configuration;

        public SettingsModule(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterInstance(_configuration.GetSettings<JwtSettings>())
                .SingleInstance();
            builder.RegisterInstance(_configuration.GetSettings<MongoSettings>())
                .SingleInstance();
        }
    }
}
