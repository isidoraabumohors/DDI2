using PacManSimulator.IteratorFactory;
using PacManSimulator.SimulatorClasses.Enums;
using PacManSimulator.SimulatorClasses.FileProcessors;
using PacManSimulator.SimulatorClasses.GameEntities.Nodes;

namespace PacManSimulator.SimulatorClasses;

public class Simulator
{
    private readonly string _pathToMaze;
    private readonly string _pathToPositions;
    private readonly MazeStructureLoader _mazeStructureLoader = new MazeStructureLoader();
    private readonly StartingPositionLoader _startingPositionLoader = new StartingPositionLoader();
    private readonly MazeFactory _mazeFactory;
    private SimulationController _simulationController;
    
    public Simulator(string pathToMaze, string pathToPositions, IFactory iteratorFactory)
    {
        _pathToMaze = pathToMaze;
        _pathToPositions = pathToPositions;
        _mazeFactory = new MazeFactory(iteratorFactory);
    }

    public void StartGameSimulation()
    {
        Node[,] mazeGraph = GetMazeGraph();
        _simulationController = new SimulationController(mazeGraph);
        _simulationController.StartSimulation();
    }

    private Node[,] GetMazeGraph()
    {
        MazeTile[,] maze = _mazeStructureLoader.GetMazeStructure(_pathToMaze);
        GameElement[,] elements = _startingPositionLoader.GetMazeElements(_pathToPositions);
        Node[,] mazeGraph = _mazeFactory.GetMaze(maze,elements);
        return mazeGraph;
    }

    public string[] GetSimulationOutput()
    {
        return _simulationController.GetOutput();
    }


}