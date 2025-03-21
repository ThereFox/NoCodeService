using NoCodeConstructor.Domain.Abstactions;

namespace NoCodeConstructor.Domain.Engine;

public class LocalVariableContext : IVariableContext
{
    private int _contextNodeId;

    private IVariableContext _globalContext;
    
    public LocalVariableContext(int id, IVariableContext context)
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

    public IVariableContext GetSubContext(int id)
    {
        throw new NotImplementedException();
    }
}