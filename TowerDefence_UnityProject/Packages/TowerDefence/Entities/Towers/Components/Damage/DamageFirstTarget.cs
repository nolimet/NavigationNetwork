﻿using Newtonsoft.Json;
using System;
using System.Linq;
using TowerDefence.Entities.Components;
using TowerDefence.Entities.Towers.Builder;
using TowerDefence.Entities.Towers.Models;
using UnityEngine;

namespace TowerDefence.Entities.Towers.Components.Damage
{
    [Serializable, Component(ComponentType.Tower)]
    internal class DamageFirstTarget : DamageComponentBase
    {
        [JsonProperty] private readonly double damage;
        [JsonProperty] private readonly float damageInterval;

        [NonSerialized] private float intervalTimer = 0f;
        public override double DamagePerSecond => damage / damageInterval;

        public override void PostInit(ITowerObject towerObject, ITowerModel towerModel)
        {
            base.PostInit(towerObject, towerModel);

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

                targetFindComponent.FoundTargets.First().Damage(damage);
            }
        }
    }
}