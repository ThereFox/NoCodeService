using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace NodeBuilder.NodeRegister;

public static class DI
{

    public static IServiceCollection AddNodeAssembly(Assembly assembly)
    {
        var types = assembly.GetTypes().Where(ex => ex.FindInterfaces().Any(ex => ex == typeof(IAction)));
    }
    
}