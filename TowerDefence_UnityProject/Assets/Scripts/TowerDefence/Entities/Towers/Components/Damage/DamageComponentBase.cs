using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TowerDefence.Entities.Towers.Models;

namespace TowerDefence.Entities.Towers.Components.Damage
{
    public abstract class DamageComponentBase : IDamageComponent
    {
        protected readonly ITowerModel model;

        protected DamageComponentBase(ITowerModel model)
        {
            this.model = model;
        }

        public abstract double DamagePerSecond { get; }

        public abstract void Tick();
    }
}