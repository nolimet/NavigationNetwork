using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TowerDefence.Entities.Enemies;
using TowerDefence.World.Towers.Models;
using UnityEngine;

namespace TowerDefence.World.Towers.Components.TargetFinders
{
    public class NearestTargetFinder : TargetFindBase
    {
        public NearestTargetFinder(ITowerObject towerObject, ITowerModel towerModel) : base(towerObject, towerModel)
        {
        }

        public override void Tick()
        {
            targetList.Clear();

            var positionTower = towerObject.GetWorldPosition();
            var rawTargets = GetEnemyObjectsInRange();
            var orderedTargets = rawTargets.Select(x => new TargetFindHelper(Vector3.Distance(x.GetWorldPosition(), positionTower), x)).OrderBy(x => x.distance);

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
