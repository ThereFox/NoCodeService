using CSharpFunctionalExtensions;
using Microsoft.Extensions.DependencyInjection;
using NoCode.Application.Interfaces;
using NoCodeConstructor.Domain.Scheme.Realisations.Scheme;
using NoCodeConstructor.Domain.Scheme.Realisations.Scheme.Entitys;
using NodeBuilder.DTOs;

namespace NodeBuilder;

public class SchemeActivator : ISchemeActivator
{
    private readonly IServiceScopeFactory _factory;

    public SchemeActivator(IServiceScopeFactory factory)
    {
        _factory = factory;
    }

    public Result<CodeScheme> Activate(List<NodeConfigInputObject> nodes)
    {
        var unknowedNodes = nodes.Select(ex => ex.Id).ToHashSet();

        foreach (var node in nodes)
        {
            node.ConnectedElements
                .ForEach(ex => unknowedNodes.RemoveWhere(id => id == ex));
        }

        var inputs = nodes.Where(ex => unknowedNodes.Contains(ex.Id))
            .ToList();

        var actions = nodes.Where(ex => unknowedNodes.Contains(ex.Id) == false)
            .ToList();

        using (var scope = _factory.CreateScope())
        {
            var activator = scope.ServiceProvider.GetRequiredService<NodeActivator>();

            var activatedInputNodes = inputs
                .Select(ex => activator.ActivateInputNode(ex))
                .ToList();
            var activatedActionNodes = actions
                .Select(ex => activator.ActivateNode(ex))
                .ToList();

            if (
                activatedInputNodes.Any(
                    ex => ex.IsFailure
                )
                ||
                activatedActionNodes.Any(
                    ex => ex.IsFailure
                )
            )
            {
                var invalidOfInputs = activatedInputNodes.Any(ex => ex.IsFailure)
                    ? activatedInputNodes.Where(ex => ex.IsFailure)
                        .Select(ex => ex.Error)
                        .Aggregate((first, second) => $"{first}, {second}")
                    : "";
                var invalidOfActions = activatedActionNodes.Any(ex => ex.IsFailure)
                    ? activatedActionNodes.Where(ex => ex.IsFailure)
                        .Select(ex => ex.Error)
                        .Aggregate((first, second) => $"{first}, {second}")
                    : "";

                return Result.Failure<CodeScheme>(
                    $"not able to construct all nodes. {invalidOfInputs} {invalidOfActions}");
            }

            var inputNodes = activatedInputNodes
                .Select(ex => ex.Value)
                .ToList();

            var actionNodes = activatedActionNodes
                .Select(ex => ex.Value)
                .ToList();

            return Result.Success(new CodeScheme(actionNodes, inputNodes));
        }
    }
}