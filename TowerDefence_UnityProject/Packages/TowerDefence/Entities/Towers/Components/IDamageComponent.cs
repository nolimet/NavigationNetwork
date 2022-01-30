using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TowerDefence.Entities.Towers.Components
{
    public interface IDamageComponent : ITickableTowerComponent
    {
        double DamagePerSecond { get; }
    }
}