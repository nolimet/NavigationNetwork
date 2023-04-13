using System;
using System.Collections.Generic;
using TowerDefence.Entities.Towers.Data;

namespace TowerDefence.Entities.Towers.Components.Interfaces
{
    public interface IPowerProducer : IPowerComponent, IDisposable, ITickablePowerComponent
    {
        double GenerationPerSecond { get; }
        double GenerationDelayInMs { get; }
        double PowerBuffer { get; }
        double MaxPowerBuffer { get; }

        event Action<IReadOnlyCollection<PowerEventArg>> PowerSend;
    }
}