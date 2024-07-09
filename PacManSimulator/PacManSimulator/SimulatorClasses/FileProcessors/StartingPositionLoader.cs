using PacManSimulator.SimulatorClasses.Enums;

namespace PacManSimulator.SimulatorClasses.FileProcessors;

public class StartingPositionLoader
{
    public GameElement[,] GetMazeElements(string path)
    {
        string[] text = File.ReadAllLines(path);
        int mazeHeight = text.Length;
        int mazeWidth = text[0].Length;
        GameElement[,] maze = new GameElement[mazeHeight, mazeWidth];
        for (int height = 0; height < mazeHeight; height++)
        {
            AddElements(maze, height, text[height]);
        }

        return maze;
    }

    private void AddElements(GameElement[,] maze, int height, string row)
    {
        int mazeWidth = row.Length;
        for (int width = 0; width < mazeWidth; width++)
        {
            maze[height, width] = GetElementType(row[width]);
        }
    }

    private GameElement GetElementType(char element)
    {
        switch (element)
        {
            case '#':
                return GameElement.Empty;
            case '-':
                return GameElement.Empty;
            case 'O':
                return GameElement.SuperDot;
            case 'o':
                return GameElement.Dot;
            case 'P':
                return GameElement.PacMan;
            case 'G':
                return GameElement.Ghost;
            default:
                throw new Exception();
        }
    }
}