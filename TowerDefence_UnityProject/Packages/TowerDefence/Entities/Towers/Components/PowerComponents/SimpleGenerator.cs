using System;
using Newtonsoft.Json;
using TowerDefence.Entities.Components;
using TowerDefence.Entities.Towers.Components.Interfaces;
using TowerDefence.Entities.Towers.Components.PowerComponents.Bases;

namespace TowerDefence.Entities.Towers.Components.PowerComponents
{
    [Serializable]
    [Component(ComponentType.Tower, typeof(IPowerComponent), AllowMultiple = false)]
    [JsonObject(MemberSerialization.OptIn)]
    internal class SimpleGenerator : BaseGenerator
    {
        public override double GenerationPerSecond => generationPerSecond;
        [JsonProperty] private readonly double generationPerSecond = 100;
        public override double GenerationDelayInMs => generationDelayInMs;
        [JsonProperty] private readonly double generationDelayInMs = -1;
        public override double MaxPowerBuffer => maxPowerBuffer;
        [JsonProperty] private readonly double maxPowerBuffer = 1000;
    }
}