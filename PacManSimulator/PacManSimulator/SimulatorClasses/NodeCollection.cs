using PacManSimulator.SimulatorClasses.GameEntities.Nodes;

namespace PacManSimulator.SimulatorClasses;

public class NodeCollection
{
    private List<Node> _nodes;

    public NodeCollection(List<Node> nodes)
    {
        _nodes = nodes;
    }

    public Node GetNodeIfGhostMovesOneSpace()
    {
        if (_nodes.Count == 1) return _nodes.Last();
        return _nodes[_nodes.Count - 2];
    }

    public Node GetNodeIfGhostDoesNotMove()
    {
        return _nodes.Last();
    }

    public Node GetNodeIfGhostMovesTwoSpaces()
    {
        if (_nodes.Count == 2) return _nodes[_nodes.Count - 2];
        if (_nodes.Count == 1) return _nodes.Last();
        return _nodes[_nodes.Count - 3];
    }
}