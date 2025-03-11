using NoCodeConstructor.Domain.Abstactions;

namespace NoCodeConstructor.Domain.Engine;

public class LocalExecutionContext : IExecutionContext
{
    private int _contextNodeId;

    private IExecutionContext _globalContext;
    
    public LocalExecutionContext(int id, IExecutionContext context)
    {
        _contextNodeId = id;
        _globalContext = context;
    }

    public void SaveValue(string key, string value)
    {
        _globalContext.SaveValue($"{_contextNodeId}./.{key}", value);
    }

    public string GetValue(string key)
    {
        if (key.StartsWith("${"))
        {
            var subkey = key.Trim(['{', '}', '$']);
            return _globalContext.GetValue(subkey);
        }
        
        return _globalContext.GetValue($"{_contextNodeId}./.{key}");
    }

    public IExecutionContext GetSubContext(int id)
    {
        throw new NotImplementedException();
    }
}