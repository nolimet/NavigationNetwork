using System;
using System.Collections.Generic;
using TowerDefence.Entities.Towers.Data;
using UnityEngine;

namespace TowerDefence.Entities.Towers.Components.Interfaces
{
    public interface IPowerProducer : IPowerComponent, IDisposable, ITickablePowerComponent
    {
        double GenerationPerSecond { get; }
        double GenerationDelayInMs { get; }
        double PowerBuffer { get; }
        double MaxPowerBuffer { get; }

        event Action<IReadOnlyCollection<PowerEventArgs>> PowerSend;
    }
}