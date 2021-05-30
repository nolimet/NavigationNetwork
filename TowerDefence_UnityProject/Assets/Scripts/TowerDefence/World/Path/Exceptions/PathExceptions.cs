using System;

namespace TowerDefence.World.Path.Exceptions
{
    public class InfitePathException : Exception
    {
        public InfitePathException(string message = "Path loops around forever") : base(message)
        {
        }
    }

    public class InvalidPathException : Exception
    {
        public InvalidPathException(string message = "This path is invalid") : base(message)
        {
        }
    }

    public class NoPathEntranceException : Exception
    {
        public NoPathEntranceException(string message = "No path entrance found") : base(message)
        {
        }
    }

    public class NoPathExitException : Exception
    {
        public NoPathExitException(string message = "No path exit found") : base(message)
        {
        }
    }
}