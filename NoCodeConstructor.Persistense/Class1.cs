using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;
using NoCode.Application.Interfaces;
using NoCodeConstructor.Persistense.Stores;

namespace NoCodeConstructor.Persistense;

public static class DIRegister
{
    public static IServiceCollection AddPersistence(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddScoped<IMongoClient>(
            ex => 
                new MongoClient(
                    "mongodb://root:example@localhost:27017/")
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