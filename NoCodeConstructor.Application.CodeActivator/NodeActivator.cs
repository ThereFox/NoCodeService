using System.Reflection;
using Common;
using CSharpFunctionalExtensions;
using Microsoft.Extensions.DependencyInjection;
using NoCode.Application.Interfaces;
using NoCodeConstructor.Domain.Abstactions;
using NoCodeConstructor.Domain.Scheme.Realisations.Scheme;
using NoCodeConstructor.Domain.Scheme.Realisations.Scheme.Entitys;
using NodeBuilder.Attributes;
using NodeBuilder.DTOs;

namespace NodeBuilder;

public class NodeActivator
{
    private readonly Dictionary<int, NodeActivationConfig> _types = new Dictionary<int, NodeActivationConfig>();
    
    private readonly IServiceProvider _serviceProvider;

    public NodeActivator(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }
    
    public void AppendNewAvaliableNode(List<Type> nodeTypes)
    {
        foreach (var checkingType in nodeTypes)
        {
            if (
                checkingType.GetInterfaces().Contains(typeof(INodeAction)) == false
                &&
                checkingType.GetInterfaces().Contains(typeof(IInputTrigger)) == false
                )
            {
                continue;
            }
            var publicCtors = checkingType
                .GetConstructors();

            if (publicCtors.Count() == 0)
            {
                throw new InvalidCastException($"invalid ctor declaration in type {checkingType.FullName}");
            }

            var notRegistredParameters = publicCtors.First().GetParameters()
                .Where(ex => _serviceProvider.GetService(ex.ParameterType) == default);

            if (notRegistredParameters.Count() > 1)
            {
                throw new InvalidCastException($"invalid ctor declaration in type {checkingType.FullName}");
            }
            
            var typeId = checkingType.GetCustomAttribute<ActionCodeAttribute>().Id;

            if (_types.ContainsKey(typeId))
            {
                throw new InvalidCastException($"Node with typeId {typeId} already exists");
            }

            if (notRegistredParameters.Count() == 1)
            {
                var configType = notRegistredParameters.First().ParameterType;

                _types.Add(typeId, new NodeActivationConfig(configType, checkingType));
            }
            else
            {
                _types.Add(typeId, new NodeActivationConfig(null, checkingType));
            }
        }
        
    }

    public Result<PipelineNode> ActivateNode(NodeConfigInputObject config)
    {
        try
        {
            if (_types.ContainsKey(config.TypeId) == false)
            {
                return Result.Failure<PipelineNode>("Type not supported");
            }

            var typeConfig = _types[config.TypeId];

            INodeAction action;

            if (typeConfig.configurationType == null)
            {
                action = (INodeAction)createObjectWithoutConfig(typeConfig);
            }
            else
            {
                var createObjectResult = createObjectWithConfig(typeConfig, config.Configuration);

                if (createObjectResult.IsFailure)
                {
                    return createObjectResult.ConvertFailure<PipelineNode>();
                }
                
                action = (INodeAction)createObjectResult.Value;
            }
            
            
            var pipe = new Pipe([config.Id], config.ConnectedElements);
            
            var node = PipelineNode.Create(config.Id, action, pipe);
            
            return node;
        }
        catch (Exception e)
        {
            return Result.Failure<PipelineNode>(e.Message);
        }
    }
    
    public Result<InputNode> ActivateInputNode(NodeConfigInputObject config)
    {
        try
        {
            if (_types.ContainsKey(config.TypeId) == false)
            {
                return Result.Failure<InputNode>("Type not supported");
            }

            var typeConfig = _types[config.TypeId];

            IInputTrigger trigger;

            if (typeConfig.configurationType == null)
            {
                trigger = (IInputTrigger)createObjectWithoutConfig(typeConfig);
            }
            else
            {
                var createObjectResult = createObjectWithConfig(typeConfig, config.Configuration);

                if (createObjectResult.IsFailure)
                {
                    return createObjectResult.ConvertFailure<InputNode>();
                }
                
                trigger = (IInputTrigger)createObjectResult.Value;
            }
            

            var pipe = new Pipe([config.Id], config.ConnectedElements);

            var node = InputNode.Create(config.Id, trigger, pipe);
            
            return node;
        }
        catch (Exception e)
        {
            return Result.Failure<InputNode>(e.Message);
        }
    }

    private Result<object> createObjectWithConfig(NodeActivationConfig config, string configData)
    {
        var configParse = ResultJsonDeserialiser.Deserialise(config.configurationType, configData);

        if (configParse.IsFailure)
        {
            return configParse.ConvertFailure<object>();
        }

        return ActivatorUtilities
            .CreateInstance(
                _serviceProvider,
                config.NodeType,
                new Object[] {configParse.Value}
            );
    }
    
    private object createObjectWithoutConfig(NodeActivationConfig config)
    {
        return ActivatorUtilities
            .CreateInstance(
                _serviceProvider,
                config.NodeType
            );
    }
}