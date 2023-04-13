using TowerDefence.Entities.Towers.Components.Interfaces;
using UnityEngine;

namespace TowerDefence.Entities.Towers.Data
{
    public record PowerEventArg(Vector2 worldPosition, double acceptPercentage, IPowerComponent target)
    {
        public Vector2 worldPosition { get; } = worldPosition;
        public double acceptPercentage { get; } = double.IsNaN(acceptPercentage) ? 0 : acceptPercentage;
        public IPowerComponent target { get; } = target;
    }
}