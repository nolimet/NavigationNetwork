using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TowerDefence.World.Path.Rendering;
using UnityEngine;
using Object = UnityEngine.Object;
using Random = UnityEngine.Random;

namespace TowerDefence.World.Path.Data
{
    //TODO Fix animation curves. There is a slowdown between keyframes. This should not happen it should be a constant speed
    [Serializable]
    public sealed class PathWorldData
    {
        public readonly IReadOnlyDictionary<int, AnimationCurve3D> Paths;

        private readonly IEnumerable<PathRendererBase> pathVisuals;

        public PathWorldData(Vector3[][] paths, IEnumerable<PathRendererBase> pathVisuals)
        {
            Dictionary<int, AnimationCurve3D> animationPaths = new Dictionary<int, AnimationCurve3D>();
            foreach (var path in paths)
            {
                animationPaths.Add(animationPaths.Count, new AnimationCurve3D(path));
            }

            Paths = animationPaths;
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
            return Paths[Random.Range(0, Paths.Count)];
        }

        [Serializable]
        public class AnimationCurve3D
        {
            public readonly AnimationCurve CurveX, CurveY, CurveZ;

            public readonly float Length;

            //TODO fix paths not being generated correctly and causing stops at corners or where there are points
            public AnimationCurve3D(Vector3[] points)
            {
                CurveX = new AnimationCurve();
                CurveY = new AnimationCurve();
                CurveZ = new AnimationCurve();

                CurveX.postWrapMode = WrapMode.Clamp;
                CurveY.postWrapMode = WrapMode.Clamp;
                CurveZ.postWrapMode = WrapMode.Clamp;

                float position = Vector3.Distance(points[0], points[1]);
                Vector3 cp = points.First();
                Vector3 lastPoint = points.First();
                Vector3 inTangent = Vector3.zero;

                CurveX.AddKey(0, cp.x);
                CurveY.AddKey(0, cp.y);
                CurveZ.AddKey(0, cp.z);
                for (int i = 1; i < points.Length; i++)
                {
                    cp = points[i];
                    var outTangent = Vector3.zero;
                    var newPosition = position + Vector3.Distance(cp, lastPoint);

                    if (points.Length + 1 < points.Length)
                    {
                        outTangent = (points[i + 1] - cp) / (newPosition - position); // not how you calc tangent!
                    }

                    //In is never set!
                    CurveX.AddKey(new Keyframe(position, cp.x, inTangent.y, outTangent.x));
                    CurveY.AddKey(new Keyframe(position, cp.y, inTangent.y, outTangent.x));
                    CurveZ.AddKey(new Keyframe(position, cp.z, inTangent.y, outTangent.x));

                    position = newPosition;
                    lastPoint = cp;
                }

                Length = position;
            }

            public void LogCurveValues()
            {
                Debug.Log("X\n" + LogCurve(CurveX));
                Debug.Log("Y\n" + LogCurve(CurveY));
                Debug.Log("Z\n" + LogCurve(CurveZ));

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
                    x = CurveX.Evaluate(position),
                    y = CurveY.Evaluate(position),
                    z = CurveZ.Evaluate(position)
                };
            }

            public bool PathCompleted(float position)
            {
                return position > Length || Mathf.Approximately(position, Length);
            }
        }
    }
}