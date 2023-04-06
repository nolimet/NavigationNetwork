using System;
using Newtonsoft.Json;
using TowerDefence.Entities.Towers.Components.BaseComponents;
using TowerDefence.Entities.Towers.Components.Interfaces;
using TowerDefence.Entities.Towers.Models;
using TowerDefence.Utility;

namespace TowerDefence.Entities.Towers.Components.PowerComponents.Bases
{
    internal abstract class BaseGenerator : IPowerProducer, IInitializable
    {
        [JsonProperty] public double GenerationPerSecond { get; }
        [JsonProperty] public double GenerationDelayInMs { get; } = -1;
        [JsonProperty] public double MaxPowerBuffer { get; }
        public double PowerBuffer { get; protected set; }
        protected TowerSettings towerSettings { get; private set; }

        protected double delayTimer;

        public virtual void PostInit(ITowerObject towerObject, ITowerModel model)
        {
            towerSettings = model.Components.GetComponent<TowerSettings>();
        }

        public virtual void PowerTick(float delta)
        {
            if (delayTimer <= 0)
            {
                double generationMult = GenerationDelayInMs > 1 ? GenerationDelayInMs / 1000 : 1;
                double addedAmount = GenerationPerSecond * generationMult * delta;
                PowerBuffer = Math.Min(MaxPowerBuffer, PowerBuffer + addedAmount);
                delayTimer = GenerationDelayInMs / 1000;
            }

            delayTimer -= delta;
        }

        public virtual void Dispose()
        {
            
        }
    }
}