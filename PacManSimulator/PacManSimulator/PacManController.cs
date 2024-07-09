using PacManSimulator.IteratorFactory;
using PacManSimulator.Iterators;
using PacManSimulator.SimulatorClasses.Enums;
using PacManSimulator.SimulatorClasses.GameEntities.Nodes;

namespace PacManSimulator;

public static class PacManController
{
    
    public static List<Node> GetNodesVisited(Node root, Goal goal, IFactory iteratorFactory)
    {
        List<Node> visitedNodes = new List<Node>();
        Iterator iterator = iteratorFactory.CreateProduct(root);

        while (iterator.HasMore())
        {
            Node node = iterator.GetNext();
            visitedNodes.Add(node);

            if ((goal == Goal.Base && node.IsBase()) || (goal == Goal.PacMan && node.HasPacMan()))
            {
                break;
            }
        }

        return visitedNodes;
    }
    
    public static IFactory GetAvoidPacManBFSFactory()
    {
        throw new NotImplementedException();
    }
}