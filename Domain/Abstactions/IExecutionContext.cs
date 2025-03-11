namespace NoCodeConstructor.Domain.Abstactions;

public interface IExecutionContext
{
    public void SaveValue(string key, string value);
    public string GetValue(string key);
    public IExecutionContext GetSubContext(int id);
}