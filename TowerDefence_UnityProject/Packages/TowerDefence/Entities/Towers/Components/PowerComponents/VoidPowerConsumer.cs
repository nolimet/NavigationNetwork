using System;
using Newtonsoft.Json;
using TowerDefence.Entities.Components;
using TowerDefence.Entities.Towers.Components.Interfaces;

namespace TowerDefence.Entities.Towers.Components.PowerComponents
{
    [Serializable]
    [Component(ComponentType.Tower, typeof(IPowerComponent), AllowMultiple = false)]
    [JsonObject(MemberSerialization.OptIn)]
    public class VoidPowerConsumer : IPowerConsumer
    {
        public bool CanReceive => true;
        public double MaxStored => 999999999;
        public double Stored => 0;

        public bool TryConsume(double amount)
        {
            return true;
        }

        public double PushPower(double maxAmount)
        {
            return maxAmount;
        }

        public void Dispose()
        {
        }
    }
}