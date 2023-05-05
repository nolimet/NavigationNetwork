using System;
using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using TowerDefence.World.Grid.Data;
using UnityEngine;
using Object = UnityEngine.Object;

namespace TowerDefence.World.Grid
{
    internal sealed class GridVisualGenerator : IDisposable
    {
        private const float TileWidth = 1;
        private const float TileLength = 1;

        private readonly GridWorldSettings worldSettings;
        private readonly WorldContainer world;
        private readonly Dictionary<(bool buildable, byte height), Material> materialCache = new();

        private Mesh tileMesh;

        private GameObject[] tiles = Array.Empty<GameObject>();

        public GridVisualGenerator(GridWorldSettings worldSettings, WorldContainer world)
        {
            this.worldSettings = worldSettings;
            this.world = world;
        }

        public async UniTask<Bounds> CreateVisuals(IEnumerable<IGridCell> nodes, GridSettings gridSettings)
        {
            DestroyTiles();

            Bounds bounds = new();

            var heightMultShaderProperty = Shader.PropertyToID("_HeightMult");
            var supportsTowerShaderProperty = Shader.PropertyToID("_SupportsTower");
            var tileGroupSizeShaderProperty = -1;
            var tileMapTextureShaderProperty = -1;

            var tileMaterial = await worldSettings.GetTileMaterial();

            List<GameObject> objects = new();

            tileMesh = CreateMesh();

            var cellGroups = GroupifyGridCells();
            foreach (var cellGroup in cellGroups)
            {
                var groupTexture = CreateCellGroupTexture(cellGroup);
                var groupMesh = CreateGroupMesh(cellGroup);
            }

            foreach (var node in nodes) CreateRenderer(tileMesh, node);

            tiles = objects.ToArray();

            return bounds;

            Mesh CreateGroupMesh(IGridCell[][] cellGroup)
            {
                List<int> tris = new();
                List<Vector3> verts = new();
                List<Vector2> uvs = new();

                void AddSubTile(float offsetX, float offsetY)
                {
                    tris.AddRange(new[] { verts.Count + 2, verts.Count + 1, verts.Count + 0 });
                    tris.AddRange(new[] { verts.Count + 1, verts.Count + 2, verts.Count + 3 });

                    AddVert(offsetX + 0, offsetY + 0, 0);
                    AddVert(offsetX + 1, offsetY + 0, 0);
                    AddVert(offsetX + 0, offsetY + 1, 0);
                    AddVert(offsetX + 1, offsetY + 1, 0);

                    AddUv(0, 0);
                    AddUv(1, 0);
                    AddUv(0, 1);
                    AddUv(1, 1);
                }

                void AddVert(float x, float y, float z)
                {
                    Vector3 v = new
                    (
                        x * TileWidth,
                        y * TileLength,
                        z
                    );

                    verts.Add(v);
                }

                void AddUv(float x, float y)
                {
                    uvs.Add(new Vector2(x, y));
                }
            }

            Mesh CreateMesh()
            {
                List<int> tris = new();
                List<Vector3> verts = new();
                List<Vector2> uvs = new();

                tris.AddRange(new[] { 2, 1, 0 });
                tris.AddRange(new[] { 1, 2, 3 });

                AddVert(0, 0, 0);
                AddVert(1, 0, 0);
                AddVert(0, 1, 0);
                AddVert(1, 1, 0);

                AddUv(0, 0);
                AddUv(1, 0);
                AddUv(0, 1);
                AddUv(1, 1);

                var m = new Mesh
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
                        x * TileWidth,
                        y * TileLength,
                        z
                    );

                    verts.Add(v);
                }

                void AddUv(float x, float y)
                {
                    uvs.Add(new Vector2(x, y));
                }
            }

            void CreateRenderer(Mesh m, IGridCell cell)
            {
                //TODO: Convert grid into a single mesh. Use a shader to draw the visuals to lower the load on memory with the many gameobjects.
                //TODO: Make sure objects are still selectable. Could use some simple hit value calculations. Would require sorted IGridCell's
                //Convert grid into a single mesh. 
                //Using a shader trick to draw the mesh..
                //This is gone be fun -_-
                //This should work though :P
                var g = new GameObject($"Tile -{cell.Position}", typeof(MeshFilter), typeof(MeshRenderer));
                g.transform.SetParent(world.TileContainer);
                g.transform.position = new Vector3
                (
                    worldSettings.TileSize.x * cell.Position.x - worldSettings.TileSize.x / 2 * gridSettings.GridWidth,
                    worldSettings.TileSize.y * cell.Position.y - worldSettings.TileSize.y / 2 * gridSettings.GridHeight,
                    10
                );

                objects.Add(g);

                var r = g.GetComponent<MeshRenderer>();
                var mf = g.GetComponent<MeshFilter>();

                if (!materialCache.TryGetValue((cell.SupportsTower, cell.CellWeight), out var material))
                {
                    material = new Material(tileMaterial);
                    material.SetFloat(heightMultShaderProperty, 1f / 255 * cell.CellWeight);
                    material.SetInt(supportsTowerShaderProperty, cell.SupportsTower ? 1 : 0);
                    materialCache.Add((cell.SupportsTower, cell.CellWeight), material);
                }

                r.sharedMaterial = material;
                mf.sharedMesh = m;

                var selectableNode = g.AddComponent<SelectableCell>();
                selectableNode.GridCell = cell;

                var collider = g.AddComponent<BoxCollider2D>();
                collider.size = worldSettings.TileSize;

                bounds.Encapsulate(r.bounds);
            }

            Texture2D CreateCellGroupTexture(IGridCell[][] cellGroup)
            {
                if (cellGroup.Length == 0) return null;

                var texture = new Texture2D(cellGroup.Length, cellGroup[0].Length, TextureFormat.RGFloat, false)
                {
                    filterMode = FilterMode.Point,
                    anisoLevel = 0
                };

                var pixels = texture.GetPixels();
                var count = 0;

                for (var x = 0; x < cellGroup.Length; x++)
                for (var y = 0; y < cellGroup[y].Length; y++)
                {
                    var cell = cellGroup[x][y];
                    var color = new Color
                    (
                        cell.CellWeight / 255f,
                        cell.SupportsTower ? 1 : 0,
                        0,
                        0
                    );
                    pixels[count] = color;
                    count++;
                }

                texture.SetPixels(pixels);
                texture.Apply(false, true);
                return texture;
            }

            IGridCell[][][] GroupifyGridCells()
            {
                List<IGridCell[][]> groups = new();
                List<List<IGridCell>> currentGroup = new();

                var horGroupCount = (int)Math.Ceiling(gridSettings.GridWidth / (double)worldSettings.MaxTileGroupSize.x);
                var vertGroupCount = (int)Math.Ceiling(gridSettings.GridHeight / (double)worldSettings.MaxTileGroupSize.y);
                var maxGroupSize = worldSettings.MaxTileGroupSize;

                var nodesArr = nodes.ToArray();

                var counter = 0;
                for (var xGroup = 0; xGroup < horGroupCount; xGroup++)
                {
                    var offsetX = xGroup * maxGroupSize.x;
                    for (var yGroup = 0; yGroup < vertGroupCount; yGroup++)
                    {
                        var offsetY = offsetX + yGroup * maxGroupSize.y;
                        for (var x = 0; x < maxGroupSize.x; x++)
                        {
                            if (offsetX + x > gridSettings.GridWidth) continue;

                            var currentRow = new List<IGridCell>();
                            for (var y = 0; y < maxGroupSize.y; y++)
                            {
                                if (offsetY + y > gridSettings.GridHeight) continue;

                                currentRow.Add(nodesArr[counter]);
                                counter++;
                                if (counter > nodesArr.Length)
                                    break;
                            }

                            if (currentRow.Count > 0) currentGroup.Add(currentRow);
                        }
                    }

                    groups.Add(currentGroup.Select(x => x.ToArray()).ToArray());
                }

                return groups.ToArray();
            }
        }

        public void DestroyTiles()
        {
            Debug.Log("Destroyed Tiles");
            foreach (var tile in tiles) Object.Destroy(tile);

            tiles = Array.Empty<GameObject>();
        }

        public void Dispose()
        {
            foreach (var (_, value) in materialCache) Object.Destroy(value);
        }
    }
}
