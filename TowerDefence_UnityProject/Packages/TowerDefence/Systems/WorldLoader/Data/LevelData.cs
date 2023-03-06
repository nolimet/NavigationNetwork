using System;
using Newtonsoft.Json;
using TowerDefence.Systems.Waves.Data;
using TowerDefence.World.Grid.Data;
using TowerDefence.World.Path.Data;

namespace TowerDefence.Systems.WorldLoader.Data
{
    //TODO move this
    [Serializable]
    public readonly struct LevelData
    {
        [JsonProperty("Waves", Required = Required.DisallowNull)]
        public readonly Wave[] Waves;

        [JsonProperty("Path", NullValueHandling = NullValueHandling.Ignore)]
        public readonly PathData? Path;

        [JsonProperty("gridData", NullValueHandling = NullValueHandling.Ignore)]
        internal readonly GridSettings? GridSettings;

        internal LevelData(Wave[] waves, PathData path)
        {
            Waves = waves;
            Path = path;
            GridSettings = default;
        }

        internal LevelData(Wave[] waves, GridSettings gridSettings)
        {
            Waves = waves;
            GridSettings = gridSettings;
            Path = default;
        }
    }
}