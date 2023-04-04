using System;

namespace TowerDefence.Entities.Towers.Components.Interfaces
{
    public interface IPowerProducer : IPowerComponent, IDisposable
    {
        double GenerationPerSecond { get; }
        double GenerationDelayInMs { get; }
        double PowerBuffer { get; }
        double MaxPowerBuffer { get; }
    }
}