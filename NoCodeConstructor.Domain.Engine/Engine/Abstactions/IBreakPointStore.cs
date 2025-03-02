namespace NoCodeConstructor.Domain.Engine.Abstactions;

public interface IBreakPointStore
{
    public Task<object> CreateBreakPointAsync();
}