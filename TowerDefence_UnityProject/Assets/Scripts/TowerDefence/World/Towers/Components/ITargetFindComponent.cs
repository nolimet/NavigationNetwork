using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TowerDefence.Entities.Enemies;
using TowerDefence.Entities.Enemies.Models;

namespace TowerDefence.World.Towers.Components
{
    public interface ITargetFindComponent : ITickableTowerComponent
    {
        /// <summary>
        /// List of defined list of a single or multiple targets to apply any effects too
        /// </summary>
        IEnumerable<IEnemyObject> FoundTarget { get; }
    }
}