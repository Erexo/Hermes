using Microsoft.Extensions.Configuration;
using System;

namespace Hermes.Infrastructure.Extensions
{
    public static class ConfigurationExtension
    {
        public static T GetSettings<T>(this IConfiguration cfg) where T : new()
        {
            var name = typeof(T).Name.Replace("Settings", string.Empty);
            
            T settings = new T();
            cfg.GetSection(name).Bind(settings);

            return settings;
        }
    }
}
