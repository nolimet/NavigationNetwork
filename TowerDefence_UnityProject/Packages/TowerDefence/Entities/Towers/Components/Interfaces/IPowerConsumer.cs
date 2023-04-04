using System;

namespace TowerDefence.Entities.Towers.Components.Interfaces
{
    public interface IPowerConsumer : IPowerComponent, IDisposable
    {
        double MaxStored { get; }
        double Stored { get; }

        bool TryConsume(double amount);
        void UpdateBuffer(double maxConsumeAmount);
    }
}