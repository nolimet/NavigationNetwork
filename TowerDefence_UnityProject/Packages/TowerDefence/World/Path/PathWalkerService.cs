using System.Collections.Generic;
using Zenject;

namespace TowerDefence.World.Path
{
    public class PathWalkerService : ITickable
    {
        private readonly List<WalkerBase> walkers = new List<WalkerBase>();
        private readonly PathWorldController worldController;

        public PathWalkerService(PathWorldController worldController)
        {
            this.worldController = worldController;
        }

        public void AddWalker(WalkerBase walker)
        {
            if (walker == null || !walker)
            {
                throw new System.NullReferenceException("Walker is not set! Walker was not set or Destroyed");
            }

            if (worldController.pathWorldData == null)
            {
                throw new System.NullReferenceException("WorldController.PathWorldData is not set! Path was not set or Destroyed");
            }

            walkers.Add(walker);
        }

        public void RemoveWalker(WalkerBase walker)
        {
            if (walkers.Contains(walker))
            {
                walkers.Remove(walker);
            }
        }

        public void Tick()
        {
            for (int i = walkers.Count - 1; i >= 0; i--)
            {
                var walker = walkers[i];
                if (!walker)
                {
                    walkers.Remove(walker);
                }
                else
                {
                    if (walker.AtEndOfPath)
                    {
                        walker.ReachedEnd();
                    }
                    else
                    {
                        walker.WalkPath();
                    }
                }
            }
        }
    }
}