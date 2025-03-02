using NoCodeConstructor.Domain.Engine;
using NoCodeConstructor.Domain.Scheme.Realisations.Pipes;

namespace NoCodeConstructor.Domain.Nodes;

public interface IBreakPointRepairableNode
{
    public Task RunFromBreakPointAsync(BreakPointRestoreContext context);
}