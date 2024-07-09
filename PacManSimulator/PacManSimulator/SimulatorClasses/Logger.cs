namespace PacManSimulator.SimulatorClasses;

public class Logger
{
    private List<string> _output = new List<string>();
    private string _currentString = "";

    public void LogNewLine()
    {
        Console.Write("\n");
        _output.Add(_currentString);
        _currentString = "";
    }

    public void Write(string output)
    {
        Console.Write(output);
        _currentString += output;
    }

    public string[] GetOutput()
    {
        _output.Add(_currentString);
        return _output.ToArray();
    }
}