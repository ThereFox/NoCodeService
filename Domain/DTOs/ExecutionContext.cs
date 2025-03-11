using NoCodeConstructor.Domain.Abstactions;

namespace NoCodeConstructor.Domain.Engine;

public class GlobalContext : IExecutionContext
{
    private readonly Dictionary<string, string> _localVariables = new Dictionary<string, string>();

    public GlobalContext()
    {
        
    }

    public void SaveValue(string key, string value)
    {
        _localVariables[key] = value;
    }

    public string GetValue(string key)
    {
        return _localVariables.TryGetValue(key, out var value) ? value : null;  
    }
    public IExecutionContext GetSubContext(int id)
    {
        return new LocalExecutionContext(id, this);
    }
}