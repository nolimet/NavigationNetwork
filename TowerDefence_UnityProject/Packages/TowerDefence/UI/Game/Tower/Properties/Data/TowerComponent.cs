using System.Collections.Generic;
using TowerDefence.Entities.Components;
using TowerDefence.UI.Game.Tower.Properties.Interfaces;

namespace TowerDefence.UI.Game.Tower.Properties.Data
{
    public record TowerComponent(IComponent Component, IReadOnlyList<ITowerProperty> Properties)
    {
        public string Name { get; } = Component.GetType().Name;
        public IComponent Component { get; } = Component;
        public IReadOnlyList<ITowerProperty> Properties { get; } = Properties;
    }
}