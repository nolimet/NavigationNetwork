using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TowerDefence.Entities.Towers.Models;

namespace TowerDefence.Entities.Towers.Components.Damage
{
    [Serializable]
    public abstract class DamageComponentBase : IDamageComponent
    {
        protected ITowerModel model { get; private set; }

        public abstract double DamagePerSecond { get; }

        public abstract void Tick();

        public virtual void PostInit(ITowerModel model)
        {
            this.model = model;
        }
    }
}