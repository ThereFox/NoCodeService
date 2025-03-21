using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using NoCodeConstructor.Nodes.Interfaces;
using Persistense.EFCore.DTOs;
using Persistense.EFCore.Stores;

namespace Persistense.EFCore;

public static class DI
{
    public static IServiceCollection AddEFCoreKeyValue(this IServiceCollection services,
        Action<DbContextOptionsBuilder> optionsAction)
    {
        services.AddDbContext<ApplicationContext>(optionsAction, ServiceLifetime.Transient);

        services.AddTransient<IKeyValueStore, KeyValueStore>();
        
        return services;
    }
}