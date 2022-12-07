using System;
using System.Collections.Generic;
using TowerDefence.Entities.Components.Interfaces;
using TowerDefence.Entities.Enemies;

namespace TowerDefence.Entities.Towers.Components.Interfaces
{
    public interface IDamageComponent : ITickableComponent
    {
        event Action<IEnumerable<IEnemyObject>> AppliedDamageToTargets;
        double DamagePerSecond { get; }
    }
}