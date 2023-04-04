using System;
using Newtonsoft.Json;
using TowerDefence.Entities.Components;
using TowerDefence.Entities.Towers.Components.Interfaces;
using UnityEngine;

namespace TowerDefence.Entities.Towers.Components.PowerComponents
{
    [Serializable, Component(ComponentType.Tower, typeof(IPowerComponent), AllowMultiple = false)]
    [JsonObject(MemberSerialization.OptIn)]
    public class SimpleGenerator : IPowerProducer
    {
        [JsonProperty] public double GenerationPerSecond { get; }
        [JsonProperty] public double GenerationDelayInMs { get; } = -1;
        [JsonProperty] public double MaxPowerBuffer { get; }
        public double PowerBuffer { get; private set; }
        public double delayTimer;

        public void PowerTick()
        {
            if (delayTimer <= 0)
            {
                double generationMult = GenerationDelayInMs > 1 ? GenerationDelayInMs / 1000 : 1;
                double addedAmount = GenerationPerSecond * generationMult * Time.deltaTime;
                PowerBuffer = Math.Min(MaxPowerBuffer, PowerBuffer + addedAmount);
                delayTimer = GenerationDelayInMs / 1000;
            }

            delayTimer -= Time.deltaTime;
        }

        public void Dispose()
        {
        }
    }
}