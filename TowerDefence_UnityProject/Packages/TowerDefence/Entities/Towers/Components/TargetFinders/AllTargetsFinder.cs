using System;
using TowerDefence.Entities.Components;
using TowerDefence.Entities.Towers.Components.Interfaces;

namespace TowerDefence.Entities.Towers.Components.TargetFinders
{
    [Serializable, Component(ComponentType.Tower, typeof(ITargetFindComponent))]
    internal sealed class AllTargetsFinder : TargetFindBase
    {
        public override void Tick()
        {
            TargetList.Clear();
            TargetList.AddRange(GetEnemyObjectsInRange());
        }
    }
}