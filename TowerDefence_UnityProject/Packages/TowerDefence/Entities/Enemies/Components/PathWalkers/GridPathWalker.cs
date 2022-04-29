using Cysharp.Threading.Tasks;
using Newtonsoft.Json;
using System.Collections.Generic;
using TowerDefence.Entities.Components;
using TowerDefence.World.Grid;
using TowerDefence.World.Grid.Data;
using UnityEngine;
using Zenject;

namespace TowerDefence.Entities.Enemies.Components
{
    [Component(ComponentType.Enemy, typeof(IPathWalkerComponent))]
    internal class GridPathWalker : BaseEnemyPathWalker
    {
        [JsonIgnore] private IGridCell startCell;
        [JsonIgnore] private IGridCell endCell;

        [JsonIgnore] private readonly List<IGridCell> path = new();
        [JsonIgnore] private GridWorld gridWorld;
        [JsonIgnore] private int currentCell = 0;

        [JsonProperty] private float moveSpeed = 0.5f;
        [JsonIgnore] private float moveTimer = 0f;

        public override float PathProgress { get; protected set; }

        [Inject]
        public void Inject(GridWorld gridWorld)
        {
            this.gridWorld = gridWorld;
        }

        public override void Tick()
        {
            if (moveTimer >= 1f)
            {
                moveTimer = 0f;

                currentCell++;
                if (currentCell >= path.Count)
                {
                    base.ReachedEnd?.Invoke(self);
                }
                else
                {
                    self.Transform.position = path[currentCell].WorldPosition;
                }
            }

            moveTimer += Time.deltaTime / moveSpeed;
        }

        public async UniTask SetStartEnd(IGridCell start, IGridCell end)
        {
            startCell = start;
            endCell = end;

            self.Transform.position = start.WorldPosition;
            var path = await gridWorld.GetPath(startCell, endCell);
            this.path.AddRange(path);
            currentCell = 0;
        }
    }
}
