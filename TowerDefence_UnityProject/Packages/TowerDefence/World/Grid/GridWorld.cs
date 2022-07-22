using Cysharp.Threading.Tasks;
using System;
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
        private readonly Dictionary<(IGridCell start, IGridCell end), IEnumerable<IGridCell>> pathCache = new();

        private Vector2Int[] entrances = Array.Empty<Vector2Int>();
        private Vector2Int[] exits = Array.Empty<Vector2Int>();

        public GridWorld(GridGenerator gridGenerator, GridVisualGenerator visualGenerator)
        {
            this.gridGenerator = gridGenerator;
            this.visualGenerator = visualGenerator;
        }

        public async UniTask CreateWorld(GridSettings settings)
        {
            ClearPathCache();

            entrances = settings.EntryPoints;
            exits = settings.EndPoints;

            world = gridGenerator.CreateNodes(settings);
            await visualGenerator.CreateVisuals(world, settings);
        }

        internal void DestroyWorld()
        {
            visualGenerator.DestroyTiles();
            world = Array.Empty<IGridCell>();
            ClearPathCache();
        }

        internal void ClearPathCache() => pathCache.Clear();

        public async UniTask<IEnumerable<IGridCell>> GetPath(int entraceId, int exitId)
        {
            var entrace = entraceId < entrances.Length ? GetCell(entrances[entraceId]) : null;
            var exit = exitId < exits.Length ? GetCell(exits[exitId]) : null;

            if (entrace != null && exit != null)
            {
                return await GetPath(entrace, exit);
            }

            return Array.Empty<IGridCell>();
        }

        public async UniTask<IEnumerable<IGridCell>> GetPath(IGridCell start, IGridCell end)
        {
            PathFinder pathFinder;
            IEnumerable<IGridCell> path;
            if (pathCache.TryGetValue((start, end), out path))
            {
                return path;
            }

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

            if (!pathCache.ContainsKey((start, end)))
            {
                pathCache.Add((start, end), path);
            }
            pathfinderPool.Add(pathFinder);
            return path;
        }

        public IGridCell GetCell(Vector2Int position)
        {
            return world.FirstOrDefault(x => x.Position.Equals(position));
        }
    }
}
