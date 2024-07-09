using PacManSimulator.Ghosts;
using PacManSimulator.IteratorFactory;
using PacManSimulator.SimulatorClasses.Enums;
using PacManSimulator.SimulatorClasses.GameEntities;
using PacManSimulator.SimulatorClasses.GameEntities.Nodes;

namespace PacManSimulator.SimulatorClasses;

public class MazeFactory
{
    private IFactory _iteratorFactory;

    public MazeFactory(IFactory iteratorFactory)
    {
        _iteratorFactory = iteratorFactory;
    }
    public Node[,] GetMaze(MazeTile[,] mazeTiles,GameElement[,] gameElements)
    {
        int mazeHeight = mazeTiles.GetLength(0);
        int mazeWidth = mazeTiles.GetLength(1);
        Node[,] maze = new Node[mazeHeight, mazeWidth];
        FillMaze(maze,mazeTiles);
        AssignNeighbors(maze);
        AddStartingElements(maze, gameElements);
        return maze;
    }

    private void FillMaze(Node[,] maze, MazeTile[,] mazeTiles)
    {
        for (int i = 0; i < maze.GetLength(0); i++)
        {
            for (int j = 0; j < maze.GetLength(1); j++)
            {
                maze[i, j] = GetNode(mazeTiles[i,j]);
            }
        }
    }

    private Node GetNode(MazeTile tile)
    {
        switch (tile)
        {
            case MazeTile.Wall:
                return new WallNode();
            case MazeTile.BasicTile:
                return new RegularNode();
            case MazeTile.GhostBase:
                return new GhostBaseNode();
            default:
                throw new Exception();
        }
    }

    private void AssignNeighbors(Node[,] maze)
    {
        for (int i = 0; i < maze.GetLength(0); i++)
        {
            for (int j = 0; j < maze.GetLength(1); j++)
            {
                if (j - 1 >= 0)
                {
                    maze[i,j].AddNeighbor(maze[i,j-1]);
                }
                
                if (i + 1 < maze.GetLength(0))
                {
                    maze[i,j].AddNeighbor(maze[i+1,j]);
                }

                if (j + 1 < maze.GetLength(1))
                {
                    maze[i,j].AddNeighbor(maze[i,j+1]);
                }
                
                if (i - 1 >= 0)
                {
                    maze[i,j].AddNeighbor(maze[i-1,j]);
                }
            }
        }
    }

    private void AddStartingElements(Node[,] maze, GameElement[,] startingElements)
    {
        for (int i = 0; i < maze.GetLength(0); i++)
        {
            for (int j = 0; j < maze.GetLength(1); j++)
            {
                AddNodeElement(maze[i, j], startingElements[i, j]);
            }
        }
    }

    private void AddNodeElement(Node node, GameElement element)
    {
        switch (element)
        {
            case GameElement.Ghost:
                AbstractGhost abstractGhost = new Ghost(_iteratorFactory);
                Dot dot = Dot.Normal;
                node.SetGhost(abstractGhost);
                node.SetDot(dot);
                break;
            case GameElement.SuperDot:
                Dot superDot = Dot.Super;
                node.SetDot(superDot);
                break;
            case GameElement.Dot:
                Dot normalDot = Dot.Normal;
                node.SetDot(normalDot);
                break;
            case GameElement.PacMan:
                PacMan pacMan = new PacMan(node);
                node.SetPacMan(pacMan);
                break;
            case GameElement.Empty:
                break;
        }
    }

    
}