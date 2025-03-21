using CSharpFunctionalExtensions;
using NoCodeConstructor.Domain.Abstactions;
using NoCodeConstructor.Domain.Configs;
using NodeBuilder.Attributes;
using ExecutionContext = NoCodeConstructor.Domain.DTOs.ExecutionContext;

namespace NoCodeConstructor.Domain.Actions;

[ActionCode(6)]
public class SetVariableAction : INodeAction
{
    private readonly SetVariableConfig _config;
    
    public SetVariableAction(SetVariableConfig config)
    {
        _config = config;
    }
    
    public Task<Result> Handle(ExecutionContext context)
    {
        var filledConfig = context.Configuration.GetConfiguration(_config);
        
        context.VariableContext.SaveValue(filledConfig.VariableName, filledConfig.VariableValue);
        
        
        
        return Task.FromResult(Result.Success()); 
    }
}