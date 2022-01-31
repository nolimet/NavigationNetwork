using DataBinding;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using TowerDefence.Entities.Towers.Builder;
using TowerDefence.Entities.Towers.Models;
using UnityEngine;

namespace TowerDefence.Entities.Towers.Components.Damage
{
    [Serializable, TowerComponent]
    public class DamageAllTargets : DamageComponentBase
    {
        [JsonProperty] private readonly double damage = 5;
        [JsonProperty] private readonly float damageInterval = 1;

        [NonSerialized] private float intervalTimer = 0f;
        public override double DamagePerSecond => damage / damageInterval;

        public override void PostInit(ITowerModel model)
        {
            base.PostInit(model);

            if (damageInterval < 0f)
            {
                throw new ArgumentOutOfRangeException("Invalid attack cooldown Time. Value should be greater or equal then 0");
            }
        }

        public override void Tick()
        {
            intervalTimer -= Time.deltaTime;
            if (intervalTimer <= 0 && targetFindComponent != null && targetFindComponent.FoundTargets.Any())
            {
                intervalTimer = damageInterval;
                foreach (var target in targetFindComponent.FoundTargets)
                {
                    target.Damage(damage);
                }
            }
        }
    }
}