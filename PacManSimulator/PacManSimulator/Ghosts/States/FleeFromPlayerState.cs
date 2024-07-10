using PacManSimulator.SimulatorClasses;
using PacManSimulator.SimulatorClasses.Enums;
using PacManSimulator.SimulatorClasses.GameEntities.Nodes;

namespace PacManSimulator.Ghosts.States;

public class FleeFromPlayerState : IState
{
    public Goal GetGoal() => Goal.Base;

    public void UpdateStateWhenPacManIsEncountered(Ghost ghost)
    {
        ghost.TransitionToState(new ReturnToBaseState());
    }

    public void UpdateStateWhenPacManGetsSuperDot(Ghost ghost)
    {
        ghost.TurnsInState = 0;
    }

    public void UpdateStateWhenReachingBase(Ghost ghost)
    {
        ghost.TransitionToState(new WanderAroundBaseState());
    }

    public void UpdateStateAtEndOfCycle(Ghost ghost)
    {
        if (ghost.TurnsInState >= 3)
        {
            ghost.TransitionToState(new SeekPlayerState());
        }
        else
        {
            ghost.TurnsInState++;
        }
    }

    public Node GetNextPosition(NodeCollection route) => route.GetNodeIfGhostMovesOneSpace();
}