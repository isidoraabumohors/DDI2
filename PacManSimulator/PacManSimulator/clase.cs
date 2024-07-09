/*
namespace PacManSimulator;

public class clase
{
    
}

public interface IGhostState
{
    Goal GetGoal();
    void UpdateStateWhenPacManIsEncountered(Ghost ghost);
    void UpdateStateWhenPacManGetsSuperDot(Ghost ghost);
    void UpdateStateWhenReachingBase(Ghost ghost);
    void UpdateStateAtEndOfCycle(Ghost ghost);
    Node GetNextPosition(NodeCollection route);
}


public class SeekPlayerState : IGhostState
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
    {
        // No action needed
    }

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

public class FleeFromPlayerState : IGhostState
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

public class ReturnToBaseState : IGhostState
{
    public Goal GetGoal() => Goal.Base;

    public void UpdateStateWhenPacManIsEncountered(Ghost ghost)
    {
        // No action needed
    }

    public void UpdateStateWhenPacManGetsSuperDot(Ghost ghost)
    {
        // No action needed
    }

    public void UpdateStateWhenReachingBase(Ghost ghost)
    {
        ghost.TransitionToState(new WanderAroundBaseState());
    }

    public void UpdateStateAtEndOfCycle(Ghost ghost)
    {
        // No action needed
    }

    public Node GetNextPosition(NodeCollection route) => route.GetNodeIfGhostMovesTwoSpaces();
}

public class WanderAroundBaseState : IGhostState
{
    public Goal GetGoal() => Goal.Base;

    public void UpdateStateWhenPacManIsEncountered(Ghost ghost)
    {
        // No action needed
    }

    public void UpdateStateWhenPacManGetsSuperDot(Ghost ghost)
    {
        // No action needed
    }

    public void UpdateStateWhenReachingBase(Ghost ghost)
    {
        // No action needed
    }

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

public class GetAngryState : IGhostState
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
    {
        // No action needed
    }

    public void UpdateStateAtEndOfCycle(Ghost ghost)
    {
        ghost.TurnsInState++;
    }

    public Node GetNextPosition(NodeCollection route) => route.GetNodeIfGhostMovesTwoSpaces();
}

public class Ghost : AbstractGhost
{
    private IGhostState _state;
    public int TurnsInState { get; private set; }

    public Ghost(IFactory iteratorFactory) : base(iteratorFactory)
    {
        TransitionToState(new SeekPlayerState());
        TurnsInState = 0;
    }

    public void TransitionToState(IGhostState newState)
    {
        _state = newState;
        TurnsInState = 0;
    }

    protected override Goal GetGoal() => _state.GetGoal();

    public override void UpdateStateWhenPacManIsEncountered() => _state.UpdateStateWhenPacManIsEncountered(this);

    public override void UpdateStateWhenPacManGetsSuperDot() => _state.UpdateStateWhenPacManGetsSuperDot(this);

    public override void UpdateStateWhenReachingBase() => _state.UpdateStateWhenReachingBase(this);

    public override void UpdateStateAtEndOfCycle() => _state.UpdateStateAtEndOfCycle(this);

    protected override Node GetNextPosition(NodeCollection route) => _state.GetNextPosition(route);
}
*/
