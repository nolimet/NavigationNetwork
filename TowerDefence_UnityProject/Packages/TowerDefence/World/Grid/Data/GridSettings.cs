﻿using Newtonsoft.Json;
using System;
using UnityEditor;
using UnityEngine;

namespace TowerDefence.World.Grid.Data
{
    [Serializable]
    internal readonly struct GridSettings
    {
        public GridSettings(int gridHeight, int gridWidth, Cell[] cells, Vector2Int[] entryPoints, Vector2Int[] endPoints)
        {
            GridHeight = gridHeight;
            GridWidth = gridWidth;
            Cells = cells;
            EntryPoints = entryPoints;
            EndPoints = endPoints;
        }

        [JsonProperty] public readonly Vector2Int[] EntryPoints;
        [JsonProperty] public readonly Vector2Int[] EndPoints;

        [JsonProperty] public readonly int GridHeight;

        [JsonProperty] public readonly int GridWidth;

        /// <summary>
        /// Grid weights and layout. 255 is not traversable
        /// </summary>
        [JsonProperty] public readonly Cell[] Cells;

        [Serializable]
        internal readonly struct Cell
        {
            public readonly bool isTraversable;
            public readonly byte weight;

            public Cell(byte weight)
            {
                this.isTraversable = weight == 255;
                this.weight = weight;
            }

            [JsonConstructor]
            public Cell(bool isTraversable, byte weight)
            {
                this.isTraversable = isTraversable;
                this.weight = weight;
            }
        }

        [MenuItem("Test/Settings")]
        public static void TestWrite()
        {
            var layoutCells = new Cell[100];
            var r = new System.Random();
            for (int i = 0; i < layoutCells.Length; i++)
            {
                layoutCells[i] = new((byte)r.Next(0, 255));
            }
            var settings = new GridSettings(10, 10, layoutCells, new[] { new Vector2Int(5, 0) }, new[] { new Vector2Int(5, 10) });
            string json = JsonConvert.SerializeObject(settings, Formatting.Indented);
            Debug.Log(json);
            var val = JsonConvert.DeserializeObject<GridSettings>(json);
            Debug.Log(string.Join(",", val.Cells));
        }
    }
}