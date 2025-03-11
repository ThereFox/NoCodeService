using CSharpFunctionalExtensions;
using NoCodeConstructor.Domain.Engine;
using NoCodeConstructor.Domain.Scheme.Realisations.Scheme.Entitys;

namespace NoCodeConstructor.Domain.Scheme.Realisations.Scheme;

public class CodeScheme : Entity<Guid>
{
    private List<PipelineNode> _nodes;
    private List<InputNode> _inputs;

    public IReadOnlyCollection<PipelineNode> Nodes => _nodes;
    public IReadOnlyCollection<InputNode> Inputs => _inputs;

    private readonly HashSet<PipelineNode> _visitedNodes = new();
    private readonly Queue<PipelineNode> _executionQueue = new();

    private CodeScheme(Guid id, List<PipelineNode> nodes, List<InputNode> inputs)
    {
        Id = id;
        _nodes = nodes;
        _inputs = inputs;
    }

    public async Task<Result> HandleEvent(EventInfo iniciator)
    {
        var executionContext = new GlobalContext();
        
        var triggeredNodes = _inputs.Select(ex => ex.IsTriggering(iniciator, executionContext))
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

            var executeResult = await current.Execute(executionContext.GetSubContext(current.Id));

            if (executeResult.IsFailure)
            {
                //catch
                return Result.Failure(executeResult.Error);
            }

            AppendNewElementsToExecuteQueue(current.OutputPipe);

            _visitedNodes.Add(current);
        }


        return Result.Success();
    }

    private void AppendNewElementsToExecuteQueue(Pipe outputPipe)
    {
        _nodes.Where(ex => outputPipe.Outputs.Contains(ex.Id))
            .ToList()
            .ForEach(ex => _executionQueue.Enqueue(ex));
    }

    public static Result<CodeScheme> Create(List<PipelineNode> nodes, List<InputNode> inputs)
    {
        if (inputs.Count == 0)
        {
            return Result.Failure<CodeScheme>("No inputs specified");
        }

        if (inputs.Any(ex => ex.OutputPipe.Outputs.Count == 0))
        {
            return Result.Failure<CodeScheme>("Has separated input");
        }

        if (allElementsReachable(nodes, inputs) == false)
        {
            return Result.Failure<CodeScheme>("not all elements reachable");
        }

        return Result.Success(new CodeScheme(Guid.NewGuid(), nodes, inputs));
    }

    private static bool allElementsReachable(List<PipelineNode> nodes, List<InputNode> inputs)
    {
        var checkedNodes = new HashSet<int>();
        var checkQueue = new Queue<PipelineNode>();

        foreach (var input in inputs)
        {
            var connected = input.OutputPipe.Outputs;

            connected.ForEach(ex =>
            {
                checkQueue.Enqueue(nodes.Where(subex => subex.Id == ex).First());
            });
        }

        while (checkQueue.Count() != 0)
        {
            var node = checkQueue.Dequeue();
            if (checkedNodes.Contains(node.Id))
            {
                continue;
            }

            checkedNodes.Add(node.Id);
            var connected = node.OutputPipe.Outputs;

            connected.ForEach(ex =>
            {
                checkQueue.Enqueue(nodes.Where(subex => subex.Id == ex).First());
            });
        }

        return checkedNodes.Count == nodes.Count;
    }
}