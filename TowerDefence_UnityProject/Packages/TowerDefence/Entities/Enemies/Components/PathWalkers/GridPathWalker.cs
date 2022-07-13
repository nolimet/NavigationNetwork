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

        [JsonProperty] private float moveSpeed = 1f;

        private const float nearTargetDistance = 0.01f;// 0.1^2;
        private Vector3 target = Vector3.zero;
        private Vector3 currentSpeed = Vector3.zero;
        private float currentRotationVelocity;

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
            float distance = Vector3.Distance(self.Transform.position, target);
            if (distance < 0.3f)
            {
                currentCell++;
                if (currentCell >= path.Count)
                {
                    if (distance < nearTargetDistance)
                        base.ReachedEnd?.Invoke(self);
                }
                else
                {
                    target = path[currentCell].WorldPosition;
                }
            }

            Vector3 position = self.Transform.position;
            Vector3 newPosition = Vector3.SmoothDamp(position, target, ref currentSpeed, 0.1f, moveSpeed);

            float currentAngle = self.Transform.rotation.eulerAngles.z;
            float targetAngle = Math.VectorToAngle(newPosition - position);

            Quaternion newRotation = Quaternion.Euler(0, 0, Mathf.SmoothDampAngle(currentAngle, targetAngle, ref currentRotationVelocity, 0.1f));

            self.Transform.SetPositionAndRotation(newPosition, newRotation);
        }

        public async UniTask SetStartEnd(IGridCell start, IGridCell end)
        {
            self.Transform.position = Vector3.one * 10000f;

            startCell = start;
            endCell = end;

            var path = await gridWorld.GetPath(startCell, endCell);
            this.path.Clear();
            this.path.AddRange(path);
            if (this.path.Any())
            {
                currentCell = 1;
                target = this.path[1].WorldPosition;
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