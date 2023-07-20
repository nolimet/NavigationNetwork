using System.Collections.Generic;
using TowerDefence.Entities.Components.Interfaces;
using TowerDefence.Entities.Enemies;

namespace TowerDefence.Entities.Towers.Components.Interfaces
{
    public interface ITargetFindComponent : ITickableComponent
    {
        /// <summary>
        /// List of defined list of a single or multiple targets to apply any effects too
        /// </summary>
        IReadOnlyList<IEnemyObject> FoundTargets { get; }
    }
}