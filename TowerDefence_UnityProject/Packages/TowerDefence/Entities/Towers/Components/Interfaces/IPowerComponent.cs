using TowerDefence.Entities.Components;

namespace TowerDefence.Entities.Towers.Components.Interfaces
{
    public interface IPowerComponent : IComponent
    {
        bool CanReceive { get; }
    }
}