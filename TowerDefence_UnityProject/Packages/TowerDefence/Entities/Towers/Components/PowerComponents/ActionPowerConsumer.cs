using System;
using Newtonsoft.Json;
using TowerDefence.Entities.Components;
using TowerDefence.Entities.Towers.Components.Interfaces;

namespace TowerDefence.Entities.Towers.Components.PowerComponents
{
    [Serializable]
    [JsonObject(MemberSerialization.OptIn)]
    [Component(ComponentType.Tower, typeof(IPowerComponent), AllowMultiple = false)]
    public class ActionPowerConsumer : IPowerConsumer
    {
        public double MaxStored { get; private set; }
        public double Stored { get; private set; }

        public bool TryConsume(double amount)
        {
            if (Stored < amount) return false;

            Stored -= amount;
            return true;
        }

        public double PushPower(double maxAmount)
        {
            var newStored = Stored + maxAmount;
            if (newStored > MaxStored)
            {
                Stored = MaxStored;
                return MaxStored - Stored;
            }

            Stored = newStored;
            return maxAmount;
        }

        public void Dispose()
        {
        }
    }
}
