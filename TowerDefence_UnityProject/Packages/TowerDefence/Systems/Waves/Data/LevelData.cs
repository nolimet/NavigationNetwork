using Newtonsoft.Json;
using System;
using TowerDefence.World.Grid.Data;
using TowerDefence.World.Path.Data;

namespace TowerDefence.Systems.Waves.Data
{
    [Serializable]
    public readonly struct LevelData
    {
        [JsonProperty("Waves", Required = Required.AllowNull, NullValueHandling = NullValueHandling.Ignore)]
        public readonly Wave[] waves;

        [JsonProperty("Path", Required = Required.AllowNull, NullValueHandling = NullValueHandling.Ignore)]
        public readonly PathData? path;

        [JsonProperty("gridData", Required = Required.AllowNull, NullValueHandling = NullValueHandling.Ignore)]
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