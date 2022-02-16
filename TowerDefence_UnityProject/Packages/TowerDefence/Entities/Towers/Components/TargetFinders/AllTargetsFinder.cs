using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
