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
        private readonly WorldContainer world;

        private Mesh tileMesh;

        private GameObject[] tiles;

        public GridVisualGenerator(GridWorldSettings worldSettings, WorldContainer world)
        {
            this.worldSettings = worldSettings;
            this.world = world;
        }

        public async UniTask CreateVisuals(IEnumerable<IGridNode> nodes)
        {
            var tileMaterial = await worldSettings.GetTileMaterial();
            List<GameObject> objects = new();

            tileMesh = CreateMesh();

            foreach (var node in nodes)
            {
                CreateRenderer(tileMesh, node);
            }

            tiles = objects.ToArray();

            Mesh CreateMesh()
            {
                List<int> tris = new();
                List<Vector3> verts = new();
                List<Vector2> uvs = new();

                tris.AddRange(new[] { 0, 1, 2 });
                tris.AddRange(new[] { 3, 2, 1 });

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

                m.UploadMeshData(true);

                return m;

                void AddVert(float x, float y, float z)
                {
                    Vector3 v = new
                    (
                        x: x * tileWidth,
                        y: y,
                        z: z * tileLength
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
                g.transform.SetParent(world.TileContainer);
                g.transform.position = new Vector3
                (
                    x: worldSettings.TileSize.x * node.Position.x,
                    y: 0,
                    z: worldSettings.TileSize.y * node.Position.y
                );

                objects.Add(g);

                var r = g.GetComponent<MeshRenderer>();
                var mf = g.GetComponent<MeshFilter>();

                r.sharedMaterial = tileMaterial;

                mf.mesh = m;
            }
        }
    }
}
