using PacManSimulator.SimulatorClasses;
using PacManSimulator.SimulatorClasses.Enums;
using PacManSimulator.SimulatorClasses.Exceptions;
using PacManSimulator.SimulatorClasses.GameEntities.Nodes;

namespace PacManSimulator.Ghosts.States;

public class SeekPlayerState : IState
{
    public Goal GetGoal() => Goal.PacMan;

    public void UpdateStateWhenPacManIsEncountered(Ghost ghost)
    {
        throw new PacManIsDeadException();
    }

    public void UpdateStateWhenPacManGetsSuperDot(Ghost ghost)
    {
        ghost.TransitionToState(new FleeFromPlayerState());
    }

    public void UpdateStateWhenReachingBase(Ghost ghost) { }

    public void UpdateStateAtEndOfCycle(Ghost ghost)
    {
        if (ghost.TurnsInState >= 10)
        {
            ghost.TransitionToState(new GetAngryState());
        }
        else
        {
            ghost.TurnsInState++;
        }
    }

    public Node GetNextPosition(NodeCollection route) => route.GetNodeIfGhostMovesOneSpace();
}