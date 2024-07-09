using PacManSimulator.SimulatorClasses.GameEntities.Nodes;

namespace PacManSimulator.SimulatorClasses.RouteCalculators;

public class PacManRouteCalculator
{
    public Node GetNextNode(Node node,Node previousPosition)
    {
        List<Node> neighbors = node.GetNeighbors();
        foreach (Node neighbor in neighbors)
        {
            if (neighbor.HasDot() && neighbor.CanBeOccupiedByPacman())
            {
                return neighbor;
            }
        }

        return GetNodeIfThereAreNoDots(node,previousPosition);
    }

    private Node GetNodeIfThereAreNoDots(Node node, Node previousPosition)
    {
        List<Node> neighbors = node.GetNeighbors();
        foreach (Node neighbor in neighbors)
        {
            if (neighbor.CanBeOccupiedByPacman() && neighbor != previousPosition)
            {
                return neighbor;
            }
        }

        return previousPosition;
    }
    
    
}