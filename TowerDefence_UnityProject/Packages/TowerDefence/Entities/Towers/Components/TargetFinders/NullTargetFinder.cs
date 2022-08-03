using System;
using System.Collections.Generic;
using TowerDefence.Entities.Components;
using TowerDefence.Entities.Enemies;
using TowerDefence.Entities.Towers.Components.Interfaces;

namespace TowerDefence.Entities.Towers.Components.TargetFinders
{
    [Serializable, Component(ComponentType.Tower, typeof(ITargetFindComponent))]
    internal class NullTargetFinder : ITargetFindComponent
    {
        public static readonly ITargetFindComponent Instance = new NullTargetFinder();

        public IEnumerable<IEnemyObject> FoundTargets => Array.Empty<IEnemyObject>();

        public void Tick()
        {

        }
    }
}
