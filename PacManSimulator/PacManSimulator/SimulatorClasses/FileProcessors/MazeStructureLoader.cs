using PacManSimulator.SimulatorClasses.Enums;

namespace PacManSimulator.SimulatorClasses.FileProcessors;

public class MazeStructureLoader
{
    public MazeTile[,] GetMazeStructure(string path)
    {
        string[] text = File.ReadAllLines(path);
        int mazeHeight = text.Length;
        int mazeWidth = text[0].Length;
        MazeTile[,] maze = new MazeTile[mazeHeight,mazeWidth];
        for (int height = 0; height < mazeHeight; height++)
        {
            AddTiles(maze, height, text[height]);
        }

        return maze;
    }

    private void AddTiles(MazeTile[,] maze, int height, string row)
    {
        int mazeWidth = row.Length;
        for (int width = 0; width < mazeWidth; width++)
        {
            maze[height, width] = GetTileType(row[width]);
        }
    }

    private MazeTile GetTileType(char tile)
    {
        switch (tile)
        {
            case '#':
                return MazeTile.Wall;
            case 'N':
                return MazeTile.BasicTile;
            case 'G':
                return MazeTile.GhostBase;
            default:
                throw new Exception();
        }
    }
}