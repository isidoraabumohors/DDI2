namespace PacManSimulator.SimulatorClasses.GameEntities.Nodes;

public class WallNode : Node
{
    public override bool CanBeOccupiedByGhosts()
    {
        return false;
    }

    public override bool CanBeOccupiedByPacman()
    {
        return false;
    }
}