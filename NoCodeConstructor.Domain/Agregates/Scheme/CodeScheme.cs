using CSharpFunctionalExtensions;
using NoCodeConstructor.Domain.Engine;
using NoCodeConstructor.Domain.Scheme.Realisations.Scheme.Entitys;

namespace NoCodeConstructor.Domain.Scheme.Realisations.Scheme;

public class CodeScheme : Entity
{
    private List<PipelineNode> _nodes;
    private List<InputNode> _inputs;

    public IReadOnlyCollection<PipelineNode> Nodes => _nodes;
    public IReadOnlyCollection<InputNode> Inputs => _inputs;

    private readonly HashSet<PipelineNode> _visitedNodes = new();
    private readonly Queue<PipelineNode> _executionQueue = new();

    public CodeScheme(List<PipelineNode> nodes, List<InputNode> inputs)
    {
        _nodes = nodes;
        _inputs = inputs;
    }

    public async Task<Result> HandleEvent(EventInfo iniciator)
    {
        var triggeredNodes = _inputs.Select(ex => ex.IsTriggering(iniciator))
            .Select((ex, index) => ex.IsSuccess && ex.Value ? index : -1)
            .Where(ex => ex != -1)
            .Select(ex => _inputs[ex]);

        if (triggeredNodes.Any() == false)
        {
            return Result.Success();
        }

        foreach (var node in triggeredNodes)
        {
            var connectedNodes = node.OutputPipe.Outputs;

            _nodes.Where(ex => connectedNodes.Contains(ex.Id))
                .ToList()
                .ForEach(ex => _executionQueue.Enqueue(ex));
        }

        while (_executionQueue.Any())
        {
            var current = _executionQueue.Dequeue();

            if (_visitedNodes.Contains(current))
            {
                continue;
            }

            var executeResult = await current.Execute(null);

            if (executeResult.IsFailure)
            {
                //catch
                return Result.Failure(executeResult.Error);
            }

            AppendNewElementsToExecuteQueue(current.OutputPipe);

            _visitedNodes.Add(current);
        }


        return Result.Failure("not realised");
    }

    private void AppendNewElementsToExecuteQueue(Pipe outputPipe)
    {
        _nodes.Where(ex => outputPipe.Outputs.Contains(ex.Id))
            .ToList()
            .ForEach(ex => _executionQueue.Enqueue(ex));
    }
}