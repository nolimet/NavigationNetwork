using TowerDefence.Entities.Components.Interfaces;

namespace TowerDefence.Entities.Enemies.Components
{
    public interface IPathWalkerComponent : ITickableComponent
    {
        float PathProgress { get; }
    }
}