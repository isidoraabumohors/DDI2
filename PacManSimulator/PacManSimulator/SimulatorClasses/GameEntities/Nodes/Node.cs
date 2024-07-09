using PacManSimulator.Ghosts;

namespace PacManSimulator.SimulatorClasses.GameEntities.Nodes;

public abstract class Node
{
    private Enums.Dot _dot = Enums.Dot.Void;
    private AbstractGhost? _ghost = null;
    private PacMan? _pacMan = null;
    private List<Node> _neighbors = new List<Node>() { };

    public void SetDot(Enums.Dot dotType)
    {
        _dot = dotType;
    }

    public void SetGhost(AbstractGhost abstractGhost)
    {
        _ghost = abstractGhost;
    }

    public void SetPacMan(PacMan pacMan)
    {
        _pacMan = pacMan;
    }

    public void RemoveGhost()
    {
        _ghost = null;
    }

    public void RemovePacMan()
    {
        _pacMan = null;
    }

    public void RemoveDot()
    {
        _dot = Enums.Dot.Void;
    }

    public void AddNeighbor(Node neighbor)
    {
        _neighbors.Add(neighbor);
    }

    public bool HasPacMan()
    {
        return _pacMan != null;
    }

    public bool HasGhost()
    {
        return _ghost != null;
    }

    public bool HasDot()
    {
        return _dot != Enums.Dot.Void;
    }

    public bool HasSuperDot()
    {
        return _dot == Enums.Dot.Super;
    }

    public List<Node> GetNeighbors()
    {
        return _neighbors;
    }

    public AbstractGhost GetGhost()
    {
        return _ghost;
    }

    public PacMan GetPacMan()
    {
        return _pacMan;
    }

    public virtual bool IsBase()
    {
        return false;
    }
    
    public abstract bool CanBeOccupiedByGhosts();

    public abstract bool CanBeOccupiedByPacman();

    public int NodeId { get; set; }
}