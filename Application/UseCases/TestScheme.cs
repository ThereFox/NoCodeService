using CSharpFunctionalExtensions;
using Microsoft.Extensions.DependencyInjection;
using NoCode.Application.Interfaces;
using NoCodeConstructor.Domain.Scheme.Realisations.Scheme;
using NodeBuilder.DTOs;

namespace NoCode.Application.UseCases;

public class TestScheme
{
    private readonly ISchemeActivator _activator;
    private readonly ISchemeStore _store;

    public TestScheme(ISchemeActivator activator, ISchemeStore store)
    {
        _activator = activator;
        _store = store;
    }

    public async Task<Result> TryRun(List<NodeConfigInputObject> config)
    {
        var node = _activator.Activate(config);

        if (node.IsFailure)
        {
            return node.ConvertFailure();
        }

        var result = await node.Value.HandleEvent(new EventInfo());

        return result;
    }
}