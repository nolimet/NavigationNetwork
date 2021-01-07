using System;

namespace TowerDefence.World.Path
{
    public class InfitePathException : Exception
    {
        public InfitePathException(string message) : base(message)
        {
        }
    }

    public class InvalidPathException : Exception
    {
        public InvalidPathException(string message) : base(message)
        {
        }
    }

    public class NoPathEntranceException : Exception
    {
        public NoPathEntranceException(string message) : base(message)
        {
        }
    }

    public class NoPathExitException : Exception
    {
        public NoPathExitException(string message) : base(message)
        {
        }
    }
}