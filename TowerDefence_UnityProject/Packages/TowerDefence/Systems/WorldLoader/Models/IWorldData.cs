using TowerDefence.Systems.Waves.Data;

namespace TowerDefence.Systems.WorldLoader.Models
{
    public interface IWorldData
    {
        string LevelName { get; set; }

        Wave[] Waves { get; set; }
    }
}