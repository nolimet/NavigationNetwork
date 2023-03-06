using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using TowerDefence.Entities.Components;
using TowerDefence.Entities.Enemies;
using TowerDefence.Entities.Towers.Components.Interfaces;
using TowerDefence.Entities.Towers.Models;
using UnityEngine;

namespace TowerDefence.Entities.Towers.Components.Damage
{
    [Serializable, Component(ComponentType.Tower, typeof(IDamageComponent))]
    [JsonObject(MemberSerialization.OptIn)]
    internal sealed class DamageFirstTarget : DamageComponentBase
    {
        [JsonProperty] private readonly double damage;
        [JsonProperty] private readonly float damageInterval;

        private float intervalTimer;
        public override event Action<IEnumerable<IEnemyObject>> AppliedDamageToTargets;
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
            if (intervalTimer > 0 || !TargetFindComponent.FoundTargets.Any()) return;
            intervalTimer = damageInterval;

            var target = TargetFindComponent.FoundTargets.First();
            target.Damage(damage);
            AppliedDamageToTargets?.Invoke(new[] { target });
        }
    }
}