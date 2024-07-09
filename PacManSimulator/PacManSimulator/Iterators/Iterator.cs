using PacManSimulator.SimulatorClasses.GameEntities.Nodes;

namespace PacManSimulator.Iterators;

public interface Iterator
{
    Node GetNext();

    bool HasMore();
}