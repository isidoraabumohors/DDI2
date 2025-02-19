﻿using PacManSimulator.Iterators;
using PacManSimulator.SimulatorClasses.Enums;
using PacManSimulator.SimulatorClasses.GameEntities.Nodes;

namespace PacManSimulator.IteratorFactory;

public class BFSIteratorFactory : IFactory
{
    
    public Iterator CreateProduct(Node root, Goal goal)
    {
        return new BFSIterator(root);
    }
}