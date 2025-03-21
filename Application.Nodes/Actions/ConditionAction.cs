using CSharpFunctionalExtensions;
using NoCodeConstructor.Domain.Abstactions;
using NoCodeConstructor.Domain.Configs;
using NodeBuilder.Attributes;
using ExecutionContext = NoCodeConstructor.Domain.DTOs.ExecutionContext;

namespace NoCodeConstructor.Domain.Actions;

[ActionCode(55555)]
public class ConditionAction : INodeAction
{
    private readonly EqualityConditionConfig _equalityConditionConfig;
    
    public ConditionAction(EqualityConditionConfig config)
    {
        _equalityConditionConfig = config;
    }
    
    public async Task<Result> Handle(ExecutionContext context)
    {
        var filledConfig = context.Configuration.GetConfiguration(_equalityConditionConfig);

        if (filledConfig.Value.Equals(filledConfig.Expected))
        {
            context.OutputePipe.Outputs = [filledConfig.TrueNodeId];
        }
        else
        {
            context.OutputePipe.Outputs = [filledConfig.FalseNodeId];
        }

        return Result.Success();
    }
}