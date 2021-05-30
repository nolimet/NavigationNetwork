using System.Collections.Generic;
using System.Linq;
using TowerDefence.World.Path;
using UnityEngine;

namespace TowerDefence.World.Towers
{
    public abstract class TowerBase : MonoBehaviour
    {
        public abstract float TargetRadius { get; }

        public abstract void Tick();

        public virtual void Destroy()
        {
            Destroy(gameObject);
        }

        public virtual IEnumerable<T> GetWalkersInRange<T>() where T : WalkerBase
        {
            var results = Physics2D.OverlapCircleAll(transform.position, TargetRadius);
            return results.Select(x => x.GetComponent<T>()).Where(x => x);
        }
    }
}