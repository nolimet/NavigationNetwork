using System;
using Newtonsoft.Json;
using TowerDefence.Entities.Components;
using TowerDefence.Entities.Towers.Components.Interfaces;

namespace TowerDefence.Entities.Towers.Components.PowerComponents
{
    [Serializable]
    [JsonObject(MemberSerialization.OptIn)]
    [Component(ComponentType.Tower, typeof(IPowerComponent), AllowMultiple = false)]
    public class NullPowerConsumer : IPowerConsumer
    {
        public double MaxStored => 0;
        public double Stored => 0;

        public bool TryConsume(double amount)
        {
            return true;
        }

        public double PushPower(double maxAmount)
        {
            return 0;
        }

        public void Dispose()
        {
        }
    }
}
