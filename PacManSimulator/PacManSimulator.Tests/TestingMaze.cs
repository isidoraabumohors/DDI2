using PacManSimulator.Ghosts;
using PacManSimulator.IteratorFactory;
using PacManSimulator.SimulatorClasses.Enums;
using PacManSimulator.SimulatorClasses.GameEntities;
using PacManSimulator.SimulatorClasses.GameEntities.Nodes;

namespace PacManSimulator.Tests;

public class TestingMaze
{
    private Node[,] _maze;
    
    public TestingMaze(IFactory factory)
    {
        CreateEmptyMaze();
        AddEntities(factory);
        AssignIds();
        AddNeighbors();
    }

    private void CreateEmptyMaze()
    {
        _maze = new Node[3,5];
        for (int i = 0; i < _maze.GetLength(0); i++)
        for (int j = 0; j < _maze.GetLength(1); j++)
            _maze[i, j] = new RegularNode();
    }

    private void AddEntities(IFactory factory)
    {
        _maze[2, 0].SetGhost(new Ghost(factory));
        _maze[2, 4] = new GhostBaseNode();
        _maze[0, 2].SetPacMan(new PacMan(_maze[0, 2]));
        _maze[1, 3] = new WallNode();
        _maze[2, 3] = new WallNode();
        _maze[0, 4].SetDot(Dot.Super);
    }

    private void AssignIds()
    {
        int id = 1;
        for (int i = 0; i < _maze.GetLength(0); i++)
        for (int j = 0; j < _maze.GetLength(1); j++)
        {
            _maze[i, j].NodeId = id;
            id++;
        }
    }
    
    private void AddNeighbors()
    {
        for (int i = 0; i < _maze.GetLength(0); i++)
        for (int j = 0; j < _maze.GetLength(1); j++)
            AddNeighbors(i, j);
    }

    private void AddNeighbors(int i, int j)
    {
        if (i - 1 >= 0)
            _maze[i, j].AddNeighbor(_maze[i - 1, j]);
        if (j + 1 < _maze.GetLength(1))
            _maze[i, j].AddNeighbor(_maze[i, j + 1]);
        if (i + 1 < _maze.GetLength(0))
            _maze[i, j].AddNeighbor(_maze[i + 1, j]);
        if (j - 1 >= 0)
            _maze[i, j].AddNeighbor(_maze[i, j - 1]);
    }

    public Node GetGhost()
        => _maze[2, 0];
}