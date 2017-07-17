using Autofac;
using Hermes.Infrastructure.Settings;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Conventions;
using MongoDB.Driver;

namespace Hermes.Infrastructure.Modules
{
    public class MongodbModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.Register(context => 
            {
                var settings = context.Resolve<MongoSettings>();
                return new MongoClient(settings.ConnectionString);
            }).SingleInstance();

            builder.Register(context =>
            {
                var client = context.Resolve<MongoClient>();
                var settings = context.Resolve<MongoSettings>();
                return client.GetDatabase(settings.Database);
            }).As<IMongoDatabase>();

            builder.RegisterInstance(new ConventionPack
            {
                new IgnoreExtraElementsConvention(true),
                new EnumRepresentationConvention(BsonType.String),
                new CamelCaseElementNameConvention()
            }).SingleInstance();
        }
    }
}
