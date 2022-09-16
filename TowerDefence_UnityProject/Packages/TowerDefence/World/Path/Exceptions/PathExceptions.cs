using System;

namespace TowerDefence.World.Path.Exceptions
{
    public sealed class InfinitePathException : Exception
    {
        public InfinitePathException(string message = "Path loops around forever") : base(message)
        {
        }
    }

    public sealed class InvalidPathException : Exception
    {
        public InvalidPathException(string message = "This path is invalid") : base(message)
        {
        }
    }

    public sealed class NoPathEntranceException : Exception
    {
        public NoPathEntranceException(string message = "No path entrance found") : base(message)
        {
        }
    }

    public sealed class NoPathExitException : Exception
    {
        public NoPathExitException(string message = "No path exit found") : base(message)
        {
        }
    }
}