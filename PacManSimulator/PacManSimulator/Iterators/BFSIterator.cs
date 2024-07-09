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

/*Una debilidad de BFSIterator es que cuando el objetivo es ir a la base, el fantasma se puede suicidar.
    Esto ocurre porque el fantasma va a la base para escapar de Pac-Man cuando come un s´uper punto. Pero
    BFSIterator puede proponer rutas que pasan por Pac-Man (haciendo que el fantasma se suicide).
    Implementa un nuevo iterador que funcione igual que BFSIterator pero si el objetivo es Goal.Base, entonces
    no explore rutas que pasan por Pac-Man. Por otro lado, si el objetivo es Goal.PacMan el iterador debe
funcionar igual que BFSIterator.
    Luego implementa GetAvoidPacManBFSFactory() dentro de PacManController. Ese m´etodo debe retornar
    un factory que cree el nuevo iterador. En los tests, damos esa factory al m´etodo GetNodesVisited(Node
    root, Goal goal, IFactory iteratorFactory) para testear el funcionamiento del nuevo iterador.
    Por ejemplo, en el mapa de la figura, el nuevo iterador debe retornar “11, 6, 12, 1, 7, 13, 2, 8” cuando el
objetivo es la base. Notar que esta secuencia no termina en la base debido a que no hay forma de llegar a
    ella sin pasar por Pac-Man. Si el objetivo es Pac-Man, el iterador debe retornar lo mismo que en la parte A.
    Hint: En cada llamado a GetNext(), BFSIterator agrega los nodos que quiere explorar a queue. Para que
no explore rutas que incluyen a Pac-Man basta con no agregar nodos que incluyan a Pac-Man a queue.*/

