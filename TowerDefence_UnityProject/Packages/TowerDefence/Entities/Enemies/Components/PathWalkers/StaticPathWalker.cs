using Newtonsoft.Json;
using NoUtil.Math;
using UnityEngine;
using static TowerDefence.World.Path.Data.PathWorldData;

namespace TowerDefence.Entities.Enemies.Components
{
    public class StaticPathWalker : BaseEnemyPathWalker
    {
        [JsonProperty] private readonly float speedMult;

        private AnimationCurve3D path;
        private Transform transform;
        private IEnemyObject self;

        private Vector3 lastPosition;

        public StaticPathWalker(float speedMult, IEnemyObject self) : base(self.DeathAction)
        {
            this.transform = self.Transform;
            this.speedMult = speedMult;
            this.self = self;
        }

        public void SetPath(AnimationCurve3D path)
        {
            this.path = path;
        }

        public override float PathProgress { get; protected set; } = 0f;

        public override void Tick()
        {
            if (transform)
            {
                lastPosition = transform.position;
                transform.position = path.Evaluate(PathProgress);

                var dir = Math.VectorToAngle(lastPosition - transform.position);

                transform.rotation = Quaternion.Euler(0, 0, dir);

                PathProgress += Time.deltaTime * speedMult;

                if (PathProgress >= path.length)
                {
                    reachedEnd?.Invoke(self);
                }
            }
        }
    }
}