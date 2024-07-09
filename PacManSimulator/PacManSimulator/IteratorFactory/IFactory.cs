using PacManSimulator.Iterators;
using PacManSimulator.SimulatorClasses.Enums;
using PacManSimulator.SimulatorClasses.GameEntities.Nodes;

namespace PacManSimulator.IteratorFactory;

public interface IFactory
{
    Iterator CreateProduct(Node root, Goal goal);
}