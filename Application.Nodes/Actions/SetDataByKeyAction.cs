using CSharpFunctionalExtensions;
using NoCodeConstructor.Domain.Abstactions;
using NoCodeConstructor.Domain.Configs;
using NoCodeConstructor.Nodes.Interfaces;
using NodeBuilder.Attributes;
using ExecutionContext = NoCodeConstructor.Domain.DTOs.ExecutionContext;

namespace NoCodeConstructor.Domain.Actions;

[ActionCode(12)]
public class SetDataByKeyAction : INodeAction
{
    private readonly SetDataByKeyConfig _config; 
    private readonly IKeyValueStore _keyValueStore;
    
    
    public SetDataByKeyAction(SetDataByKeyConfig keyConfig, IKeyValueStore store)
    {
        _config = keyConfig;
        _keyValueStore = store;
    }
    public async Task<Result> Handle(ExecutionContext context)
    {
        var actualConfig = context.Configuration.GetConfiguration(_config);
        
        var dataByKey = await _keyValueStore.SetValue(actualConfig.Key, actualConfig.Value);
        
        if (dataByKey.IsFailure)
        {
            return dataByKey;
        }

        return Result.Success();
    }
}