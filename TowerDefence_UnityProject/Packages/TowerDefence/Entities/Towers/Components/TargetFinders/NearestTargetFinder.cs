using System;
using System.Linq;
using TowerDefence.Entities.Components;
using TowerDefence.Entities.Enemies;
using TowerDefence.Entities.Towers.Components.Interfaces;
using UnityEngine;

namespace TowerDefence.Entities.Towers.Components.TargetFinders
{
    [Serializable, Component(ComponentType.Tower, typeof(ITargetFindComponent))]
    public sealed class NearestTargetFinder : TargetFindBase
    {
        public override void Tick()
        {
            TargetList.Clear();

            var positionTower = TowerObject.GetWorldPosition();
            var rawTargets = GetEnemyObjectsInRange();
            var orderedTargets = rawTargets.Select(x => new TargetFindHelper(Vector3.Distance(x.GetWorldPosition(), positionTower), x)).OrderBy(x => x.Distance).ToArray();
            if (orderedTargets.Any())
                TargetList.Add(orderedTargets.First().Enemy);
        }

        private readonly struct TargetFindHelper
        {
            public readonly float Distance;
            public readonly IEnemyObject Enemy;

            public TargetFindHelper(float distance, IEnemyObject enemy)
            {
                Distance = distance;
                Enemy = enemy;
            }
        }
    }
}