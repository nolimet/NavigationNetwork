using Newtonsoft.Json;
using System;
using UnityEditor;
using UnityEngine;

namespace TowerDefence.World.Grid
{
    [Serializable]
    internal sealed class GridSettings
    {
        public GridSettings(int gridHeight, int gridWidth, GridLayout gridLayout)
        {
            GridHeight = gridHeight;
            GridWidth = gridWidth;
            this.gridLayout = gridLayout;
        }

        [JsonProperty] public int GridHeight { get; private set; }

        [JsonProperty] public int GridWidth { get; private set; }

        /// <summary>
        /// Grid weights and layout. 255 is not traversable
        /// </summary>
        [JsonProperty] public GridLayout gridLayout { get; private set; }

        [System.Serializable]
        internal readonly struct GridLayout
        {
            public readonly LayoutNode[] nodes;

            public int Length => nodes.Length;

            public LayoutNode this[int index]
            {
                get => nodes[index];
            }

            public GridLayout(LayoutNode[] gridLayout)
            {
                this.nodes = gridLayout;
            }
        }

        internal readonly struct LayoutNode
        {
            public readonly bool isTraversable;
            public readonly byte weight;

            public LayoutNode(byte weight)
            {
                this.isTraversable = weight == 255;
                this.weight = weight;
            }
        }

        [MenuItem("Test/Settings")]
        public static void TestWrite()
        {
            var layoutNodes = new LayoutNode[100];
            var r = new System.Random();
            for (int i = 0; i < layoutNodes.Length; i++)
            {
                layoutNodes[i] = new((byte)r.Next(0, 255));
            }
            var settings = new GridSettings(10, 10, new GridLayout(layoutNodes));
            string json = JsonConvert.SerializeObject(settings, Formatting.Indented);
            Debug.Log(json);
            var val = JsonConvert.DeserializeObject<GridSettings>(json);
            Debug.Log(string.Join(",", val.gridLayout));
        }
    }
}