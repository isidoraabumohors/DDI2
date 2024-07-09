using PacManSimulator.IteratorFactory;
using PacManSimulator.SimulatorClasses;
using PacManSimulator.SimulatorClasses.Enums;
using PacManSimulator.SimulatorClasses.Exceptions;
using PacManSimulator.SimulatorClasses.GameEntities.Nodes;

namespace PacManSimulator.Ghosts;

public class Ghost : AbstractGhost
{
    private State _state;
    private int _turnsInState;

    public Ghost(IFactory iteratorFactory) : base(iteratorFactory)
    {
        _state = State.SeekPlayer;
        _turnsInState = 0;
    }
    
    protected override Goal GetGoal()
    {
        switch (_state)
        {
            case State.SeekPlayer:
                return Goal.PacMan;
            case State.FleeFromPlayer:
                return Goal.Base;
            case State.ReturnToBase:
                return Goal.Base;
            case State.WanderAroundBase:
                return Goal.Base;
            default:
                throw new Exception();
        }
    }
    
    public override void UpdateStateWhenPacManIsEncountered()
    {
        switch (_state)
        {
            case State.FleeFromPlayer:
                _state = State.ReturnToBase;
                _turnsInState = 0;
                break;
            case State.SeekPlayer:
                throw new PacManIsDeadException();
        }
    }

    public override void UpdateStateWhenPacManGetsSuperDot()
    {
        switch (_state)
        {
            case State.SeekPlayer:
                _state = State.FleeFromPlayer;
                _turnsInState = 0;
                break;
            case State.FleeFromPlayer:
                _turnsInState = 0;
                break;
        }
    }

    public override void UpdateStateWhenReachingBase()
    {
        switch (_state)
        {
            case State.ReturnToBase:
                _state = State.WanderAroundBase;
                _turnsInState = 0;
                break;
            case State.FleeFromPlayer:
                _state = State.WanderAroundBase;
                _turnsInState = 0;
                break;
        }
    }

    public override void UpdateStateAtEndOfCycle()
    {
        switch (_state)
        {
            case State.FleeFromPlayer:
                if (_turnsInState == 3)
                {
                    _state = State.SeekPlayer;
                    _turnsInState = 0;
                }
                else _turnsInState++;
                break;
            case State.WanderAroundBase:
                if (_turnsInState == 2)
                {
                    _state = State.SeekPlayer;
                    _turnsInState = 0;
                }
                else _turnsInState++;
        
                break;
            case State.SeekPlayer:
                if (_turnsInState == 10)
                {
                    _state = State.GetAngry;
                    _turnsInState = 0;
                }
                else _turnsInState++;
                break;
        }
    }

    protected override Node GetNextPosition(NodeCollection route)
    {
        switch (_state)
        {
            case State.SeekPlayer:
                return route.GetNodeIfGhostMovesOneSpace();
            case State.FleeFromPlayer:
                return route.GetNodeIfGhostMovesOneSpace();
            case State.WanderAroundBase:
                return route.GetNodeIfGhostDoesNotMove();
            case State.ReturnToBase:
                return route.GetNodeIfGhostMovesTwoSpaces();
            default:
                throw new Exception();
        }
    }
}