using PacManSimulator.Iterators;
using PacManSimulator.SimulatorClasses.Enums;
using PacManSimulator.SimulatorClasses.GameEntities.Nodes;

namespace PacManSimulator.IteratorFactory;
public class AvoidPacManBFSIteratorFactory : IFactory
{
    public Iterator CreateProduct(Node root, Goal goal)
    {
        return new AvoidPacManBFSIterator(root, goal);
    }
}




