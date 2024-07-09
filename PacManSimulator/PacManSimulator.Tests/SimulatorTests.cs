
using PacManSimulator.IteratorFactory;
using PacManSimulator.SimulatorClasses;
using PacManSimulator.SimulatorClasses.Enums;
using PacManSimulator.SimulatorClasses.GameEntities.Nodes;

namespace PacManSimulator.Tests;

public class SimulatorTests
{
    [Fact]
    public void PartA_BFSIterator_ShouldWorkOnTestingMazeWhenGoalIsBase()
    {
        IFactory factory = new BFSIteratorFactory();
        string expectedNodes = "11,6,12,1,7,13,2,8,3,4,5,10,15";
        TestingMaze maze = new TestingMaze(factory);
        Node ghost = maze.GetGhost();
        Goal goal = Goal.Base;

        List<Node> nodes = PacManController.GetNodesVisited(ghost, goal, factory);
        
        Assert.True(nodes[^1].IsBase(), "El último nodo de la ruta no es la base."); 
        string nodeIds = GetNodeIds(nodes);
        Assert.Equal(expectedNodes, nodeIds);
    }

    [Fact]
    public void PartA_BFSIterator_ShouldWorkOnTestingMazeWhenGoalIsPacMan()
    {
        IFactory factory = new BFSIteratorFactory();
        string expectedNodes = "11,6,12,1,7,13,2,8,3";
        TestingMaze maze = new TestingMaze(factory);
        Node ghost = maze.GetGhost();
        Goal goal = Goal.PacMan;

        List<Node> nodes = PacManController.GetNodesVisited(ghost, goal, factory);
        
        Assert.True(nodes[^1].HasPacMan(), "El último nodo de la ruta no contiene a pacman."); 
        string nodeIds = GetNodeIds(nodes);
        Assert.Equal(expectedNodes, nodeIds);
    }

    private string GetNodeIds(List<Node> nodes)
    {
        List<string> ret = new List<string>();
        foreach (var node in nodes)
            ret.Add(node.NodeId.ToString());
        return string.Join(",", ret);
    }

    [Theory]
    [InlineData("maze_0.txt", "positions_0.txt", "test_simulation_0.txt")]
    [InlineData("maze_1.txt", "positions_1.txt", "test_simulation_1.txt")]
    [InlineData("maze_2.txt", "positions_2.txt", "test_simulation_2.txt")]
    [InlineData("maze_3.txt", "positions_3.txt", "test_simulation_3.txt")]
    [InlineData("maze_4.txt", "positions_4.txt", "test_simulation_4.txt")]
    public void PartA_BFSIterator_ShouldUseTheRightAlgorithm(string structureFileName, string startingPositionsFileName,
        string expectedFileName)
    {
        IFactory factory = new BFSIteratorFactory();
        AssertSimulationCorrectness(factory, structureFileName, startingPositionsFileName, expectedFileName);
    }

    private void AssertSimulationCorrectness(IFactory factory, string structureFileName,
        string startingPositionsFileName, string expectedFileName)
    {
        string mazePath = Path.Combine("Mazes", structureFileName);
        string positionsPath = Path.Combine("StartingPositions", startingPositionsFileName);
        Simulator gameSimulator = new Simulator(mazePath, positionsPath, factory);
        gameSimulator.StartGameSimulation();
        string[] result = gameSimulator.GetSimulationOutput();
        string[] expected = GetExpectedOutputLines(expectedFileName);

        Assert.Equal(result, expected);
    }

    [Fact]
    public void PartB_AvoidPacManBFS_ShouldWorkOnTestingMazeWhenGoalIsBase()
    {
        IFactory factory = PacManController.GetAvoidPacManBFSFactory();
        string expectedNodes = "11,6,12,1,7,13,2,8";
        TestingMaze maze = new TestingMaze(factory);
        Node ghost = maze.GetGhost();
        Goal goal = Goal.Base;

        List<Node> nodes = PacManController.GetNodesVisited(ghost, goal, factory);
        
        string nodeIds = GetNodeIds(nodes);
        Assert.Equal(expectedNodes, nodeIds);
    }

    [Fact]
    public void PartB_AvoidPacManBFS_ShouldWorkOnTestingMazeWhenGoalIsPacMan()
    {
        IFactory factory = PacManController.GetAvoidPacManBFSFactory();
        string expectedNodes = "11,6,12,1,7,13,2,8,3";
        TestingMaze maze = new TestingMaze(factory);
        Node ghost = maze.GetGhost();
        Goal goal = Goal.PacMan;

        List<Node> nodes = PacManController.GetNodesVisited(ghost, goal, factory);
        
        Assert.True(nodes[^1].HasPacMan(), "El último nodo de la ruta no contiene a pacman."); 
        string nodeIds = GetNodeIds(nodes);
        Assert.Equal(expectedNodes, nodeIds);
    }

    
    [Theory]
    [InlineData("maze_5.txt", "positions_5.txt", "test_simulation_5.txt")]
    [InlineData("maze_6.txt", "positions_6.txt", "test_simulation_6.txt")]
    public void PartB_AvoidPacManBFS_ShouldUseTheAlgorithmCorrectly(string structureFileName,
        string startingPositionsFileName, string expectedFileName)
    {
        IFactory factory = PacManController.GetAvoidPacManBFSFactory();
        AssertSimulationCorrectness(factory, structureFileName, startingPositionsFileName, expectedFileName);
    }
    
    [Theory]
    [InlineData("maze_10.txt", "positions_10.txt", "test_simulation_10.txt")]
    [InlineData("maze_11.txt", "positions_11.txt", "test_simulation_11.txt")]
    [InlineData("maze_12.txt", "positions_12.txt", "test_simulation_12.txt")]
    [InlineData("maze_13.txt", "positions_13.txt", "test_simulation_13.txt")]
    [InlineData("maze_14.txt", "positions_14.txt", "test_simulation_14.txt")]
    public void PartC_State_ShouldSuccessfullyManageGhostStates(string structureFileName,
        string startingPositionsFileName, string expectedFileName)
    {
        IFactory factory = new BFSIteratorFactory();
        AssertSimulationCorrectness(factory, structureFileName, startingPositionsFileName, expectedFileName);
    }

    [Theory]
    [InlineData("maze_15.txt", "positions_15.txt", "test_simulation_15.txt")]
    [InlineData("maze_16.txt", "positions_16.txt", "test_simulation_16.txt")]
    [InlineData("maze_17.txt", "positions_17.txt", "test_simulation_17.txt")]
    [InlineData("maze_18.txt", "positions_18.txt", "test_simulation_18.txt")]
    [InlineData("maze_19.txt", "positions_19.txt", "test_simulation_19.txt")]
    public void PartD_State_ShouldSuccessfullyManageGetAngryState(string structureFileName,
        string startingPositionsFileName, string expectedFileName)
    {
        IFactory factory = new BFSIteratorFactory();
        AssertSimulationCorrectness(factory, structureFileName, startingPositionsFileName, expectedFileName);
    }

    private string[] GetExpectedOutputLines(string fileName)
    {
        return GetLines("ExpectedOutput", fileName);
    }

    private string[] GetLines(string folderName, string fileName)
    {
        var path = Path.Combine(folderName, fileName);
        return File.ReadAllLines(path);
    }
}
