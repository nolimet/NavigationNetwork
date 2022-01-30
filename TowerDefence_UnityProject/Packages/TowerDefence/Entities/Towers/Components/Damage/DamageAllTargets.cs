using DataBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TowerDefence.Entities.Towers.Models;
using UnityEngine;

namespace TowerDefence.Entities.Towers.Components.Damage
{
    public class DamageAllTargets : DamageComponentBase
    {
        private readonly double damage;
        private readonly float attackCooldownTime;
        private readonly BindingContext bindingContext = new(true);

        private ITargetFindComponent targetFindComponent;
        private float cooldownTimer = 0f;

        public DamageAllTargets(ITowerModel model, double damage, float attackCooldownTime) : base(model)
        {
            this.damage = damage;
            if (attackCooldownTime < 0f)
            {
                throw new ArgumentOutOfRangeException("Invalid attack cooldown Time. Value should be greater or equal then 0");
            }
            this.attackCooldownTime = attackCooldownTime;

            bindingContext.Bind(model, x => x.Components, OnComponentsChanged);
        }

        ~DamageAllTargets()
        {
            bindingContext.Dispose();
        }

        private void OnComponentsChanged(IList<ITowerComponent> obj)
        {
            targetFindComponent = obj.Any(x => x is ITargetFindComponent) ? obj.First(x => x is ITargetFindComponent) as ITargetFindComponent : null;
        }

        public override double DamagePerSecond => damage / attackCooldownTime;

        public override void Tick()
        {
            cooldownTimer -= Time.deltaTime;
            if (cooldownTimer <= 0 && targetFindComponent != null && targetFindComponent.FoundTargets.Any())
            {
                cooldownTimer = attackCooldownTime;
                foreach (var target in targetFindComponent.FoundTargets)
                {
                    target.Damage(damage);
                }
            }
        }
    }
}