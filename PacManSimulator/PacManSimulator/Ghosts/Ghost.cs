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

/*
Parte C: Implementa el patr´on state (1 punto)
La clase Ghost tiene la l´ogica que controla el comportamiento de los fantasmas. Pero su implementaci´on se
basa en muchos switchs duplicados. Tu objetivo es mejorar esta implementaci´on utilizando el patr´on state.
Actualmente, el comportamiento del fantasma est´a modelado utilizando 4 estados posibles:
SeekPlayer: En este estado el objetivo es Pac-Man. Si Pac-Man es encontrado, Pac-Man muere.
Adem´as, el fantasma avanza un solo espacio por turno. Si Pac-Man come un super punto el estado
cambia a FleeFromPlayer. Si el fantasma lleva 10 turnos seguidos en este estado, en el turno 11 cambia
al estado GetAngry (ver parte D).
FleeFromPlayer: En este estado el objetivo es volver a la base. En cada turno, el fantasma avanza un
espacio. Si encuentra a Pac-Man, Pac-Man no muere (pues acaba de comer un super punto) y el estado
cambia a ReturnToBase. Si se llega a la base, el estado cambia a WanderAroundBase. Si han pasado 3
iteraciones desde que Pac-Man comi´o el super punto, en la iteraci´on 4 el estado cambia a SeekPlayer.
Pero si Pac-Man vuelve a comer un super punto, entonces el contador se reseta.
ReturnToBase: En este estado Pac-Man acaba de comer al fantasma, por lo que debe volver a la base
para revivir. En este estado, el objetivo es volver a la base. Para ello, el fantasma avanza dos casillas
por turno. Encontrar a Pac-Man no cambia el estado ni mata a Pac-Man. Llegar a la base hace que el
estado cambie a WanderAroundBase.
2
WanderAroundBase: En este estado, el fantasma ya se encuentra en la base y se mantiene ah´ı, sin
moverse, por 2 iteraciones. En la tercera iteraci´on el estado cambia a SeekPlayer.
La implementaci´on actual consta de 6 m´etodos principales. GetGoal() retorna el objetivo actual del fantasma.
El m´etodo GetNextPosition(...) define cu´antos espacios se mueve el fantasma en cada iteraci´on. Luego
hay 4 m´etodos UpdateStateWhen... que actualizan el estado seg´un lo que ocurre en el juego. Adem´as, el
m´etodo UpdateStateWhenPacManIsEncountered() lanza una excepci´on si es que Pac-Man muere.
Si al implementar state no rompiste nada, deber´ıas seguir pasando todos los test cases de la parte C.*/

// Respondeme to do en un solo trozo de codigo, por favor. Gracias.