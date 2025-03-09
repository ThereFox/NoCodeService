using System.Reflection;
using Common;
using CSharpFunctionalExtensions;
using NoCode.Application.Interfaces;
using NoCodeConstructor.Domain.Nodes;
using NodeBuilder.DTOs;

namespace NodeBuilder;

public class NodeActivator : INodeActivator
{
    private readonly Dictionary<int, NodeActivationConfig> _types = new Dictionary<int, NodeActivationConfig>();
    
    public void AppendNewAvaliableNode(List<Type> nodeTypes)
    {
        
    }

    public Result<INode> ActivateNode(NodeConfigInputObject config)
    {
        if (_types.ContainsKey(config.TypeId) == false)
        {
            return Result.Failure<INode>("Type not supported");
        }
        
        var typeConfig = _types[config.TypeId];
        
        var configParse = ResultJsonDeserialiser.Deserialise(type)
        
        var instanse = Activator.CreateInstance(
            type.NodeType, BindingFlags.Public, null,
            new Object[ config.Id ]);
        
    }
}