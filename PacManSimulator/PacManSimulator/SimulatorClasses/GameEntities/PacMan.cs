using PacManSimulator.SimulatorClasses.GameEntities.Nodes;
using PacManSimulator.SimulatorClasses.RouteCalculators;

namespace PacManSimulator.SimulatorClasses.GameEntities;

public class PacMan
{
    private bool _powerUp = false;
    private PacManRouteCalculator _pacManRouteCalculator = new PacManRouteCalculator();
    private Node _previousPosition;

    public PacMan(Node node)
    {
        _previousPosition = node;
    }
    public void Move(Node node)
    {
        PacMan pacMan = node.GetPacMan();
        Node nextPosition = _pacManRouteCalculator.GetNextNode(node,_previousPosition);
        nextPosition.SetPacMan(pacMan);
        node.RemovePacMan();
        _previousPosition = node;
        if (nextPosition.HasSuperDot())
        {
            _powerUp = true;
        }
        if (nextPosition.HasDot())
        {
            nextPosition.RemoveDot();
        }
    }

    public bool IsPoweredUp()
    {
        return _powerUp;
    }

    public void ResetPowerUp()
    {
        _powerUp = false;
    }
}