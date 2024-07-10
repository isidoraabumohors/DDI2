using PacManSimulator.SimulatorClasses;
using PacManSimulator.SimulatorClasses.Enums;
using PacManSimulator.SimulatorClasses.GameEntities.Nodes;

namespace PacManSimulator.Ghosts;

public interface IState
{
    Goal GetGoal();
    void UpdateStateWhenPacManIsEncountered(Ghost ghost);
    void UpdateStateWhenPacManGetsSuperDot(Ghost ghost);
    void UpdateStateWhenReachingBase(Ghost ghost);
    void UpdateStateAtEndOfCycle(Ghost ghost);
    Node GetNextPosition(NodeCollection route);
}