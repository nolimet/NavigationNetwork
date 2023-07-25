using System;

namespace TowerDefence.Entities.Towers.Components.Interfaces
{
    public interface IPowerConsumer : IPowerComponent, IDisposable
    {
        double MaxStored { get; }
        double Stored { get; }

        bool TryConsume(double amount);

        /// <summary>
        ///     Pushes power into the buffer of this consumer
        /// </summary>
        /// <param name="maxAmount">maximium amount you can push into the consumer</param>
        /// <returns>Actual amount it accepted</returns>
        double PushPower(double maxAmount);
    }
}