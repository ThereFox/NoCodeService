using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace NodeBuilder.NodeRegister;

public static class DI
{
    public static IServiceCollection AddNodeAssembly(this IServiceCollection collection, Assembly assembly)
    {
        return collection;
        //var types = assembly.GetTypes().Where(ex => ex.GetInterfaces().Any(ex => ex == typeof(IActionNode))).ToList();
    }
}