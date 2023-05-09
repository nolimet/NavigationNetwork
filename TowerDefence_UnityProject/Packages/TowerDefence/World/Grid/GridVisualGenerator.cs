using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using TowerDefence.World.Grid.Data;
using UnityEngine;
using Object = UnityEngine.Object;

namespace TowerDefence.World.Grid
{
    internal sealed class GridVisualGenerator : IDisposable
    {
        private readonly GridWorldSettings worldSettings;
        private readonly WorldContainer world;
        private readonly List<Texture2D> groupTextures = new();
        private readonly Dictionary<Vector2Int, Mesh> tileMeshCache = new();

        private Mesh groupMesh;

        private GameObject[] tiles = Array.Empty<GameObject>();

        public GridVisualGenerator(GridWorldSettings worldSettings, WorldContainer world)
        {
            this.worldSettings = worldSettings;
            this.world = world;
        }

        public async UniTask<Bounds> CreateVisuals(IReadOnlyList<IReadOnlyList<IGridCell>> nodes, GridSettings gridSettings)
        {
            DestroyTiles();

            Bounds bounds = new();
            groupMesh ??= CreateGroupMesh();

            var tileGroupSizeShaderProperty = Shader.PropertyToID("_GroupSize");
            var tileMapTextureShaderProperty = Shader.PropertyToID("_GroupHeightMap");

            var tileMaterial = await worldSettings.GetTileMaterial();

            List<GameObject> objects = new();

            var cellGroups = GroupifyGridCells();
            foreach (var cellGroup in cellGroups)
            {
                var groupTexture = CreateCellGroupTexture(cellGroup);
                CreateRenderer(groupTexture, cellGroup);
            }

            tiles = objects.ToArray();

            return bounds;

            void CreateRenderer(Texture2D tileTexture, IGridCell[][] cellGroup)
            {
                var center = Vector2.zero;
                var cellCount = 0;

                var groupSize = new Vector4(cellGroup.Length, cellGroup[0].Length);
                foreach (var subGroup in cellGroup)
                foreach (var cell in subGroup)
                {
                    cellCount++;
                    center += cell.WorldPosition;
                }

                center /= cellCount;
                center -= worldSettings.TileGroupSize * worldSettings.TileSize / 2;

                var g = new GameObject($"TileGroup - {center}", typeof(MeshFilter), typeof(MeshRenderer));
                g.transform.SetParent(world.TileContainer);
                g.transform.position = center;

                objects.Add(g);

                var r = g.GetComponent<MeshRenderer>();
                var mf = g.GetComponent<MeshFilter>();

                mf.sharedMesh = groupMesh;
                r.sharedMaterial = tileMaterial;

                var mat = r.material;
                mat.SetTexture(tileMapTextureShaderProperty, tileTexture);
                mat.SetVector(tileGroupSizeShaderProperty, groupSize);

                var selectableNode = g.AddComponent<SelectableCellGroup>();
                selectableNode.GridCellGroup = cellGroup;

                var collider = g.AddComponent<BoxCollider2D>();
                collider.size = groupSize * worldSettings.TileSize;

                bounds.Encapsulate(r.bounds);
                g.isStatic = true;
            }

            Texture2D CreateCellGroupTexture(IGridCell[][] cellGroup)
            {
                if (cellGroup.Length == 0) return null;

                var texture = new Texture2D(cellGroup.Length, cellGroup[0].Length, TextureFormat.RGFloat, false)
                {
                    filterMode = FilterMode.Point,
                    anisoLevel = 0
                };

                for (var x = cellGroup.Length - 1; x >= 0; x--)
                for (var y = cellGroup[x].Length - 1; y >= 0; y--)
                {
                    var cell = cellGroup[x][y];
                    var color = new Color
                    (
                        cell.CellWeight / 255f,
                        cell.SupportsTower ? 1 : 0,
                        0,
                        0
                    );

                    texture.SetPixel(x, y, color);
                }

                texture.Apply(false, true);

                groupTextures.Add(texture);
                return texture;
            }

            IEnumerable<IGridCell[][]> GroupifyGridCells()
            {
                List<IGridCell[][]> groups = new();
                List<IGridCell[]> currentGroup = new();
                List<IGridCell> groupY = new();

                //TODO use fraction to fix problem with partial groups not working correctly
                var horGroupCount = gridSettings.GridWidth / (double)worldSettings.TileGroupSize.x;
                var vertGroupCount = gridSettings.GridHeight / (double)worldSettings.TileGroupSize.y;
                var maxGroupSize = worldSettings.TileGroupSize;

                if (horGroupCount % 1 + vertGroupCount % 1 > double.Epsilon) throw new Exception($"World size is not divisible by the world tile group size! Make sure you increase the world size by steps of {worldSettings.TileGroupSize}");

                vertGroupCount = Math.Floor(vertGroupCount);
                horGroupCount = Math.Floor(horGroupCount);

                for (var xGroup = 0; xGroup < horGroupCount; xGroup++)
                for (var yGroup = 0; yGroup < vertGroupCount; yGroup++)
                {
                    for (var x = 0; x < maxGroupSize.x; x++)
                    {
                        var xOff = xGroup * maxGroupSize.x + x;
                        var yOff = yGroup * maxGroupSize.y;
                        for (var y = 0; y < maxGroupSize.y; y++) groupY.Add(nodes[xOff][yOff + y]);
                        currentGroup.Add(groupY.ToArray());
                        groupY.Clear();
                    }

                    groups.Add(currentGroup.ToArray());
                    currentGroup.Clear();
                }

                return groups.ToArray();
            }
        }

        private Mesh CreateGroupMesh()
        {
            List<int> tris = new();
            List<Vector3> verts = new();
            List<Vector2> uvs = new();

            var subDivideCount = worldSettings.TileGroupGroupSubDivideCount;
            var tileSizeFrac = worldSettings.TileSize * worldSettings.TileGroupSize / worldSettings.TileGroupGroupSubDivideCount;

            for (var x = 0; x < subDivideCount.x; x++)
            for (var y = 0; y < subDivideCount.y; y++)
                AddSubTile(x, y);

            var mesh = new Mesh
            {
                vertices = verts.ToArray(),
                triangles = tris.ToArray(),
                uv = uvs.ToArray()
            };
            mesh.UploadMeshData(true);
            return mesh;

            void AddSubTile(float offsetX, float offsetY)
            {
                tris.AddRange(new[] { verts.Count + 2, verts.Count + 1, verts.Count + 0 });
                tris.AddRange(new[] { verts.Count + 1, verts.Count + 2, verts.Count + 3 });

                AddVert(offsetX + 0, offsetY + 0, 0);
                AddVert(offsetX + 1, offsetY + 0, 0);
                AddVert(offsetX + 0, offsetY + 1, 0);
                AddVert(offsetX + 1, offsetY + 1, 0);

                var fracX = 1f / subDivideCount.x;
                var fracY = 1f / subDivideCount.y;
                AddUv(fracX * offsetX, fracY * offsetY);
                AddUv(fracX * (offsetX + 1), fracY * offsetY);
                AddUv(fracX * offsetX, fracY * (offsetY + 1));
                AddUv(fracX * (offsetX + 1), fracY * (offsetY + 1));
            }

            void AddVert(float x, float y, float z)
            {
                Vector3 v = new Vector3(x, y, z) * tileSizeFrac;

                verts.Add(v);
            }

            void AddUv(float x, float y)
            {
                uvs.Add(new Vector2(x, y));
            }
        }

        public void DestroyTiles()
        {
            Debug.Log("Destroyed Tiles");
            foreach (var groupTexture in groupTextures) Object.Destroy(groupTexture);
            groupTextures.Clear();

            foreach (var tile in tiles) Object.Destroy(tile);
            tiles = Array.Empty<GameObject>();
        }

        public void Dispose()
        {
            DestroyTiles();
        }
    }
}
