using System.Collections.Generic;
using System.Linq;
using TowerDefence.Entities.Towers.Models;
using TowerDefence.Systems.Selection;
using TowerDefence.World.Path;
using UnityEngine;

namespace TowerDefence.Entities.Towers
{
    //Keep this part kind of but use ITowerComponents to define the behaviour of the tower
    public abstract class TowerBase : MonoBehaviour, ISelectable
    {
        public abstract float TargetRadius { get; }
        public abstract float AttacksPerSecond { get; }
        protected float attackDelay;

        protected virtual bool CanAttack(bool tickDelay = true)
        {
            if (tickDelay)
            {
                attackDelay -= Time.deltaTime;
            }

            if (attackDelay <= 0)
            {
                attackDelay = 1 / AttacksPerSecond;
                return true;
            }

            return false;
        }

        public abstract void Tick();

        public virtual void Destroy()
        {
            Destroy(gameObject);
        }

        //TODO Create settings object
        public void Setup(ITowerModel towerModel)
        {
            towerModel.Range = TargetRadius;
        }

        protected virtual IEnumerable<T> GetWalkersInRange<T>() where T : WalkerBase
        {
            var results = Physics2D.OverlapCircleAll(transform.position, TargetRadius);
            return results.Select(x => x.GetComponent<T>()).Where(x => x);
        }
    }
}