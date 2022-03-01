using System;
using TowerDefence.Entities.Components;
using TowerDefence.Entities.Towers.Components.Interfaces;

namespace TowerDefence.Entities.Towers.Components.TargetFinders
{
    [Serializable, Component(ComponentType.Tower, typeof(ITargetFindComponent))]
    internal class AllTargetsFinder : TargetFindBase
    {
        public override void Tick()
        {
            targetList.Clear();
            targetList.AddRange(GetEnemyObjectsInRange());
        }
    }
}
