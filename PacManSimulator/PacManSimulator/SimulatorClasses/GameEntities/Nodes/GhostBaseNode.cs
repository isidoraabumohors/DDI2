namespace PacManSimulator.SimulatorClasses.GameEntities.Nodes;

public class GhostBaseNode:Node
{
    public override bool CanBeOccupiedByGhosts()
    {
        return true;
    }

    public override bool CanBeOccupiedByPacman()
    {
        return false;
    }

    public override bool IsBase()
    {
        return true;
    }
}