using System;
using Newtonsoft.Json;
using TowerDefence.Entities.Towers.Components.Interfaces;
using TowerDefence.Entities.Towers.Models;
using TowerDefence.Utility;

// ReSharper disable InconsistentNaming

namespace TowerDefence.Entities.Towers.Components.PowerComponents.Bases
{
    internal abstract class BaseGenerator : IPowerProducer, IInitializable
    {
        [JsonProperty] public double GenerationPerSecond { get; }

        [JsonProperty] public double GenerationDelayInMs { get; } = -1;

        [JsonProperty] public double MaxPowerBuffer { get; }

        protected IPowerTargetFinder PowerTargetFinder { get; private set; }
        public double PowerBuffer { get; protected set; }
        protected double delayTimer;

        public virtual void PostInit(ITowerObject towerObject, ITowerModel model)
        {
            PowerTargetFinder = model.Components.GetComponent<IPowerTargetFinder>();
        }

        public virtual void PowerTick(double delta)
        {
            if (delayTimer <= 0)
            {
                var generationMult = GenerationDelayInMs > 1 ? GenerationDelayInMs / 1000 : 1;
                var addedAmount = GenerationPerSecond * generationMult * delta;
                PowerBuffer = Math.Min(MaxPowerBuffer, PowerBuffer + addedAmount);
                delayTimer = GenerationDelayInMs / 1000;

                var maxPowerPush = PowerBuffer / PowerTargetFinder.Targets.Count;
                var length = PowerTargetFinder.Targets.Count;
                for (var i = 0; i < length; i++)
                    switch (PowerTargetFinder.Targets[i])
                    {
                        case IPowerConsumer consumer:
                        {
                            var accepted = consumer.PushPower(maxPowerPush);
                            maxPowerPush += (accepted - maxPowerPush) / (length - i);
                            break;
                        }
                        case IPowerBuffer buffer:
                        {
                            var accepted = buffer.PushPower(maxPowerPush);
                            maxPowerPush += (accepted - maxPowerPush) / (length - i);
                            break;
                        }
                    }
            }

            delayTimer -= delta;
        }

        public virtual void Dispose()
        {
        }
    }
}
