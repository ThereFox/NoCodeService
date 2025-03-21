using CSharpFunctionalExtensions;
using NoCodeConstructor.Domain.Abstactions;
using NodeBuilder.Attributes;

namespace NoCodeConstructor.Domain.Actions;

[ActionCode(11)]
public class GetTelegramCredentials : INodeAction
{
    public GetTelegramCredentials()
    {
        
    }
    public Task<Result> Handle(IExecutionContext context)
    {
        throw new NotImplementedException();
    }
}