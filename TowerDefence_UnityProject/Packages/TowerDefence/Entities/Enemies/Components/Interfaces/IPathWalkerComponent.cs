using TowerDefence.Entities.Components.Interfaces;
using UnityEngine.Events;

namespace TowerDefence.Entities.Enemies.Components
{
    public interface IPathWalkerComponent : ITickableComponent
    {
        float PathProgress { get; }
        UnityAction<IEnemyObject> ReachedEnd { get; set; }
    }
}