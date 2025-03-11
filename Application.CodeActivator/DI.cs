using Microsoft.Extensions.DependencyInjection;
using NoCode.Application.Interfaces;
using NodeBuilder.Validator;

namespace NodeBuilder;

public static class DI
{
    public static IServiceCollection AddNodeRegistration(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddSingleton<NodeActivator>();
        serviceCollection.AddScoped<ISchemeActivator, SchemeActivator>();
        return serviceCollection;
    }
    

    public static IServiceProvider AddNodeType<T>(this IServiceProvider serviceProvider)
    {
        var activator = serviceProvider.GetRequiredService<NodeActivator>();
        activator.AppendNewAvaliableNode([typeof(T)]);
        return serviceProvider;
    }
}