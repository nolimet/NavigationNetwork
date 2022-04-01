using Cysharp.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;

namespace TowerDefence.World.Grid
{
    internal class GridWorld
    {
        private IEnumerable<IGridNode> world;
        private readonly GridGenerator gridGenerator = new();
        private readonly GridVisualGenerator visualGenerator;
        private readonly List<PathFinder> pathfinderPool = new();

        public GridWorld(GridGenerator gridGenerator, GridVisualGenerator visualGenerator)
        {
            this.gridGenerator = gridGenerator;
            this.visualGenerator = visualGenerator;
        }

        public async UniTask CreateWorld(GridSettings settings)
        {
            world = gridGenerator.CreateNodes(settings);
            await visualGenerator.CreateVisuals(world);
        }

        public async UniTask<IEnumerable<IGridNode>> GetPath(IGridNode start, IGridNode end)
        {
            PathFinder pathFinder;
            IEnumerable<IGridNode> path;
            if (pathfinderPool.Any(x => !x.Working))
            {
                pathFinder = pathfinderPool.First(x => !x.Working);
                pathfinderPool.Remove(pathFinder);
            }
            else
            {
                pathFinder = new PathFinder();
            }

            await UniTask.SwitchToThreadPool();
            path = pathFinder.GetPath(start, end);
            await UniTask.SwitchToMainThread();

            pathfinderPool.Add(pathFinder);
            return path;
        }
    }
}
