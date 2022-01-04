using NoUtil.Math;
using TowerDefence.World.Towers;
using TowerDefence.World.Towers.Models;
using UnityEngine;

namespace TowerDefence.UI.Tower
{
    [RequireComponent(typeof(MeshRenderer)), RequireComponent(typeof(MeshFilter))]
    public class TowerRangeDrawer : MonoBehaviour
    {
        private Mesh mesh;
        [SerializeField] private int segments = 10;
        [SerializeField] private float width = 2f;

        private MeshFilter filter;

        private void Awake()
        {
            filter = GetComponent<MeshFilter>();
        }

        private Mesh CreateMesh()
        {
            var mesh = new Mesh();
            var vertices = new Vector3[segments * 2];
            var normals = new Vector3[segments * 2];
            var triangles = new int[segments * 2 * 3];

            int endRing = segments * 2;

            for (int i = 0; i < segments; i++)
            {
                normals[i] = Vector3.up;

                int triOffSet = i * 6;
                int j = i * 2;
                triangles[triOffSet + 0] = j + 1;
                triangles[triOffSet + 1] = j + 0;
                triangles[triOffSet + 2] = j + 2 >= endRing ? 0 : j + 2;

                triangles[triOffSet + 3] = j + 1;
                triangles[triOffSet + 4] = j + 2 >= endRing ? 0 : j + 2;
                triangles[triOffSet + 5] = j + 3 >= endRing ? 1 : j + 3;
            }

            mesh.vertices = vertices;
            mesh.normals = normals;
            mesh.triangles = triangles;

            Debug.Log(vertices.Length);
            Debug.Log(string.Join(", ", vertices));
            Debug.Log(normals.Length);
            Debug.Log(triangles.Length);
            Debug.Log(string.Join(", ", triangles));
            return mesh;
        }

        public void DrawRange(ITowerModel towerModel)
        {
            DrawRange((float)towerModel.Range);
            transform.position = towerModel.Position;
        }

        private void DrawRange(float radius)
        {
            if (!mesh)
            {
                mesh = CreateMesh();
                filter.mesh = mesh;
            }

            float angleFraction = 360f / segments;
            float angle = 0f;
            var verts = mesh.vertices;
            for (int i = 0; i < verts.Length; i++)
            {
                verts[i] = i % 2 == 0
                    ? GetCirclePosition(radius - width)
                    : GetCirclePosition(radius);

                angle += i % 2 == 1 ? angleFraction : 0;
            }

            mesh.vertices = verts;
            mesh.UploadMeshData(false);

            Vector3 GetCirclePosition(float radius)
            {
                return Math.AngleToVector(angle) * radius;
            }
        }
    }
}