using Newtonsoft.Json;
using NoUtil.Math;
using TowerDefence.Entities.Components;
using UnityEngine;
using static TowerDefence.World.Path.Data.PathWorldData;

namespace TowerDefence.Entities.Enemies.Components
{
    [Component(ComponentType.Enemy, typeof(IPathWalkerComponent))]
    [JsonObject(MemberSerialization.OptIn)]
    public sealed class StaticPathWalker : BaseEnemyPathWalker
    {
        [JsonProperty] private readonly float speedMult;

        private AnimationCurve3D path;

        private Vector3 lastPosition;

        public void SetPath(AnimationCurve3D path)
        {
            this.path = path;
        }

        public override float PathProgress { get; protected set; }

        public override void Tick()
        {
            lastPosition = Self.Transform.position;
            Self.Transform.position = path.Evaluate(PathProgress);

            var dir = Math.VectorToAngle(lastPosition - Self.Transform.position);

            Self.Transform.rotation = Quaternion.Euler(0, 0, dir);

            PathProgress += Time.deltaTime * speedMult;

            if (PathProgress >= path.Length)
            {
                ReachedEnd?.Invoke(Self);
            }
        }
    }
}