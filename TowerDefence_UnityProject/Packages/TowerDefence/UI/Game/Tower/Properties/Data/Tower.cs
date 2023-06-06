using System.Collections.Generic;
using TowerDefence.Entities.Towers.Models;

namespace TowerDefence.UI.Game.Tower.Properties.Data
{
    public record Tower(string Name, ITowerModel Model, IReadOnlyList<TowerComponent> Components)
    {
        public string Name { get; } = Name;
        public ITowerModel Model { get; } = Model;
        public IReadOnlyList<TowerComponent> Components { get; } = Components;
    }
}