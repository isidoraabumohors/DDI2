using PacManSimulator.SimulatorClasses;
using PacManSimulator.SimulatorClasses.Enums;
using PacManSimulator.SimulatorClasses.GameEntities.Nodes;

namespace PacManSimulator.Ghosts.States;

public class WanderAroundBaseState : IState
{
    public Goal GetGoal() => Goal.Base;

    public void UpdateStateWhenPacManIsEncountered(Ghost ghost) { }

    public void UpdateStateWhenPacManGetsSuperDot(Ghost ghost) { }

    public void UpdateStateWhenReachingBase(Ghost ghost) { }

    public void UpdateStateAtEndOfCycle(Ghost ghost)
    {
        if (ghost.TurnsInState >= 2)
        {
            ghost.TransitionToState(new SeekPlayerState());
        }
        else
        {
            ghost.TurnsInState++;
        }
    }

    public Node GetNextPosition(NodeCollection route) => route.GetNodeIfGhostDoesNotMove();
}