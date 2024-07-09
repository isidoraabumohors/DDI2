using PacManSimulator.IteratorFactory;
using PacManSimulator.SimulatorClasses.Enums;
using PacManSimulator.SimulatorClasses.GameEntities.Nodes;

namespace PacManSimulator.SimulatorClasses.RouteCalculators;

public class GhostRouteCalculator
{
    private IFactory _iteratorFactory;

    public GhostRouteCalculator(IFactory iteratorFactory)
    {
        _iteratorFactory = iteratorFactory;
    }

    public List<Node> GetNodesVisited(Node root, Goal goal)
    {
        return PacManController.GetNodesVisited(root, goal, _iteratorFactory);
    }
    
}