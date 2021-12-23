using Newtonsoft.Json;
using System;
using TowerDefence.World.Path.Data;

namespace TowerDefence.Systems.Waves.Data
{
    [Serializable]
    public readonly struct LevelData
    {
        [JsonProperty("Waves", NullValueHandling = NullValueHandling.Ignore)]
        public readonly Wave[] waves;

        [JsonProperty("Path", Required = Required.Always)]
        public readonly PathData path;

        public LevelData(Wave[] waves, PathData path)
        {
            this.waves = waves;
            this.path = path;
        }
    }
}