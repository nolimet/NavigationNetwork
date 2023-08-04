using System;
using Newtonsoft.Json;
using TowerDefence.Entities.Components;
using TowerDefence.Entities.Towers.Components.Interfaces;
using TowerDefence.Entities.Towers.Components.PowerComponents.Bases;
using TowerDefence.UI.Game.Tower.Properties.Attributes;

namespace TowerDefence.Entities.Towers.Components.PowerComponents
{
    [Serializable]
    [Component(ComponentType.Tower, typeof(IPowerComponent), AllowMultiple = false)]
    [JsonObject(MemberSerialization.OptIn)]
    internal class SimpleGenerator : BaseGenerator
    {
        public override double GenerationPerSecond => generationPerTick/(PowerTickService.PowerTickDelay/1000d);
        [JsonProperty] private readonly double generationPerTick = 100;
        public override double GenerationDelayInMs => generationDelayInMs;
        [JsonProperty] private readonly double generationDelayInMs = -1;
        [HiddenProperty] public override double MaxPowerBuffer => maxPowerBuffer;
        [JsonProperty] private readonly double maxPowerBuffer = 1000;
    }
}