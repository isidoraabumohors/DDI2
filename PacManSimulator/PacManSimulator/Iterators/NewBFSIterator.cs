using PacManSimulator.SimulatorClasses.Enums;
using PacManSimulator.SimulatorClasses.GameEntities.Nodes;

namespace PacManSimulator.Iterators;

public class AvoidPacManBFSIterator : Iterator
{
    private readonly Queue<Node> _queue = new Queue<Node>();
    private readonly HashSet<Node> _alreadyVisitedNodes = new HashSet<Node>();
    private readonly Goal _goal;

    public AvoidPacManBFSIterator(Node root, Goal goal)
    {
        _queue.Enqueue(root);
        _goal = goal;
    }

    public Node GetNext()
    {
        while (_queue.Any())
        {
            Node node = _queue.Dequeue();
            _alreadyVisitedNodes.Add(node);

            foreach (Node neighbor in node.GetNeighbors())
            {
                if (neighbor.CanBeOccupiedByGhosts() && !_alreadyVisitedNodes.Contains(neighbor))
                {
                    if (_goal == Goal.Base && neighbor.HasPacMan())
                    {
                        continue;
                    }

                    _queue.Enqueue(neighbor);
                    _alreadyVisitedNodes.Add(neighbor);
                }
            }

            if (_goal != Goal.Base || !node.HasPacMan())
            {
                return node;
            }
        }
        return null; 
    }

    public bool HasMore()
    {
        return _queue.Any();
    }
}