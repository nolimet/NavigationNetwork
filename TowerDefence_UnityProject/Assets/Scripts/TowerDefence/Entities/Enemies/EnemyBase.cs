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
        private UnityAction<IEnemyBaseModel> onEnemyDied;

        [SerializeField] private double maxHealth = 0;
        [SerializeField] private Vector2 healthBarOffset = Vector2.zero;

        private IEnemyBaseModel model;
        protected BindingContext bindingContext = new(true);

        public IEnemyBaseModel Model => model;

        //TODO add sources or types support
        public virtual void ApplyDamage(double damage)
        {
            model.health -= damage;
        }

        public void Setup(UnityAction<IEnemyBaseModel> onEnemyDied, AnimationCurve3D path, IEnemyBaseModel model)
        {
            this.onEnemyDied = onEnemyDied;

            this.SetPath(path);
            this.model = model;

            model.health = maxHealth;
            model.maxHealth = maxHealth;
            model.healthOffset = healthBarOffset;
            bindingContext.Bind(model, x => x.health, OnHealthChanged);
        }

        private void OnHealthChanged(double obj)
        {
            if (model.health <= 0)
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