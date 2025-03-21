using CSharpFunctionalExtensions;

namespace NoCodeConstructor.Nodes.Interfaces;

public interface IKeyValueStore
{
    public Task<Result<string>> GetValue(string key);
    public Task<Result> SetValue(string key, string value);
}