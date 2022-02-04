using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TowerDefence.Entities.Components;

namespace TowerDefence.Entities.Towers.Components.BaseComponents
{
    [Component(ComponentType.Tower)]
    internal class TowerSettings : IComponent
    {
        public readonly string Name;
        public readonly double Range;
    }
}