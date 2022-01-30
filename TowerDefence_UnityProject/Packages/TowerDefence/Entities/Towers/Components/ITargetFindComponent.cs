using System.Collections.Generic;
using TowerDefence.Entities.Enemies;

namespace TowerDefence.Entities.Towers.Components
{
    public interface ITargetFindComponent : ITickableTowerComponent
    {
        /// <summary>
        /// List of defined list of a single or multiple targets to apply any effects too
        /// </summary>
        IEnumerable<IEnemyObject> FoundTargets { get; }
    }
}