using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using TowerDefence.Entities.Towers.Components.Interfaces;
using TowerDefence.Entities.Towers.Data;
using TowerDefence.Entities.Towers.Models;
using TowerDefence.UI.Game.Tower.Properties.Attributes;
using TowerDefence.Utility;

namespace TowerDefence.Entities.Towers.Components.PowerComponents.Bases
{
    internal abstract class BaseGenerator : IPowerProducer, IInitializable
    {
        [JsonProperty] [HiddenProperty] public bool CanReceive { get; private set; }
        public abstract double GenerationPerSecond { get; }
        public abstract double GenerationDelayInMs { get; }
        public abstract double MaxPowerBuffer { get; }

        public event Action<IReadOnlyCollection<PowerEventArg>> PowerSend;

        protected IPowerTargetFinder PowerTargetFinder { get; private set; }
        protected readonly List<PowerEventArg> PowerEventArgsList = new();

        [ProgressBarProperty(nameof(MaxPowerBuffer))]
        public double PowerBuffer { get; protected set; }

        protected double DelayTimer;

        public virtual void PostInit(ITowerObject towerObject, ITowerModel model)
        {
            PowerTargetFinder = model.Components.GetComponent<IPowerTargetFinder>();
        }

        public virtual void PowerTick(double deltaMs)
        {
            if (DelayTimer <= 0)
            {
                PowerEventArgsList.Clear();

                var deltaS = deltaMs / 1000d;
                var generationMult = GenerationDelayInMs > 1 ? GenerationDelayInMs / 1000 : 1;
                var addedAmount = GenerationPerSecond * generationMult * deltaS;
                PowerBuffer = Math.Min(MaxPowerBuffer, PowerBuffer + addedAmount);
                DelayTimer = GenerationDelayInMs;

                var maxPowerPush = PowerBuffer / PowerTargetFinder.Targets.Count;
                var totalPushed = 0d;
                var length = PowerTargetFinder.Targets.Count(x => x.powerComponent is not IPowerProducer);
                for (var i = 0; i < length; i++)
                {
                    var target = PowerTargetFinder.Targets[i];
                    var accepted = target.powerComponent switch
                    {
                        IPowerConsumer consumer => consumer.PushPower(maxPowerPush),
                        IPowerBuffer buffer => buffer.PushPower(maxPowerPush),
                        _ => 0
                    };

                    if (accepted > 0)
                        PowerEventArgsList.Add(new PowerEventArg(target.worldPosition, accepted / maxPowerPush,
                            target.powerComponent));

                    totalPushed += accepted;
                    maxPowerPush += (accepted - maxPowerPush) / (length - i);
                }

                PowerBuffer -= totalPushed;

                PowerSend?.Invoke(PowerEventArgsList);
            }

            DelayTimer -= deltaMs;
        }

        public virtual void Dispose()
        {
        }
    }
}