using System.Collections.Generic;

namespace TowerDefence.Entities.Towers.Components.Interfaces
{
    internal interface IPowerTargetFinder
    {
        IReadOnlyList<ITowerObject> Targets { get; }
    }
}