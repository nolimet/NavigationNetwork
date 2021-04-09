using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

namespace TowerDefence.World.Path.Data
{
    [System.Serializable]
    public class PathWorldData
    {
        public readonly IReadOnlyDictionary<int, AnimationCurve3D> paths;

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

        public AnimationCurve3D GetRandomPath()
        {
            return paths[Random.Range(0, paths.Count)];
        }

        [System.Serializable]
        public class AnimationCurve3D
        {
            public readonly AnimationCurve curveX, curveY, curveZ;

            public readonly float length = 0;

            //TODO fix paths not being generated correctly and causing stops at corners or where there are points
            public AnimationCurve3D(Vector3[] points)
            {
                curveX = new AnimationCurve();
                curveY = new AnimationCurve();
                curveZ = new AnimationCurve();

                curveX.postWrapMode = WrapMode.Clamp;
                curveY.postWrapMode = WrapMode.Clamp;
                curveZ.postWrapMode = WrapMode.Clamp;

                float position = Vector3.Distance(points[0], points[1]);
                Vector3 cp = points.First();
                Vector3 lastPoint = points.First();
                Vector3 inTangent = Vector3.zero;

                curveX.AddKey(0, cp.x);
                curveY.AddKey(0, cp.y);
                curveZ.AddKey(0, cp.z);
                for (int i = 1; i < points.Length; i++)
                {
                    cp = points[i];
                    var outTangent = Vector3.zero;
                    var newPosition = position + Vector3.Distance(cp, lastPoint);

                    if (points.Length + 1 < points.Length)
                    {
                        outTangent = (points[i + 1] - cp) / (newPosition - position);// not how you calc tangent!
                    }
                    //In is never set!
                    curveX.AddKey(new Keyframe(position, cp.x, inTangent.y, outTangent.x));
                    curveY.AddKey(new Keyframe(position, cp.y, inTangent.y, outTangent.x));
                    curveZ.AddKey(new Keyframe(position, cp.z, inTangent.y, outTangent.x));

                    position = newPosition;
                    lastPoint = cp;
                }

                length = position;

                void calculateTangent()
                {
                }

                //curveX = new AnimationCurve();
                //curveY = new AnimationCurve();
                //curveZ = new AnimationCurve();

                //curveX.postWrapMode = WrapMode.Clamp;
                //curveY.postWrapMode = WrapMode.Clamp;
                //curveZ.postWrapMode = WrapMode.Clamp;

                //float position = Vector3.Distance(points[0], points[1]);
                //Vector3 lastPoint = points.First();
                //Vector3 cp = points.First();

                //curveX.AddKey(0, cp.x);
                //curveY.AddKey(0, cp.y);
                //curveZ.AddKey(0, cp.z);

                //for (int i = 1; i < points.Length; i++)
                //{
                //    cp = points[i];
                //    curveX.AddKey(position, cp.x);
                //    curveY.AddKey(position, cp.y);
                //    curveZ.AddKey(position, cp.z);

                //    position += Vector3.Distance(cp, lastPoint);
                //    lastPoint = cp;
                //}
                //length = position;
            }

            /*
              *  public static AnimationCurve Linear(float timeStart, float valueStart, float timeEnd, float valueEnd)
              *   {
              *      float num = (valueEnd - valueStart) / (timeEnd - timeStart);
              *      Keyframe[] keys = new Keyframe[]
              *       {
              *           new Keyframe(timeStart, valueStart, 0f, num),
              *           new Keyframe(timeEnd, valueEnd, num, 0f)
              *       };
              *       return new AnimationCurve(keys);
              *   }
              */

            public void LogCurveValues()
            {
                Debug.Log("X\n" + LogCurve(curveX));
                Debug.Log("Y\n" + LogCurve(curveY));
                Debug.Log("Z\n" + LogCurve(curveZ));

                string LogCurve(AnimationCurve curve)
                {
                    StringBuilder log = new StringBuilder();
                    foreach (var c in curve.keys)
                    {
                        log.AppendLine($"t:{c.time}, v{c.value}, tIn{c.inTangent}, tOut{c.outTangent}");
                    }
                    return log.ToString();
                }
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
                return position > length && Mathf.Approximately(position, length);
            }
        }
    }
}