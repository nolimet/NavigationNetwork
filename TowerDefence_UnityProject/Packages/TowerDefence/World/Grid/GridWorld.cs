using System;
using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using TowerDefence.World.Grid.Data;
using UnityEngine;

namespace TowerDefence.World.Grid
{
    internal sealed class GridWorld
    {
        public Action OnPathCacheCleared;

        private IReadOnlyCollection<IGridCell> world;
        private readonly GridGenerator gridGenerator;
        private readonly GridVisualGenerator visualGenerator;
        private readonly List<PathFinder> pathfinderPool = new();
        private readonly Dictionary<(IGridCell start, IGridCell end), IReadOnlyCollection<IGridCell>> pathCache = new();

        private Vector2Int[] entrances = Array.Empty<Vector2Int>();
        private Vector2Int[] exits = Array.Empty<Vector2Int>();

        private Bounds gridBounds;

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
            gridBounds = await visualGenerator.CreateVisuals(world, settings);
        }

        internal void DestroyWorld()
        {
            visualGenerator.DestroyTiles();
            world = Array.Empty<IGridCell>();
            ClearPathCache();
        }

        internal void ClearPathCache()
        {
            Debug.Log("Cleared Path Cache");
            OnPathCacheCleared?.Invoke();
            pathCache.Clear();
        }

        public Bounds GetGridBounds()
        {
            return gridBounds;
        }

        public async UniTask<IEnumerable<IGridCell>> GetPath(int entranceId, int exitId)
        {
            var entrance = entranceId < entrances.Length ? GetCell(entrances[entranceId]) : null;
            var exit = exitId < exits.Length ? GetCell(exits[exitId]) : null;

            if (entrance != null && exit != null)
            {
                return await GetPath(entrance, exit);
            }

            return Array.Empty<IGridCell>();
        }

        public async UniTask<IReadOnlyCollection<IGridCell>> GetPath(IGridCell start, IGridCell end)
        {
            if (pathCache.TryGetValue((start, end), out var path))
            {
                return path;
            }

            PathFinder pathFinder;
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