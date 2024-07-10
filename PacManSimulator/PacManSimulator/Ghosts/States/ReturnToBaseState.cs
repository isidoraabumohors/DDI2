using PacManSimulator.SimulatorClasses;
using PacManSimulator.SimulatorClasses.Enums;
using PacManSimulator.SimulatorClasses.GameEntities.Nodes;

namespace PacManSimulator.Ghosts.States;

public class ReturnToBaseState : IState
{
    public Goal GetGoal() => Goal.Base;

    public void UpdateStateWhenPacManIsEncountered(Ghost ghost) { }

    public void UpdateStateWhenPacManGetsSuperDot(Ghost ghost) { }

    public void UpdateStateWhenReachingBase(Ghost ghost)
    {
        ghost.TransitionToState(new WanderAroundBaseState());
    }
    
    public void UpdateStateAtEndOfCycle(Ghost ghost) { }

    public Node GetNextPosition(NodeCollection route) => route.GetNodeIfGhostMovesTwoSpaces();
}