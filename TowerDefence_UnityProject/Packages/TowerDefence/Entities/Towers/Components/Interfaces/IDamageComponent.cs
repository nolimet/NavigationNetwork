using TowerDefence.Entities.Components.Interfaces;

namespace TowerDefence.Entities.Towers.Components.Interfaces
{
    public interface IDamageComponent : ITickableComponent
    {
        double DamagePerSecond { get; }
    }
}