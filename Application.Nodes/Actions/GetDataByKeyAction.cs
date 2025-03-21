using CSharpFunctionalExtensions;
using NoCodeConstructor.Domain.Abstactions;
using NoCodeConstructor.Domain.Configs;
using NoCodeConstructor.Nodes.Interfaces;
using NodeBuilder.Attributes;
using ExecutionContext = NoCodeConstructor.Domain.DTOs.ExecutionContext;

namespace NoCodeConstructor.Domain.Actions;

[ActionCode(11)]
public class GetDataByKeyAction : INodeAction
{
    private readonly GetDataByKeyConfig _config;
    private readonly IKeyValueStore _keyValueStore;
    
    
    public GetDataByKeyAction(GetDataByKeyConfig keyConfig, IKeyValueStore store)
    {
        _config = keyConfig;
        _keyValueStore = store;
    }
    public async Task<Result> Handle(ExecutionContext context)
    {
        var dataByKey = await _keyValueStore.GetValue(_config.Key);
        if (dataByKey.IsFailure)
        {
            return dataByKey;
        }
        
        context.VariableContext.SaveValue(_config.Name, dataByKey.Value);

        return Result.Success();
    }
}