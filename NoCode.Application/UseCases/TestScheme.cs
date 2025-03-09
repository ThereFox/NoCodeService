using CSharpFunctionalExtensions;
using Microsoft.Extensions.DependencyInjection;
using NoCode.Application.Interfaces;
using NoCodeConstructor.Domain.Scheme.Realisations.Scheme;
using NodeBuilder.DTOs;

namespace NoCode.Application.UseCases;

public class TestScheme
{
    private readonly ISchemeActivator _activator;
    
    public TestScheme(ISchemeActivator activator)
    {
        _activator = activator;
    }

    public async Task<Result> HandleNode(List<NodeConfigInputObject> config)
    {
        var node = _activator.Activate(config);

        if (node.IsFailure)
        {
            return node.ConvertFailure();
        }

        var result = await node.Value.HandleEvent(new EventInfo());

        Console.WriteLine(result);

        return result;
    }
    
}