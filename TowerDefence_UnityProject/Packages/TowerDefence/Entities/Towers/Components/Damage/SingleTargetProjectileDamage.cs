using System;
using System.Collections.Generic;
using TowerDefence.Entities.Components;
using TowerDefence.Entities.Enemies;
using TowerDefence.Entities.Towers.Components.Interfaces;
using TowerDefence.Entities.Towers.Models;

namespace TowerDefence.Entities.Towers.Components.Damage
{
    [Component(ComponentType.Tower, typeof(IDamageComponent))]
    public class SingleTargetProjectileDamage : IDamageComponent, IInitializable
    {
        public event Action<IEnumerable<IEnemyObject>> AppliedDamageToTargets;
        public double DamagePerSecond { get; private set; }
        public void Tick()
        {
            throw new NotImplementedException();
        }

        public void PostInit(ITowerObject towerObject, ITowerModel model)
        {
            throw new NotImplementedException();
        }
    }
}