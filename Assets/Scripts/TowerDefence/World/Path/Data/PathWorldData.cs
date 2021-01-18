using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace TowerDefence.World.Path.Data
{
    public class PathWorldData
    {
        private readonly IReadOnlyDictionary<int, AnimationCurve3D> paths;
        private readonly IEnumerable<PathLineRenderer> pathVisuals;

        public PathWorldData(Vector3[][] paths, IEnumerable<PathLineRenderer> pathVisuals)
        {
            Dictionary<int, AnimationCurve3D> animationPaths = new Dictionary<int, AnimationCurve3D>();
            foreach (var path in paths)
            {
                animationPaths.Add(animationPaths.Count, new AnimationCurve3D(path));
            }

            this.paths = animationPaths;
            this.pathVisuals = pathVisuals;
        }

        public void Destroy()
        {
            foreach (var pathvisual in pathVisuals)
            {
                Object.Destroy(pathvisual.gameObject);
            }
        }

        public Vector3 Evaluate(float position, int pathId)
        {
            return paths[pathId].Evaluate(position);
        }

        private class AnimationCurve3D
        {
            private readonly AnimationCurve curveX, curveY, curveZ;
            private readonly float length = 0;

            public AnimationCurve3D(Vector3[] points)
            {
                curveX = new AnimationCurve();
                curveY = new AnimationCurve();
                curveZ = new AnimationCurve();

                curveX.postWrapMode = WrapMode.Clamp;
                curveY.postWrapMode = WrapMode.Clamp;
                curveZ.postWrapMode = WrapMode.Clamp;

                float position = 0f;
                Vector3 lastPoint = points.First();

                for (int i = 0; i < points.Length; i++)
                {
                    Vector3 cp = points[i];
                    curveX.AddKey(position, cp.x);
                    curveY.AddKey(position, cp.y);
                    curveZ.AddKey(position, cp.z);

                    position += Vector3.Distance(cp, lastPoint);
                    lastPoint = cp;
                }
                length = position;
            }

            public Vector3 Evaluate(float position)
            {
                return new Vector3
                {
                    x = curveX.Evaluate(position),
                    y = curveY.Evaluate(position),
                    z = curveZ.Evaluate(position)
                };
            }

            public bool PathCompleted(float position)
            {
                return position < length && !Mathf.Approximately(position, length);
            }
        }
    }
}