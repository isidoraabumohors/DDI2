using PacManSimulator.SimulatorClasses.GameEntities.Nodes;

namespace PacManSimulator.Iterators;

public class BFSIterator : Iterator
{
    private readonly Queue<Node> _queue = new Queue<Node>();
    private readonly HashSet<Node> _alreadyVisitedNodes = new HashSet<Node>();

    public BFSIterator(Node root)
    {
        _queue.Enqueue(root);
    }
    
    public Node GetNext()
    {
        Node node = _queue.Dequeue();
        _alreadyVisitedNodes.Add(node);
        foreach (Node neighbor in node.GetNeighbors())
        {
            if (neighbor.CanBeOccupiedByGhosts() && !_alreadyVisitedNodes.Contains(neighbor))
            {
                _queue.Enqueue(neighbor);
                _alreadyVisitedNodes.Add(neighbor);
            }
        }
        return node;
    }

    public bool HasMore()
    {
        return _queue.Any();
    }
}