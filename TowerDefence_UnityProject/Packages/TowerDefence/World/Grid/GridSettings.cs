using Newtonsoft.Json;
using System;
using UnityEngine;

namespace TowerDefence.World.Grid
{
    [Serializable]
    internal sealed class GridSettings
    {
        [field: SerializeField]
        [JsonProperty] public int GridHeight { get; private set; }

        [field: SerializeField]
        [JsonProperty] public int GridWidth { get; private set; }

        /// <summary>
        /// Grid weights and layout. -255 is not traversable
        /// </summary>
        [field: SerializeField]
        [JsonProperty] public byte[] gridLayout { get; private set; }
    }
}
