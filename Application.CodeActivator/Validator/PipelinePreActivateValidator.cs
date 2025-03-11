using CSharpFunctionalExtensions;
using NoCodeConstructor.Domain.Scheme.Realisations.Scheme;
using NodeBuilder.DTOs;

namespace NodeBuilder.Validator;

public class PipelinePreActivateValidator
{
    public Result ChackSchemeValidity(List<NodeConfigInputObject> scheme)
    {
        if (scheme.Any() == false)
        {
            return Result.Failure("empty scheme");
        }

        var ids = scheme.Select(ex => ex.Id).DistinctBy(ex => ex);
        
        if (ids.Count() != scheme.Count)
        {
            return Result.Failure("duplicate ids");
        }
        
        foreach (var node in scheme)
        {
            if (node.ConnectedElements.All(ex => ids.Contains(ex)) == false)
            {
                return Result.Failure($"not all connected elements are exists");
            }

            if (node.ConnectedElements.Any(ex => ex == node.Id))
            {
                return Result.Failure($"node {node.Id} have link to himself");
            }
        }
        
        return Result.Success();
    }
}