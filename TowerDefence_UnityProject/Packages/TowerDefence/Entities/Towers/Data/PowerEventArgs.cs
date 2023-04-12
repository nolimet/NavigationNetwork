using TowerDefence.Entities.Towers.Components.Interfaces;
using UnityEngine;

namespace TowerDefence.Entities.Towers.Data
{
    public record PowerEventArgs(Vector2 worldPosition, double acceptPercentage, IPowerComponent target)
    {
        public Vector2 worldPosition { get; } = worldPosition;
        public double acceptPercentage { get; } = acceptPercentage;
        public IPowerComponent target { get; } = target;
    }
}