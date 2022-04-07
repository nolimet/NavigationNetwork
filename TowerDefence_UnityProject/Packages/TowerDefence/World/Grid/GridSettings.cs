using Newtonsoft.Json;
using System;
using UnityEditor;
using UnityEngine;

namespace TowerDefence.World.Grid
{
    [Serializable]
    internal sealed class GridSettings
    {
        public GridSettings(int gridHeight, int gridWidth, Node[] nodes)
        {
            GridHeight = gridHeight;
            GridWidth = gridWidth;
            Nodes = nodes;
        }

        [JsonProperty] public int GridHeight { get; private set; }

        [JsonProperty] public int GridWidth { get; private set; }

        /// <summary>
        /// Grid weights and layout. 255 is not traversable
        /// </summary>
        [JsonProperty] public readonly Node[] Nodes;

        [Serializable]
        internal readonly struct Node
        {
            public readonly bool isTraversable;
            public readonly byte weight;

            public Node(byte weight)
            {
                this.isTraversable = weight == 255;
                this.weight = weight;
            }

            [JsonConstructor]
            public Node(bool isTraversable, byte weight)
            {
                this.isTraversable = isTraversable;
                this.weight = weight;
            }
        }

        [MenuItem("Test/Settings")]
        public static void TestWrite()
        {
            var layoutNodes = new Node[100];
            var r = new System.Random();
            for (int i = 0; i < layoutNodes.Length; i++)
            {
                layoutNodes[i] = new((byte)r.Next(0, 255));
            }
            var settings = new GridSettings(10, 10, layoutNodes);
            string json = JsonConvert.SerializeObject(settings, Formatting.Indented);
            Debug.Log(json);
            var val = JsonConvert.DeserializeObject<GridSettings>(json);
            Debug.Log(string.Join(",", val.Nodes));
        }
    }
}