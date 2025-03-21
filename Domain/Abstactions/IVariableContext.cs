namespace NoCodeConstructor.Domain.Abstactions;

public interface IVariableContext
{
    public void SaveValue(string key, string value);
    public string GetValue(string key);
    public IVariableContext GetSubContext(int id);
}