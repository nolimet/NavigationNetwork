using Newtonsoft.Json;
using System;
using UnityEditor;
using UnityEngine;

namespace TowerDefence.World.Grid
{
    [Serializable]
    internal sealed class GridSettings
    {
        public GridSettings(int gridHeight, int gridWidth, bool[] gridLayout)
        {
            GridHeight = gridHeight;
            GridWidth = gridWidth;
            this.gridLayout = gridLayout;
        }

        [JsonProperty] public int GridHeight { get; private set; }

        [JsonProperty] public int GridWidth { get; private set; }

        /// <summary>
        /// Grid weights and layout. -255 is not traversable
        /// </summary>
        [JsonProperty] public bool[] gridLayout { get; private set; }

        [MenuItem("Test/Settings")]
        public static void TestWrite()
        {
            var settings = new GridSettings(10, 10, new[] { true, true, true, true, false, false, true, true, false });
            string json = JsonConvert.SerializeObject(settings, Formatting.Indented);
            Debug.Log(json);
            var val = JsonConvert.DeserializeObject<GridSettings>(json);
            Debug.Log(string.Join(",", val.gridLayout));
        }
    }
}
