using System.Collections.Generic;
using System.Linq;
using TowerDefence.Entities.Enemies;
using TowerDefence.World.Path;
using UnityEngine;

namespace TowerDefence.World.Towers
{
    public abstract class SingleTargetTowerBase : TowerBase
    {
        protected readonly List<WalkerBase> TargetList = new List<WalkerBase>();

        protected T GetFirst<T>() where T : WalkerBase
        {
            if (TargetList.Count == 0)
            {
                return null;
            }

            return TargetList.FirstOrDefault() is T enemy ? enemy : null;
        }

        protected T GetNearest<T>() where T : WalkerBase
        {
            if (TargetList.Count == 0)
            {
                return null;
            }

            Vector2 towerPos = transform.position;
            var nearest = TargetList.OrderBy(x => Vector2.Distance(x.transform.position, towerPos)).FirstOrDefault();
            return nearest is T enemy ? enemy : null;
        }

        protected T GetHealthiest<T>() where T : EnemyBase
        {
            if (TargetList.Count == 0)
            {
                return null;
            }

            var healthiest = TargetList.Cast<EnemyBase>().OrderBy(x => x.Model.maxHealth / x.Model.health).Last();
            return healthiest is T enemy ? enemy : null;
        }

        protected T GetDeadest<T>() where T : EnemyBase
        {
            var deadest = TargetList.Cast<EnemyBase>().OrderBy(x => x.Model.maxHealth / x.Model.health).First();
            return deadest is T enemy ? enemy : null;
        }

        protected void UpdateTargetList()
        {
            var currentInRange = base.GetWalkersInRange<WalkerBase>();
            var newWalkers = currentInRange.Where(x => !TargetList.Contains(x));
            var removedWalkers = TargetList.Where(x => !currentInRange.Contains(x)).ToArray();

            foreach (WalkerBase removedWalker in removedWalkers)
            {
                TargetList.Remove(removedWalker);
            }

            TargetList.AddRange(newWalkers);
        }
    }
}