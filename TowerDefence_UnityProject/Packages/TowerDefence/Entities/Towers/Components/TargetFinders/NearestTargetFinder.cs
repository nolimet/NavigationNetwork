using System;
using System.Linq;
using TowerDefence.Entities.Enemies;
using TowerDefence.Entities.Towers.Builder;
using UnityEngine;

namespace TowerDefence.Entities.Towers.Components.TargetFinders
{
    [Serializable, Component]
    public class NearestTargetFinder : TargetFindBase
    {
        public NearestTargetFinder()
        {
        }

        public override void Tick()
        {
            targetList.Clear();

            var positionTower = towerObject.GetWorldPosition();
            var rawTargets = GetEnemyObjectsInRange();
            var orderedTargets = rawTargets.Select(x => new TargetFindHelper(Vector3.Distance(x.GetWorldPosition(), positionTower), x)).OrderBy(x => x.distance);
            if (orderedTargets.Any())
                targetList.Add(orderedTargets.First().enemy);
        }

        private struct TargetFindHelper
        {
            public float distance;
            public IEnemyObject enemy;

            public TargetFindHelper(float distance, IEnemyObject enemy)
            {
                this.distance = distance;
                this.enemy = enemy;
            }
        }
    }
}