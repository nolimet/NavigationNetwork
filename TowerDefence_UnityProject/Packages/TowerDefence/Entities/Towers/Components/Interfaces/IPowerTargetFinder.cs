using System.Collections.Generic;
using TowerDefence.Entities.Components;
using UnityEngine;

namespace TowerDefence.Entities.Towers.Components.Interfaces
{
    internal interface IPowerTargetFinder : IComponent
    {
        IReadOnlyList<(Vector2 worldPosition, IPowerComponent powerComponent)> Targets { get; }
    }
}