using PacManSimulator.Ghosts;
using PacManSimulator.IteratorFactory;
using PacManSimulator.SimulatorClasses;
using PacManSimulator.SimulatorClasses.Enums;
using PacManSimulator.SimulatorClasses.Exceptions;
using PacManSimulator.SimulatorClasses.GameEntities.Nodes;

namespace PacManSimulator.Tests;

public class GhostTests() : Ghost(new BFSIteratorFactory())
{
    [Fact]
    public void PartC_State_SeekPlayerStateShouldFollowPacMan()
    {
        // Se parte en el estado SeekPlayer
        UpdateStateWhenReachingBase(); // No debería hacer nada en SeekPlayer
        Assert.Equal(Goal.PacMan, GetGoal()); // El objetivo en SeekPlayer es PacMan
    }

    [Fact]
    public void PartC_State_SeekPlayerStateShouldKillPacManWhenPacManIsEncountered()
        => Assert.Throws<PacManIsDeadException>(UpdateStateWhenPacManIsEncountered); // al matar a pacman se debería tirar esta excepción

    [Fact]
    public void PartC_State_SeekPlayerStateShouldMoveOnlyOneSpaceAtTheTime()
        => AssertThatGhostMovesOnlyOneSpace();

    private void AssertThatGhostMovesOnlyOneSpace()
    {
        List<Node> nodes = [new RegularNode(), new RegularNode(), new RegularNode(), new RegularNode()];
        NodeCollection route = new NodeCollection(nodes);
        Node expected = route.GetNodeIfGhostMovesOneSpace();
        Node actual = GetNextPosition(route);
        Assert.Equal(expected, actual);
    }

    [Fact]
    public void PartC_State_FleeFromPlayerStateShouldGoToBase()
    {
        UpdateStateWhenPacManGetsSuperDot(); // mueve a FleeFromPlayer
        Assert.Equal(Goal.Base, GetGoal()); // el objetivo en FleeFromPlayer es la base
    }

    [Fact]
    public void PartC_State_FleeFromPlayerStateShouldMoveOnlyOneSpaceAtTheTime()
    {
        UpdateStateWhenPacManGetsSuperDot(); // mueve a FleeFromPlayer
        AssertThatGhostMovesOnlyOneSpace();
    }
    
    [Fact]
    public void PartC_State_FleeFromPlayerStateMovesToSeekPacManAfterFourIterations()
    {
        UpdateStateWhenPacManGetsSuperDot(); // mueve a FleeFromPlayer
        for(int i = 0; i < 4; i++) // aumenta el iterador 4 veces, haciendo que el estado vuelva a SeekPlayer
            UpdateStateAtEndOfCycle();
        Assert.Equal(Goal.PacMan, GetGoal()); // en SeekPlayer el objetivo es PacMan
    }
    
    [Fact]
    public void PartC_State_FleeFromPlayerStateIterationsCounterResetsWhenPacManGetsSuperDot()
    {
        UpdateStateWhenPacManGetsSuperDot(); // mueve a FleeFromPlayer  
        UpdateStateAtEndOfCycle(); // aumenta el iterador
        UpdateStateWhenPacManGetsSuperDot(); // resetea el iterador
        for(int i = 0; i < 3; i++) // aumenta el iterador 3 veces (que no basta para cambiar de estado)
            UpdateStateAtEndOfCycle(); 
        Assert.Equal(Goal.Base, GetGoal()); // el objetivo de FleeFromPlayer es la base
    }

    [Fact]
    public void PartC_State_ReturnToBaseStateShouldGoToBase()
    {
        UpdateStateWhenPacManGetsSuperDot(); // mueve a FleeFromPlayer
        UpdateStateWhenPacManIsEncountered(); // mueve a ReturnToBase
        UpdateStateWhenPacManGetsSuperDot(); // No debería pasar nada
        UpdateStateWhenPacManIsEncountered(); // No debería pasar nada
        UpdateStateAtEndOfCycle(); // No debería pasar nada
        Assert.Equal(Goal.Base, GetGoal()); // el objetivo es ir a la base desde ReturnToBase
    }

    [Fact]
    public void PartC_State_ReturnToBaseStateMovesTwoSpacesAtTheTime()
    {
        UpdateStateWhenPacManGetsSuperDot(); // mueve a FleeFromPlayer
        UpdateStateWhenPacManIsEncountered(); // mueve a ReturnToBase
        AssertThatGhostMovesTwoSpaces(); // en este estado el fantasma se mueve dos espacios a la vez
    }
    
    private void AssertThatGhostMovesTwoSpaces()
    {
        List<Node> nodes = [new RegularNode(), new RegularNode(), new RegularNode(), new RegularNode()];
        NodeCollection route = new NodeCollection(nodes);
        Node expected = route.GetNodeIfGhostMovesTwoSpaces();
        Node actual = GetNextPosition(route);
        Assert.Equal(expected, actual);
    }

    [Fact]
    public void PartC_State_WanderAroundBaseStateShouldGoToBase()
    {
        UpdateStateWhenPacManGetsSuperDot(); // mueve a FleeFromPlayer
        UpdateStateWhenPacManIsEncountered(); // mueve a ReturnToBase
        UpdateStateWhenReachingBase(); // mueve a WanderAroundBase
        UpdateStateWhenPacManIsEncountered(); // no pasa nada
        UpdateStateWhenPacManGetsSuperDot(); // no pasa nada
        UpdateStateWhenReachingBase(); // no pasa nada
        Assert.Equal(Goal.Base, GetGoal()); // el objetivo es ir a la base en WanderAroundBase
    }
    
    [Fact]
    public void PartC_State_WanderAroundBaseStateDoesNotMove()
    {
        UpdateStateWhenPacManGetsSuperDot(); // mueve a FleeFromPlayer
        UpdateStateWhenReachingBase(); // mueve a WanderAroundBase
        UpdateStateAtEndOfCycle(); // aumenta el contador de iteraciones en 1
        UpdateStateAtEndOfCycle(); // aumenta el contador de iteraciones en 1 (no es suficiente para cambiar de estado)
        AssertThatGhostDoesNotMove();
    }

    private void AssertThatGhostDoesNotMove()
    {
        List<Node> nodes = [new RegularNode(), new RegularNode(), new RegularNode(), new RegularNode()];
        NodeCollection route = new NodeCollection(nodes);
        Node expected = route.GetNodeIfGhostDoesNotMove();
        Node actual = GetNextPosition(route);
        Assert.Equal(expected, actual);
    }
    
    [Fact]
    public void PartC_State_WanderAroundBaseStateMovesBackToSeekPlayerAfterThreeIterations()
    {
        UpdateStateWhenPacManGetsSuperDot(); // mueve a FleeFromPlayer
        UpdateStateWhenReachingBase(); // mueve a WanderAroundBase
        for(int i = 0; i < 3; i++) // tres iteraciones deberían mover el estado a SeekPlayer
            UpdateStateAtEndOfCycle();
        Assert.Equal(Goal.PacMan, GetGoal()); // el objetivo en SeekPlayer es PacMan
    }
    
    [Fact]
    public void PartD_State_AngryStateShouldGoToPacMan()
    {
        for(int i = 0; i < 11; i++) // luego de 11 iteraciones en SeekPlayer se debería over a AngryState
            UpdateStateAtEndOfCycle();
        Assert.Equal(Goal.PacMan, GetGoal()); // el objetivo en Angry es PacMan
    }

    [Fact]
    public void PartD_State_AngryStateMovesTwoSpacesAtTheTime()
    {
        for(int i = 0; i < 11; i++) // luego de 11 iteraciones en SeekPlayer se debería over a AngryState
            UpdateStateAtEndOfCycle();
        AssertThatGhostMovesTwoSpaces();
    }

    [Fact]
    public void PartD_State_SeekPlayerDoesNotChangeAfterTenIterations()
    {
        for(int i = 0; i < 10; i++) // 10 iteraciones en SeekPlayer no es suficiente para ir a AngryState
            UpdateStateAtEndOfCycle();
        AssertThatGhostMovesOnlyOneSpace();
    }

    [Fact]
    public void PartD_State_AngryStateKillsPacManThenItIsEncountered()
    {
        for(int i = 0; i < 11; i++) // luego de 11 iteraciones en SeekPlayer se debería over a AngryState
            UpdateStateAtEndOfCycle();
        Assert.Throws<PacManIsDeadException>(UpdateStateWhenPacManIsEncountered); // al matar a pacman se debería tirar esta excepción
    }

    [Fact]
    public void PartD_State_AngryStateMovesToFleeFromPlayerWhenPacmanGetsSuperDot()
    {
        for(int i = 0; i < 11; i++) // luego de 11 iteraciones en SeekPlayer se debería over a AngryState
            UpdateStateAtEndOfCycle();
        UpdateStateWhenReachingBase(); // Encontrar la base no hace nada
        UpdateStateAtEndOfCycle(); // Tampoco hace nada
        UpdateStateWhenPacManGetsSuperDot(); // Mueve el estado a FleeFromPlayer
        Assert.Equal(Goal.Base, GetGoal()); // En FleeFromPlayer el objetivo es la base
        AssertThatGhostMovesOnlyOneSpace(); // En FleeFromPlayer se mueve un espacio a la vez
    }
}