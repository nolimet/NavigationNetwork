using System.Linq;
using TowerDefence.Entities.Enemies;
using TowerDefence.Entities.Towers.Models;
using UnityEngine;

namespace TowerDefence.Entities.Towers.Components.TargetFinders
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