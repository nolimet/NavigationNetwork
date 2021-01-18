using UnityEngine;
using System.Collections;

namespace Util
{
    [RequireComponent(typeof(LineRenderer))]
    public class DrawCircle : MonoBehaviour
    {
        public static GameObject Draw(Vector3 pos, Color color, float radius, int numSegments = 128, float width = 0.5f)
        {
            GameObject g = new GameObject("radius");
            g.transform.position = pos;

            DrawCircle d = g.AddComponent<DrawCircle>();
            d.DoRenderer(Vector3.zero, color, radius, numSegments, width);

            return g;
        }

        public static GameObject Draw(Transform parent, Color color, float radius, int numSegments = 128, float width = 0.1f)
        {
            GameObject g = new GameObject("radius");
            g.transform.SetParent(parent);
            g.transform.localPosition = Vector3.zero;


            DrawCircle d = g.AddComponent<DrawCircle>();
            d.DoRenderer(Vector3.zero, color, radius, numSegments, width);

            return g;
        }

        public void DoRenderer(Vector3 pos, Color color, float radius, int numSegments = 128, float width = 0.1f)
        {
            if (numSegments <= 0)
                numSegments = 1;

            LineRenderer l = gameObject.GetComponent<LineRenderer>();
            Material m = new Material(Shader.Find("Unlit/Color"));
            m.color = color;
            l.material = m;
            l.startColor = color;
            l.endColor = color;
            l.startWidth = width;
            l.endWidth = width;
            l.positionCount = numSegments +1;
            l.useWorldSpace = false;

            float deltaTheta = (float)(2.0 * Mathf.PI) / numSegments;
            float theta = 0f;
            Vector3 posdelta;
            float y, x;
            for (int i = 0; i < numSegments+1; i++)
            {
                x = radius * Mathf.Cos(theta);
                y = radius * Mathf.Sin(theta);
                posdelta = new Vector3(x, y, 5);
                l.SetPosition(i, posdelta);
                theta += deltaTheta;
            }
        }
    }
}