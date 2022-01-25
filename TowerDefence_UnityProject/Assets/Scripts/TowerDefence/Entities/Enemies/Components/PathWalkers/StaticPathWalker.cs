using NoUtil.Math;
using UnityEngine;
using UnityEngine.Events;
using static TowerDefence.World.Path.Data.PathWorldData;

namespace TowerDefence.Entities.Enemies.Components
{
    public class StaticPathWalker : BaseEnemyPathWalker
    {
        private readonly AnimationCurve3D path;
        private readonly Transform transform;
        private readonly float speedMult;
        private readonly IEnemyObject self;

        private Vector3 lastPosition;

        public StaticPathWalker(AnimationCurve3D path, float speedMult, IEnemyObject self) : base(self.DeathAction)
        {
            this.path = path;
            this.transform = self.Transform;
            this.speedMult = speedMult;
            this.self = self;
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