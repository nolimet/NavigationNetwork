using Cysharp.Threading.Tasks;
using System.Collections.Generic;
using UnityEngine;

namespace TowerDefence.World.Grid
{
    internal class GridVisualGenerator
    {
        private const float tileWidth = 1;
        private const float tileLength = 1;
        private readonly GridWorldSettings worldSettings;

        private Mesh[] worldMeshes;
        private Material[,] materials;
        private GameObject[] tiles;

        public GridVisualGenerator(GridWorldSettings worldSettings)
        {
            this.worldSettings = worldSettings;
        }

        public async UniTask CreateVisuals(IEnumerable<IGridNode> nodes)
        {
            var tileMaterial = await worldSettings.GetTileMaterial();

            List<int> tris = new();
            List<Vector3> verts = new();
            List<Vector2> uvs = new();

            List<Mesh> meshes = new();
            List<Material> materials = new();
            List<GameObject> objects = new();


            foreach (var node in nodes)
            {
                var m = CreateMeshForNode(node);
                tris.Clear();
                verts.Clear();
                uvs.Clear();
                CreateRenderer(m, node);
            }

            worldMeshes = meshes.ToArray();
            tiles = objects.ToArray();

            Mesh CreateMeshForNode(IGridNode node)
            {
                tris.Add(0);
                tris.Add(1);
                tris.Add(2);

                tris.Add(3);
                tris.Add(2);
                tris.Add(1);

                AddVert(0, 0, 0);
                AddVert(1, 0, 0);
                AddVert(0, 0, 1);
                AddVert(1, 0, 1);

                AddUv(0, 0);
                AddUv(1, 0);
                AddUv(0, 1);
                AddUv(1, 1);

                var m = new Mesh()
                {
                    vertices = verts.ToArray(),
                    triangles = tris.ToArray(),
                    uv = uvs.ToArray()
                };

                meshes.Add(m);
                m.UploadMeshData(true);

                return m;

                void AddVert(float x, float y, float z)
                {
                    Vector3 v = new
                    (
                        x: (x + node.Position.x) * tileWidth,
                        y: y,
                        z: (z + node.Position.y) * tileLength
                    );

                    verts.Add(v);
                }

                void AddUv(float x, float y)
                {
                    uvs.Add(new Vector2(x, y));
                }
            }

            void CreateRenderer(Mesh m, IGridNode node)
            {
                var g = new GameObject($"Tile -{node.Position}", typeof(MeshFilter), typeof(MeshRenderer));
                objects.Add(g);

                var r = g.GetComponent<MeshRenderer>();
                var mf = g.GetComponent<MeshFilter>();

                r.sharedMaterial = tileMaterial;

                mf.mesh = m;
            }
        }
    }
}
