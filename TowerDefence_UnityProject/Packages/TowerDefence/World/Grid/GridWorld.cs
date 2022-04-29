using Cysharp.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using TowerDefence.World.Grid.Data;
using UnityEngine;

namespace TowerDefence.World.Grid
{
    internal class GridWorld
    {
        private IEnumerable<IGridCell> world;
        private readonly GridGenerator gridGenerator;
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
            await visualGenerator.CreateVisuals(world, settings);
        }

        public async UniTask<IEnumerable<IGridCell>> GetPath(IGridCell start, IGridCell end)
        {
            PathFinder pathFinder;
            IEnumerable<IGridCell> path;
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

        public IGridCell GetCell(Vector2Int position)
        {
            return world.FirstOrDefault(x => x.Position.Equals(position));
        }
    }
}
