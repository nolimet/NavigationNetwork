using Cysharp.Threading.Tasks;
using Newtonsoft.Json;
using NoUtil.Extentsions;
using NoUtil.Math;
using System.Collections.Generic;
using System.Linq;
using TowerDefence.Entities.Components;
using TowerDefence.Entities.Enemies.Components.BaseComponents;
using TowerDefence.World.Grid;
using TowerDefence.World.Grid.Data;
using UnityEngine;
using Zenject;

namespace TowerDefence.Entities.Enemies.Components
{
    [Component(ComponentType.Enemy, typeof(IPathWalkerComponent))]
    internal class GridPathWalker : BaseEnemyPathWalker
    {
        [JsonIgnore] private EnemySettings enemySettings;
        [JsonIgnore] private IGridCell startCell;
        [JsonIgnore] private IGridCell endCell;

        [JsonIgnore] private readonly List<IGridCell> path = new();
        [JsonIgnore] private GridWorld gridWorld;
        [JsonIgnore] private int currentCell = 0;

        [JsonIgnore] private float moveTimer = 0f;
        private Vector3 target = Vector3.zero;
        private Vector3 origin = Vector3.zero;
        private Vector3 currentSpeed = Vector3.zero;

        public override float PathProgress { get; protected set; }

        [Inject]
        public void Inject(GridWorld gridWorld)
        {
            this.gridWorld = gridWorld;
        }

        protected override void OnComponentsChanged(IList<IComponent> components)
        {
            if (components.TryFind(x => x is EnemySettings, out var component) && component is EnemySettings settings)
            {
                enemySettings = settings;
            }
        }

        public override void Tick()
        {
            if (moveTimer >= 1f)
            {
                currentCell++;
                if (currentCell >= path.Count)
                {
                    base.ReachedEnd?.Invoke(self);
                }
                else
                {
                    moveTimer -= 1f;
                    origin = target;
                    target = path[currentCell].WorldPosition;

                    self.Transform.rotation = Quaternion.Euler(0, 0, Math.VectorToAngle(target - self.Transform.position));
                }
            }

            self.Transform.position = Vector3.SmoothDamp(self.Transform.position, target, ref currentSpeed, 0.1f);
            // self.Transform.position = Vector3.Lerp(origin, target, moveTimer);
            moveTimer += Time.deltaTime * enemySettings.Speed;
        }

        public async UniTask SetStartEnd(IGridCell start, IGridCell end)
        {
            self.Transform.position = Vector3.one * 10000f;

            startCell = start;
            endCell = end;

            origin = start.WorldPosition;
            var path = await gridWorld.GetPath(startCell, endCell);
            this.path.Clear();
            this.path.AddRange(path);
            if (this.path.Any())
            {
                currentCell = 1;
                target = this.path[1].WorldPosition;
                origin = start.WorldPosition;
                self.Transform.position = start.WorldPosition;
                self.Transform.rotation = Quaternion.Euler(0, 0, Math.VectorToAngle(target - self.Transform.position));
            }
            else
            {
                model.Health = 0f;
            }

        }
    }
}
