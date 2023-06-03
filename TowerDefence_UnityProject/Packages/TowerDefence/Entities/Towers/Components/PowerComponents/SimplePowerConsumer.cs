using Newtonsoft.Json;
using TowerDefence.Entities.Components;
using TowerDefence.Entities.Towers.Components.Interfaces;

namespace TowerDefence.Entities.Towers.Components.PowerComponents
{
    [Component(ComponentType.Tower)]
    public class SimplePowerConsumer : IPowerConsumer
    {
        [JsonProperty] public double MaxStored { get; private set; } = 1000;
        [JsonProperty] public double Stored { get; protected set; }

        public bool TryConsume(double amount)
        {
            if (Stored < amount) return false;
            Stored -= amount;
            return true;
        }

        public double PushPower(double maxAmount)
        {
            if (Stored + maxAmount > MaxStored)
            {
                double added = MaxStored - Stored;
                Stored = MaxStored;
                return added;
            }

            Stored += maxAmount;
            return maxAmount;
        }

        public void Dispose()
        {
        }
    }
}