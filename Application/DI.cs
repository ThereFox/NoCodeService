using Microsoft.Extensions.DependencyInjection;
using NoCode.Application.UseCases;

namespace NoCode.Application;

public static class DI
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<TestScheme>();
        services.AddScoped<ExecuteSavedScheme>();
        services.AddScoped<SaveScheme>();

        return services;
    }
}