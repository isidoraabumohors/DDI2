using PacManSimulator.SimulatorClasses;
using PacManSimulator.SimulatorClasses.Enums;
using PacManSimulator.SimulatorClasses.Exceptions;
using PacManSimulator.SimulatorClasses.GameEntities.Nodes;

namespace PacManSimulator.Ghosts.States;

public class GetAngryState : IState
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

    public void UpdateStateWhenReachingBase(Ghost ghost)
    { }

    public void UpdateStateAtEndOfCycle(Ghost ghost)
    {
        ghost.TurnsInState++;
    }

    public Node GetNextPosition(NodeCollection route) => route.GetNodeIfGhostMovesTwoSpaces();
}

