using UnityEngine;
using static TowerDefence.World.Path.Data.PathWorldData;

namespace TowerDefence.World.Path
{
    public abstract class WalkerBase : MonoBehaviour
    {
        private AnimationCurve3D path;
        private Vector3 lastPosition;
        private float position = 0f;

        public bool AtEndOfPath => path.PathCompleted(position);

        public abstract void ReachedEnd();

        public void DestroyWalker()
        {
            Destroy(gameObject);
        }

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

                var dir = lastPosition - transform.position;
                transform.rotation = Quaternion.Euler(dir);

                position += Time.deltaTime;
            }
        }
    }
}