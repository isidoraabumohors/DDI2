using PacManSimulator.Ghosts;
using PacManSimulator.SimulatorClasses.Exceptions;
using PacManSimulator.SimulatorClasses.GameEntities;
using PacManSimulator.SimulatorClasses.GameEntities.Nodes;

namespace PacManSimulator.SimulatorClasses;

public class SimulationController
{
    private MazePrinter _printer = new MazePrinter();
    private Node[,] _mazeGraph;

    public SimulationController(Node[,] mazeGraph)
    {
        _mazeGraph = mazeGraph;
    }

    public void StartSimulation()
    {
        try
        {
            StartMainLoop();
        }
        catch (PacManIsDeadException e)
        {
            _printer.PrintMaze(_mazeGraph);
            Console.WriteLine("Pac-Man ha sido derrotado");
        }
        catch (PacManWonException e)
        {
            _printer.PrintMaze(_mazeGraph);
            Console.WriteLine("Pac-Man ha ganado");
        }
    }

    private void StartMainLoop()
    {
        _printer.PrintMaze(_mazeGraph);
        // Console.ReadLine();
        while (true)
        {
            MoveAllGhosts();
            MovePacMan();
            UpdateStateAtEndOfCycle();
            CheckIfPacManWon();
            _printer.PrintMaze(_mazeGraph);
            // Console.ReadLine();
        }
    }

    private void MoveAllGhosts()
    {
        List<Node> nodesWithGhosts = GetNodesWithGhosts();
        foreach (Node node in nodesWithGhosts)
        {
            MoveOneGhost(node);
        }
    }

    private List<Node> GetNodesWithGhosts()
    {
        List<Node> nodesWithGhosts = new List<Node>();
        for (int i = 0; i < _mazeGraph.GetLength(0); i++)
        for (int j = 0; j < _mazeGraph.GetLength(1); j++)
        {
            if (_mazeGraph[i, j].HasGhost())
            {
                nodesWithGhosts.Add(_mazeGraph[i,j]);
            }
        }

        return nodesWithGhosts;
    }

    private void MoveOneGhost(Node node)
    {
        AbstractGhost abstractGhost = node.GetGhost();
        abstractGhost.Move(node);
        UpdateStateIfPacManIsEncountered();
        UpdateStateIfGhostReachedBase();
    }

    private void MovePacMan()
    {
        Node nodeWithPacMan = GetNodeWithPacMan();
        PacMan pacMan = nodeWithPacMan.GetPacMan();
        pacMan.Move(nodeWithPacMan);
        if (pacMan.IsPoweredUp())
        {
            UpdateStateIfPacManGotSuperDot();
            pacMan.ResetPowerUp();
        }
        UpdateStateIfPacManIsEncountered();
    }

    private Node GetNodeWithPacMan()
    {
        for (int i = 0; i < _mazeGraph.GetLength(0); i++)
        for (int j = 0; j < _mazeGraph.GetLength(1); j++)
        {
            if (_mazeGraph[i, j].HasPacMan())
            {
                return _mazeGraph[i, j];
            }
        }

        throw new Exception();
    }

    private void UpdateStateIfPacManIsEncountered()
    {
        List<Node> nodesWithGhosts = GetNodesWithGhosts();
        foreach (Node node in nodesWithGhosts)
        {
            if (node.HasGhost() && node.HasPacMan())
            {
                node.GetGhost().UpdateStateWhenPacManIsEncountered();
            }
        }
    }

    private void UpdateStateIfGhostReachedBase()
    {
        List<Node> nodesWithGhosts = GetNodesWithGhosts();
        foreach (Node node in nodesWithGhosts)
        {
            if (node.HasGhost() && node is GhostBaseNode)
            {
                node.GetGhost().UpdateStateWhenReachingBase();
            }
        }
    }

    private void UpdateStateIfPacManGotSuperDot()
    {
        List<Node> nodesWithGhosts = GetNodesWithGhosts();
        foreach (Node node in nodesWithGhosts)
        {
            node.GetGhost().UpdateStateWhenPacManGetsSuperDot();
        }
    }

    private void UpdateStateAtEndOfCycle()
    {
        List<Node> nodesWithGhosts = GetNodesWithGhosts();
        foreach (Node node in nodesWithGhosts)
        {
            node.GetGhost().UpdateStateAtEndOfCycle();
        }
    }

    private void CheckIfPacManWon()
    {
        int dots = 0;
        for (int i = 0; i < _mazeGraph.GetLength(0); i++)
        for (int j = 0; j < _mazeGraph.GetLength(1); j++)
        {
            if (_mazeGraph[i, j].HasDot())
            {
                dots++;
            }
        }
        if (dots == 0) throw new PacManWonException();
    }

    public string[] GetOutput()
    {
        return _printer.GetOutput();
    }


}