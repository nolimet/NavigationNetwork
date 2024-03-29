﻿using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using TowerDefence.World.Grid.Data;
using UnityEngine;
using Object = UnityEngine.Object;

namespace TowerDefence.World.Grid
{
    internal sealed class GridVisualGenerator
    {
        private const float tileWidth = 1;
        private const float tileLength = 1;
        private readonly GridWorldSettings worldSettings;
        private readonly WorldContainer world;

        private Mesh tileMesh;

        private GameObject[] tiles = Array.Empty<GameObject>();

        public GridVisualGenerator(GridWorldSettings worldSettings, WorldContainer world)
        {
            this.worldSettings = worldSettings;
            this.world = world;
        }

        public async UniTask CreateVisuals(IEnumerable<IGridCell> nodes, GridSettings gridSettings)
        {
            DestroyTiles();

            int heightMultShaderProperty = Shader.PropertyToID("_HeightMult");
            int supportsTowerShaderProperty = Shader.PropertyToID("_SupportsTower");

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
                        x: x * tileWidth,
                        y: y * tileLength,
                        z: z
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
                var g = new GameObject($"Tile -{cell.Position}", typeof(MeshFilter), typeof(MeshRenderer));
                g.transform.SetParent(world.TileContainer);
                g.transform.position = new Vector3
                (
                    x: worldSettings.TileSize.x * cell.Position.x - worldSettings.TileSize.x / 2 * gridSettings.GridWidth,
                    y: worldSettings.TileSize.y * cell.Position.y - worldSettings.TileSize.y / 2 * gridSettings.GridHeight,
                    z: 10
                );

                objects.Add(g);

                var r = g.GetComponent<MeshRenderer>();
                var mf = g.GetComponent<MeshFilter>();

                r.sharedMaterial = tileMaterial;
                r.material.SetFloat(heightMultShaderProperty, 1f / 255 * cell.CellWeight);
                r.material.SetInt(supportsTowerShaderProperty, cell.SupportsTower ? 1 : 0);
                mf.sharedMesh = m;

                var selectableNode = g.AddComponent<SelectableCell>();
                selectableNode.GridCell = cell;

                var collider = g.AddComponent<BoxCollider2D>();
                collider.size = worldSettings.TileSize;
            }
        }

        public void DestroyTiles()
        {
            Debug.Log("Destroyed Tiles");
            foreach (var tile in tiles)
            {
                Object.Destroy(tile);
            }

            tiles = Array.Empty<GameObject>();
        }
    }
}