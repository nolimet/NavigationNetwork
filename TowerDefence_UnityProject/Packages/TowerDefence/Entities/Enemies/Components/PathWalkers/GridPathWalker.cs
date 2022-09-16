using Cysharp.Threading.Tasks;
using Newtonsoft.Json;
using NoUtil.Math;
using System.Collections.Generic;
using System.Linq;
using TowerDefence.Entities.Components;
using TowerDefence.World.Grid;
using TowerDefence.World.Grid.Data;
using UnityEngine;
using Zenject;
using static TowerDefence.Systems.Waves.Data.Wave;

namespace TowerDefence.Entities.Enemies.Components
{
    [Component(ComponentType.Enemy, typeof(IPathWalkerComponent))]
    [JsonObject(MemberSerialization.OptIn)]
    internal sealed class GridPathWalker : BaseEnemyPathWalker
    {
        private readonly List<IGridCell> path = new();
        private GridWorld gridWorld;
        private int currentCell = 0;

        [JsonProperty] private float moveSpeed = 1f;

        private const float nearTargetDistance = 0.01f;// 0.1^2;
        private Vector3 target = Vector3.zero;
        private Vector3 currentSpeed = Vector3.zero;
        private float currentRotationVelocity;
        private EnemyGroup enemyGroup;

        public override float PathProgress { get; protected set; }

        [Inject]
        public void Inject(GridWorld gridWorld)
        {
            this.gridWorld = gridWorld;
            gridWorld.OnPathCacheCleared += OnPathCacheCleared;
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

        public async UniTask SetStartEnd(EnemyGroup group)
        {
            enemyGroup = group;
            self.Transform.position = Vector3.one * 10000f;

            var path = await gridWorld.GetPath(group.entranceId, group.exitId);
            this.path.Clear();
            this.path.AddRange(path);
            if (this.path.Any() && this.path.Count > 2)
            {
                currentCell = 1;
                target = this.path[1].WorldPosition;
                self.Transform.position = this.path[0].WorldPosition;
                self.Transform.rotation = Quaternion.Euler(0, 0, Math.VectorToAngle(target - self.Transform.position));
            }
            else
            {
                Debug.Log("Pathing failed");
                model.Health = 0f;
            }
        }

        private void OnPathCacheCleared()
        {
            SetStartEnd(enemyGroup).Preserve().Forget();
        }
    }
}