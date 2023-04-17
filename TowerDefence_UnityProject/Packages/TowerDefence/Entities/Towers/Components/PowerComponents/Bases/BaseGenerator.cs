using System;
using System.Collections.Generic;
using TowerDefence.Entities.Towers.Components.Interfaces;
using TowerDefence.Entities.Towers.Data;
using TowerDefence.Entities.Towers.Models;
using TowerDefence.Utility;

// ReSharper disable InconsistentNaming

namespace TowerDefence.Entities.Towers.Components.PowerComponents.Bases
{
    internal abstract class BaseGenerator : IPowerProducer, IInitializable
    {
        public abstract double GenerationPerSecond { get; }
        public abstract double GenerationDelayInMs { get; }
        public abstract double MaxPowerBuffer { get; }

        public event Action<IReadOnlyCollection<PowerEventArg>> PowerSend;

        protected IPowerTargetFinder PowerTargetFinder { get; private set; }
        protected readonly List<PowerEventArg> powerEventArgsList = new();
        public double PowerBuffer { get; protected set; }
        protected double delayTimer;

        public virtual void PostInit(ITowerObject towerObject, ITowerModel model)
        {
            PowerTargetFinder = model.Components.GetComponent<IPowerTargetFinder>();
        }

        public virtual void PowerTick(double deltaMs)
        {
            if (delayTimer <= 0)
            {
                powerEventArgsList.Clear();
                deltaMs /= 1000;

                var generationMult = GenerationDelayInMs > 1 ? GenerationDelayInMs / 1000 : 1;
                var addedAmount = GenerationPerSecond * generationMult * deltaMs;
                PowerBuffer = Math.Min(MaxPowerBuffer, PowerBuffer + addedAmount);
                delayTimer = GenerationDelayInMs / 1000;

                var maxPowerPush = PowerBuffer / PowerTargetFinder.Targets.Count;
                var length = PowerTargetFinder.Targets.Count;
                for (var i = 0; i < length; i++)
                {
                    var target = PowerTargetFinder.Targets[i];
                    switch (target.powerComponent)
                    {
                        case IPowerConsumer consumer:
                        {
                            var accepted = consumer.PushPower(maxPowerPush);
                            powerEventArgsList.Add(new PowerEventArg(target.worldPosition, accepted / maxPowerPush, target.powerComponent));

                            maxPowerPush += (accepted - maxPowerPush) / (length - i);
                            break;
                        }
                        case IPowerBuffer buffer:
                        {
                            var accepted = buffer.PushPower(maxPowerPush);
                            powerEventArgsList.Add(new PowerEventArg(target.worldPosition, accepted / maxPowerPush, target.powerComponent));

                            maxPowerPush += (accepted - maxPowerPush) / (length - i);
                            break;
                        }
                    }
                }

                PowerSend?.Invoke(powerEventArgsList);
            }

            delayTimer -= deltaMs;
        }

        public virtual void Dispose()
        {
        }
    }
}