using PacManSimulator.SimulatorClasses.GameEntities.Nodes;

namespace PacManSimulator.SimulatorClasses;

public class MazePrinter
{
    private Logger _logger = new Logger();
    public void PrintMaze(Node[,] maze)
    {
        ResetConsolePointer();
        for (int i = 0; i < maze.GetLength(0); i++)
        {
            for (int j = 0; j < maze.GetLongLength(1); j++)
            {
                SetBackgroundColor(maze[i, j]);
                PrintIcon(maze[i, j]);
            }
            _logger.LogNewLine();
        }
        Console.BackgroundColor = ConsoleColor.Black;
        Console.ForegroundColor = ConsoleColor.White;
    }

    private void ResetConsolePointer()
    {
        try
        {
            SetCursorToPosition(0, 0);
        }
        catch (IOException e)
        {
            // Cursor Could not be set to the desired position, this is used because the tests do not recognize
            // Console.SetCursorPosition
        }
    }
    
    private void SetCursorToPosition(int x, int y)
    {
        Console.SetCursorPosition(x, y);
    }

    private void SetBackgroundColor(Node node)
    {
        if (node is WallNode)
        {
            Console.BackgroundColor = ConsoleColor.DarkBlue;
        }
        else if (node is RegularNode)
        {
            Console.BackgroundColor = ConsoleColor.Black;
        }
        else if (node is GhostBaseNode)
        {
            Console.BackgroundColor = ConsoleColor.Magenta;
        }
    }

    private void PrintIcon(Node node)
    {
        if (node.HasGhost())
        {
            Console.ForegroundColor = ConsoleColor.Red;
            
            _logger.Write("   £   ");
        }
        else if (node.HasPacMan())
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            _logger.Write("   ¢   ");
        }
        else if (node.HasSuperDot())
        {
            Console.ForegroundColor = ConsoleColor.Magenta;
            _logger.Write("   *   ");
        }
        else if (node.HasDot())
        {
            Console.ForegroundColor = ConsoleColor.Green;
            _logger.Write("   o   ");
        }
        else
        {
            _logger.Write("       ");
        }
    }

    public string[] GetOutput()
    {
        return _logger.GetOutput();
    }
}