using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace TowerDefence.World.Towers
{
    public abstract class TowerBase : MonoBehaviour
    {
        public abstract void Tick();

        public virtual void Destroy()
        {
            Destroy(gameObject);
        }
    }
}