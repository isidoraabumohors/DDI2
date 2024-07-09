using PacManSimulator.IteratorFactory;
using PacManSimulator.SimulatorClasses;
using PacManSimulator.SimulatorClasses.Enums;
using PacManSimulator.SimulatorClasses.GameEntities.Nodes;
using PacManSimulator.SimulatorClasses.RouteCalculators;

namespace PacManSimulator.Ghosts;

public abstract class AbstractGhost
{
    private readonly GhostRouteCalculator _ghostRouteCalculator;

    protected AbstractGhost(IFactory iteratorFactory)
    {
        _ghostRouteCalculator = new GhostRouteCalculator(iteratorFactory);
    }
    
    public abstract void UpdateStateWhenPacManIsEncountered();
    
    public abstract void UpdateStateWhenPacManGetsSuperDot();
    
    public abstract void UpdateStateWhenReachingBase();

    public abstract void UpdateStateAtEndOfCycle();
    
    public void Move(Node node)
    {
        Goal goal = GetGoal();
        List<Node> nodesVisited = _ghostRouteCalculator.GetNodesVisited(node, goal);
        List<Node> correctRoute = GetCorrectRouteFromNodesVisited(nodesVisited);
        NodeCollection routeAsCollection = new NodeCollection(correctRoute);
        Node nextPosition = GetNextPosition(routeAsCollection);
        if (!nextPosition.HasGhost())
        {
            nextPosition.SetGhost(this);
            node.RemoveGhost();
        }
    }
    
    protected abstract Goal GetGoal();

    private List<Node> GetCorrectRouteFromNodesVisited(List<Node> route)
    {
        List<Node> correctRoute = new List<Node>() {route.Last()};
        Node lastNodeInRoute = route.Last();
        for (int i = route.Count - 1; i >= 0; i--)
        {
            if (NodesAreNeighbors(lastNodeInRoute, route[i]))
            {
                correctRoute.Add(route[i]);
                lastNodeInRoute = route[i];
            }
        }
        
        return correctRoute;
    }

    private bool NodesAreNeighbors(Node firstNode, Node secondNode)
    {
        return firstNode.GetNeighbors().Contains(secondNode);
    }
    
    protected abstract Node GetNextPosition(NodeCollection route);
    
}