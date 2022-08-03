using Newtonsoft.Json;
using NoUtil.Math;
using TowerDefence.Entities.Components;
using UnityEngine;
using static TowerDefence.World.Path.Data.PathWorldData;

namespace TowerDefence.Entities.Enemies.Components
{
    [Component(ComponentType.Enemy, typeof(IPathWalkerComponent))]
    [JsonObject(MemberSerialization.OptIn)]
    public class StaticPathWalker : BaseEnemyPathWalker
    {
        [JsonProperty] private readonly float speedMult;

        private AnimationCurve3D path;

        private Vector3 lastPosition;

        public void SetPath(AnimationCurve3D path)
        {
            this.path = path;
        }

        public override float PathProgress { get; protected set; } = 0f;

        public override void Tick()
        {
            lastPosition = self.Transform.position;
            self.Transform.position = path.Evaluate(PathProgress);

            var dir = Math.VectorToAngle(lastPosition - self.Transform.position);

            self.Transform.rotation = Quaternion.Euler(0, 0, dir);

            PathProgress += Time.deltaTime * speedMult;

            if (PathProgress >= path.length)
            {
                ReachedEnd?.Invoke(self);
            }
        }
    }
}