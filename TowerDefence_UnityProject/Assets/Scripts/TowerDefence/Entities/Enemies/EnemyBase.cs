using DataBinding;
using System;
using TowerDefence.Entities.Enemies.Models;
using TowerDefence.World.Path;
using UnityEngine;
using UnityEngine.Events;
using static TowerDefence.World.Path.Data.PathWorldData;

namespace TowerDefence.Entities.Enemies
{
    public abstract class EnemyBase : WalkerBase
    {
        private UnityAction<IEnemyModel> onEnemyDied;

        [SerializeField] private double maxHealth = 0;
        [SerializeField] private Vector2 healthBarOffset = Vector2.zero;

        private IEnemyModel model;
        protected BindingContext bindingContext = new(true);

        public IEnemyModel Model => model;

        //TODO add sources or types support
        public virtual void ApplyDamage(double damage)
        {
            model.Health -= damage;
        }

        public void Setup(UnityAction<IEnemyModel> onEnemyDied, AnimationCurve3D path, IEnemyModel model)
        {
            this.onEnemyDied = onEnemyDied;

            this.SetPath(path);
            this.model = model;

            model.Health = maxHealth;
            model.MaxHealth = maxHealth;
            model.HealthOffset = healthBarOffset;
            bindingContext.Bind(model, x => x.Health, OnHealthChanged);
        }

        private void OnHealthChanged(double obj)
        {
            if (model.Health <= 0)
            {
                onEnemyDied?.Invoke(model);
            }
        }

        public override void ReachedEnd()
        {
            onEnemyDied?.Invoke(model);
        }

        private void OnDestroy()
        {
            bindingContext.Dispose();
        }
    }
}