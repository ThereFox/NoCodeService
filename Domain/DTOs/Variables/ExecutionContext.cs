using NoCodeConstructor.Domain.Abstactions;

namespace NoCodeConstructor.Domain.Engine;

public class GlobalVariableContext : IVariableContext
{
    private readonly Dictionary<string, string> _localVariables = new Dictionary<string, string>();

    public GlobalVariableContext()
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
    public IVariableContext GetSubContext(int id)
    {
        return new LocalVariableContext(id, this);
    }
}