using PacManSimulator;
using PacManSimulator.IteratorFactory;
using PacManSimulator.SimulatorClasses;

string path = "Mazes/maze_9.txt";
string path2 = "StartingPositions/positions_9.txt";
IFactory iteratorFactory = PacManController.GetAvoidPacManBFSFactory();
// IFactory iteratorFactory = new BFSIteratorFactory();
Simulator gameSimulator = new Simulator(path, path2, iteratorFactory);

gameSimulator.StartGameSimulation();

string[] output = gameSimulator.GetSimulationOutput();

File.WriteAllLines("Output.txt", output);