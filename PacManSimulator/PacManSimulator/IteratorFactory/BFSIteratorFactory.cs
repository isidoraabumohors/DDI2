using PacManSimulator.Iterators;
using PacManSimulator.SimulatorClasses.Enums;
using PacManSimulator.SimulatorClasses.GameEntities.Nodes;

namespace PacManSimulator.IteratorFactory;

public class BFSIteratorFactory : IFactory
{
    
    //En concreto, tienes que implementar dos m´etodos. El primero es CreateProduct(Node root) dentro de
    // BFSIteratorFactory. Ese m´etodo debe retornar un iterador que ejecute una b´usqueda por amplitud.
    public Iterator CreateProduct(Node root)
    {
        return new BFSIterator(root);
    }
}