using Newtonsoft.Json;
using System;
using TowerDefence.World.Grid.Data;
using TowerDefence.World.Path.Data;

namespace TowerDefence.Systems.Waves.Data
{
    [Serializable]
    public readonly struct LevelData
    {
        [JsonProperty("Waves", Required = Required.DisallowNull)]
        public readonly Wave[] waves;

        [JsonProperty("Path", NullValueHandling = NullValueHandling.Ignore)]
        public readonly PathData? path;

        [JsonProperty("gridData", NullValueHandling = NullValueHandling.Ignore)]
        internal readonly GridSettings? gridSettings;

        internal LevelData(Wave[] waves, PathData path)
        {
            this.waves = waves;
            this.path = path;
            this.gridSettings = default;
        }

        internal LevelData(Wave[] waves, GridSettings gridSettings)
        {
            this.waves = waves;
            this.gridSettings = gridSettings;
            this.path = default;
        }
    }
}