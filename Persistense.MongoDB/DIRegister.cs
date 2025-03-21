using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;
using NoCode.Application.Interfaces;
using NoCodeConstructor.Persistense.Stores;

namespace NoCodeConstructor.Persistense;

public static class DIRegister
{
    public static IServiceCollection AddMongoDBPipelineStore(this IServiceCollection serviceCollection, string connectionString)
    {
        serviceCollection.AddScoped<IMongoClient>(
            ex =>
                new MongoClient(
                    connectionString
                    )
        );
        serviceCollection.AddScoped<IMongoDatabase>(ex =>
        {
            var client = ex.GetRequiredService<IMongoClient>();
            return client.GetDatabase("Pipelines");
        });

        serviceCollection.AddScoped<ISchemeStore, SchemeStore>();

        return serviceCollection;
    }
}