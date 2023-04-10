using System;

namespace TowerDefence.Entities.Towers.Components.Interfaces
{
    public interface IPowerBuffer : IPowerComponent, IDisposable
    {
        double MaxStorageAmount { get; }
        double StoredAmount { get; }

        double PushPower(double maxPowerPush);
    }
}
