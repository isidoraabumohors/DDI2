namespace PacManSimulator.SimulatorClasses.GameEntities.Nodes;

public class RegularNode: Node
{
    public override bool CanBeOccupiedByGhosts()
    {
        return true;
    }

    public override bool CanBeOccupiedByPacman()
    {
        return true;
    }
}