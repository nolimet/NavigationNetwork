using NoUtil.Math;
using UnityEngine;
using static TowerDefence.World.Path.Data.PathWorldData;

namespace TowerDefence.World.Path
{
    public abstract class WalkerBase : MonoBehaviour
    {
        private AnimationCurve3D path;
        private Vector3 lastPosition;
        protected float position { get; private set; }

        [SerializeField]
        private float speedMult = 1;

        public bool AtEndOfPath => path.PathCompleted(position);

        public abstract void ReachedEnd();

        public void SetPath(AnimationCurve3D path)
        {
            this.path = path;
        }

        public void WalkPath()
        {
            if (transform)
            {
                lastPosition = transform.position;
                transform.position = path.Evaluate(position);

                var dir = Math.VectorToAngle(lastPosition - transform.position);

                transform.rotation = Quaternion.Euler(0, 0, dir);

                position += Time.deltaTime * speedMult;
            }
        }
    }
}