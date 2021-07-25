using System;
using TowerDefence.World.Path.Data;

namespace TowerDefence.Systems.Waves.Data
{
    [Serializable]
    public readonly struct LevelData
    {
        public readonly Wave[] waves;
        public readonly PathData path;

        public LevelData(Wave[] waves, PathData path)
        {
            this.waves = waves;
            this.path = path;
        }
    }
}