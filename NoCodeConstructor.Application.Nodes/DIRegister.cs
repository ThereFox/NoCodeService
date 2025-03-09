using Microsoft.Extensions.DependencyInjection;
using NoCodeConstructor.Domain.Abstactions;
using NodeBuilder;

namespace NoCodeConstructor.Nodes;

public static class DIRegister
{
    public static IServiceProvider AddCommonNodes(this IServiceProvider serviceProvider)
    {
        var activator = serviceProvider.GetService<NodeActivator>();
        
        var assemblyTypes = typeof(DIRegister).Assembly.GetTypes();

        var nodes = assemblyTypes.Where(
            ex =>
            {
                return ex.GetInterfaces()
                    .Any(subex => 
                        subex == typeof(INodeAction) 
                        ||
                        subex == typeof(IInputTrigger));
            })
            .ToList();

        activator.AppendNewAvaliableNode(nodes);

        return serviceProvider;
    }
}