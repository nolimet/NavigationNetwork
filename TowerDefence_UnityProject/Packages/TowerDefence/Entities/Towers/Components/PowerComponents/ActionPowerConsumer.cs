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

        public void UpdateBuffer(double maxConsumeAmount)
        {
            MaxStored = maxConsumeAmount * 2;
        }

        public void PowerTick(float delta)
        {
        }

        public void Dispose()
        {
        }
    }
}