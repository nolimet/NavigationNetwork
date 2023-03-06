using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using Newtonsoft.Json;
using NoUtil.Math;
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
        private int currentCell;

        [JsonProperty] private float moveSpeed = 1f;

        private const float NearTargetDistance = 0.01f; // 0.1^2;
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
            float distance = Vector3.Distance(Self.Transform.position, target);
            if (distance < 0.3f)
            {
                currentCell++;
                if (currentCell >= path.Count)
                {
                    if (distance < NearTargetDistance)
                        ReachedEnd?.Invoke(Self);
                }
                else
                {
                    target = path[currentCell].WorldPosition;
                }
            }

            Vector3 position = Self.Transform.position;
            Vector3 newPosition = Vector3.SmoothDamp(position, target, ref currentSpeed, 0.1f, moveSpeed);

            float currentAngle = Self.Transform.rotation.eulerAngles.z;
            float targetAngle = Math.VectorToAngle(newPosition - position);

            Quaternion newRotation = Quaternion.Euler(0, 0, Mathf.SmoothDampAngle(currentAngle, targetAngle, ref currentRotationVelocity, 0.1f));

            Self.Transform.SetPositionAndRotation(newPosition, newRotation);
        }

        public async UniTask SetStartEnd(EnemyGroup group)
        {
            enemyGroup = group;
            Self.Transform.position = Vector3.one * 10000f;

            var newPath = await gridWorld.GetPath(group.EntranceId, group.ExitId);
            path.Clear();
            path.AddRange(newPath);
            if (path.Any() && path.Count > 2)
            {
                currentCell = 1;
                target = path[1].WorldPosition;
                Self.Transform.position = path[0].WorldPosition;
                Self.Transform.rotation = Quaternion.Euler(0, 0, Math.VectorToAngle(target - Self.Transform.position));
            }
            else
            {
                Debug.Log("Pathing failed");
                Model.Health = 0f;
            }
        }

        private void OnPathCacheCleared()
        {
            SetStartEnd(enemyGroup).Preserve().Forget();
        }
    }
}