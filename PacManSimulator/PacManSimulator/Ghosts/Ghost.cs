using PacManSimulator.Ghosts;
using PacManSimulator.Ghosts.States;
using PacManSimulator.IteratorFactory;
using PacManSimulator.SimulatorClasses;
using PacManSimulator.SimulatorClasses.Enums;
using PacManSimulator.SimulatorClasses.GameEntities.Nodes;

public class Ghost : AbstractGhost
{
    private IState _state;
    public int TurnsInState { get; set; }

    public Ghost(IFactory iteratorFactory) : base(iteratorFactory)
    {
        TransitionToState(new SeekPlayerState());
        TurnsInState = 0;
    }

    public void TransitionToState(IState newState)
    {
        _state = newState;
        TurnsInState = 0;
    }

    protected override Goal GetGoal() => _state.GetGoal();

    public override void UpdateStateWhenPacManIsEncountered()
    {
        _state.UpdateStateWhenPacManIsEncountered(this);
    }

    public override void UpdateStateWhenPacManGetsSuperDot()
    {
        _state.UpdateStateWhenPacManGetsSuperDot(this);
    }

    public override void UpdateStateWhenReachingBase()
    {
        _state.UpdateStateWhenReachingBase(this);
    }

    public override void UpdateStateAtEndOfCycle()
    {
        _state.UpdateStateAtEndOfCycle(this);
    }

    protected override Node GetNextPosition(NodeCollection route)
    {
        return _state.GetNextPosition(route);
    }
}

