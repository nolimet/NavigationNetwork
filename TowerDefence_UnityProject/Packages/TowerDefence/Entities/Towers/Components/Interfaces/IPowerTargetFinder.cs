using System.Collections.Generic;
using TowerDefence.Entities.Components;

namespace TowerDefence.Entities.Towers.Components.Interfaces
{
    internal interface IPowerTargetFinder : IComponent
    {
        IReadOnlyList<IPowerComponent> Targets { get; }
    }
}
